using DirectShowLib;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using WebcamController.Models;

namespace WebcamController.Services
{
    public class CameraService : IDisposable
    {
        private IAMCameraControl _cameraControl;
        private IBaseFilter _cameraFilter;

        public Device ConnectedDevice = null;

        public List<DsDevice> GetAvailableCameras()
        {
            return DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice).ToList();
        }

        #region Gerenciamento de propriedades

        public void Connect(Device device)
        {
            if (ConnectedDevice != null) Disconnect();

            var dsDevice = GetDsDevice(device);
            Guid guid = typeof(IBaseFilter).GUID;
            dsDevice.Mon.BindToObject(null, null, ref guid, out object filter);
            _cameraFilter = (IBaseFilter)filter;
            _cameraControl = _cameraFilter as IAMCameraControl;
            ConnectedDevice = device;

            if (_cameraControl == null)
            {
                Disconnect();
                throw new NotSupportedException("Camera does not support IAMCameraControl.");
            }
        }

        public void Dispose()
        {
            Disconnect();
        }

        public void Disconnect()
        {
            if (ConnectedDevice == null) return;

            if (_cameraControl != null)
            {
                Marshal.ReleaseComObject(_cameraControl);
                _cameraControl = null;
            }

            if (_cameraFilter != null)
            {
                Marshal.ReleaseComObject(_cameraFilter);
                _cameraFilter = null;
            }

            ConnectedDevice = null;
        }

        public bool SupportsControlProperty(CameraControlProperty prop)
        {
            if (ConnectedDevice == null) throw new InvalidOperationException("Camera is not connected.");

            return _cameraControl.GetRange(prop, out _, out _, out _, out _, out _) == 0;
        }

        public (int Current, CameraControlFlags Flags) GetControlProperty(CameraControlProperty prop)
        {
            if (ConnectedDevice == null) throw new InvalidOperationException("Camera is not connected.");
            _cameraControl.Get(prop, out int current, out CameraControlFlags flags);
            return (current, flags);
        }
        public (int Min, int Max, int Step, int Default, CameraControlFlags Flags) GetControlPropertyRange(CameraControlProperty prop)
        {
            if (ConnectedDevice == null) throw new InvalidOperationException("Camera is not connected.");

            _cameraControl.GetRange(prop, out int min, out int max, out int step, out int def, out CameraControlFlags flags);
            return (min, max, step, def, flags);
        }

        public void SetControlProperty(CameraControlProperty prop, int value, CameraControlFlags flag)
        {
            if (ConnectedDevice == null) throw new InvalidOperationException("Camera is not connected."); ;
            _cameraControl?.Set(prop, value, flag);
        }

        public bool SupportsPropertyPages()
        {
            if (ConnectedDevice == null) throw new InvalidOperationException("Camera is not connected."); ;
            return _cameraFilter is ISpecifyPropertyPages;
        }

        public void ShowPropertyPage(nint ownerHandle)
        {
            if (ConnectedDevice == null) throw new InvalidOperationException("Camera is not connected.");
            
            nint filterPtr = nint.Zero;
            DsCAUUID caGUID = new DsCAUUID();
            try
            {
                (_cameraFilter as ISpecifyPropertyPages).GetPages(out caGUID);
                filterPtr = Marshal.GetIUnknownForObject(_cameraFilter);

                string caption = ConnectedDevice.FriendlyName;
                int hr = NativeMethods.OleCreatePropertyFrame(ownerHandle, 0, 0, caption, 1, new[] { filterPtr }, caGUID.cElems, caGUID.pElems, 0, 0, nint.Zero);
                DsError.ThrowExceptionForHR(hr);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to display property page.", ex);
            }
            finally
            {
                if (caGUID.pElems != nint.Zero) Marshal.FreeCoTaskMem(caGUID.pElems);
                if (filterPtr != nint.Zero) Marshal.Release(filterPtr);
            }
        }

        private DsDevice GetDsDevice(Device device)
        {
            var dsDevice = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice)
                .FirstOrDefault(d => d.DevicePath == device.DevicePath);
            return dsDevice;
        }

        #endregion

        #region Captura de quadro
        public Bitmap CapturePhoto()
        {
            if (ConnectedDevice == null)
                throw new InvalidOperationException("Câmera não está conectada.");

            using (var session = new FrameCaptureSession(_cameraFilter))
            {
                Bitmap capturedBitmap = session.CaptureFrame();
                return ResizeAndCenterBitmap(capturedBitmap);
            }
        }
        public class FrameCaptureSession : IDisposable
        {
            private IGraphBuilder _graph;
            private ICaptureGraphBuilder2 _captureGraph;
            private ISampleGrabber _sampleGrabber;
            private IBaseFilter _grabberBase;
            private IMediaControl _mediaControl;
            private readonly SampleGrabberCallback _callback;
            private bool _disposed = false;

            public FrameCaptureSession(IBaseFilter _cameraFilter)
            {
                if (_cameraFilter == null)
                    throw new ArgumentNullException(nameof(_cameraFilter), "O filtro da câmera não pode ser nulo.");

                try
                {
                    // 1. Inicializa os objetos COM
                    _graph = (IGraphBuilder)new FilterGraph();
                    _captureGraph = (ICaptureGraphBuilder2)new CaptureGraphBuilder2();
                    _sampleGrabber = (ISampleGrabber)new SampleGrabber();
                    _grabberBase = (IBaseFilter)_sampleGrabber;

                    _captureGraph.SetFiltergraph(_graph);

                    // 2. Configura o SampleGrabber para o formato de imagem desejado
                    var mediaType = new AMMediaType
                    {
                        majorType = MediaType.Video,
                        subType = MediaSubType.RGB24,
                        formatType = FormatType.VideoInfo
                    };
                    int hr = _sampleGrabber.SetMediaType(mediaType);
                    DsError.ThrowExceptionForHR(hr);
                    DsUtils.FreeAMMediaType(mediaType);

                    // 3. Adiciona os filtros ao grafo
                    _graph.AddFilter(_cameraFilter, "Camera");
                    _graph.AddFilter(_grabberBase, "SampleGrabber");

                    // 4. Constrói o fluxo de renderização
                    hr = _captureGraph.RenderStream(PinCategory.Capture, MediaType.Video, _cameraFilter, null, _grabberBase);
                    DsError.ThrowExceptionForHR(hr);

                    // 5. Prepara o SampleGrabber para a captura
                    _sampleGrabber.SetBufferSamples(true);
                    _sampleGrabber.SetOneShot(true); // Captura apenas um quadro por Run()

                    _callback = new SampleGrabberCallback();
                    _sampleGrabber.SetCallback(_callback, 1); // 1 = Usa o método BufferCB

                    _mediaControl = _graph as IMediaControl;
                }
                catch
                {
                    // Se qualquer parte da configuração falhar, libera o que já foi criado.
                    Dispose();
                    throw; // Re-lança a exceção original
                }
            }

            public Bitmap CaptureFrame()
            {
                if (_disposed)
                    throw new ObjectDisposedException(nameof(FrameCaptureSession));

                // Inicia o grafo para ESTA captura específica
                _mediaControl.Run();

                // Espera pelo quadro disponível
                if (!_callback.WaitForNewFrame(5000))
                    throw new TimeoutException("Tempo limite excedido ao capturar a imagem.");

                // Para o grafo IMEDIATAMENTE após a captura
                _mediaControl.Stop();

                // Processa os dados do quadro capturado
                var actualMediaType = new AMMediaType();
                _sampleGrabber.GetConnectedMediaType(actualMediaType);

                var videoInfo = (VideoInfoHeader)Marshal.PtrToStructure(actualMediaType.formatPtr, typeof(VideoInfoHeader));
                int width = videoInfo.BmiHeader.Width;
                int height = videoInfo.BmiHeader.Height;
                int stride = width * 3;
                byte[] rawData = _callback.CapturedBuffer;

                if (rawData == null || rawData.Length < stride * height)
                    throw new InvalidOperationException("O buffer de imagem capturado é inválido ou incompleto.");

                // Cria o Bitmap a partir dos dados brutos
                var capturedBitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                var bmpData = capturedBitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, capturedBitmap.PixelFormat);

                // A imagem do DirectShow geralmente vem de "baixo para cima", então invertemos na cópia.
                for (int y = 0; y < height; y++)
                {
                    int sourceIndex = y * stride;
                    nint destPtr = bmpData.Scan0 + (height - 1 - y) * bmpData.Stride;
                    Marshal.Copy(rawData, sourceIndex, destPtr, stride);
                }

                capturedBitmap.UnlockBits(bmpData);
                DsUtils.FreeAMMediaType(actualMediaType);

                return capturedBitmap;
            }
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!_disposed)
                {
                    // Libera recursos COM não gerenciados
                    if (_mediaControl != null)
                    {
                        _mediaControl.Stop();
                        Marshal.ReleaseComObject(_mediaControl);
                        _mediaControl = null;
                    }
                    if (_sampleGrabber != null)
                    {
                        Marshal.ReleaseComObject(_sampleGrabber);
                        _sampleGrabber = null;
                    }
                    if (_grabberBase != null)
                    {
                        Marshal.ReleaseComObject(_grabberBase);
                        _grabberBase = null;
                    }
                    if (_captureGraph != null)
                    {
                        Marshal.ReleaseComObject(_captureGraph);
                        _captureGraph = null;
                    }
                    if (_graph != null)
                    {
                        Marshal.ReleaseComObject(_graph);
                        _graph = null;
                    }
                    _disposed = true;
                }
            }

            private class SampleGrabberCallback : ISampleGrabberCB
            {
                private readonly ManualResetEvent _newFrameEvent = new ManualResetEvent(false);
                public byte[] CapturedBuffer { get; private set; }

                public int BufferCB(double sampleTime, nint pBuffer, int bufferLen)
                {
                    if (CapturedBuffer == null || CapturedBuffer.Length < bufferLen)
                    {
                        CapturedBuffer = new byte[bufferLen];
                    }
                    Marshal.Copy(pBuffer, CapturedBuffer, 0, bufferLen);
                    _newFrameEvent.Set();
                    return 0;
                }

                public int SampleCB(double sampleTime, IMediaSample pSample)
                {
                    if (pSample != null)
                    {
                        Marshal.ReleaseComObject(pSample);
                    }
                    return 0;
                }

                public bool WaitForNewFrame(int timeoutMilliseconds)
                {
                    _newFrameEvent.Reset();
                    return _newFrameEvent.WaitOne(timeoutMilliseconds);
                }
            }
        }

        private Bitmap ResizeAndCenterBitmap(Bitmap original, int canvasSize = 64)
        {
            if (original == null)
                throw new ArgumentNullException(nameof(original));

            float scale = Math.Min((float)canvasSize / original.Width, (float)canvasSize / original.Height);
            int newWidth = (int)(original.Width * scale);
            int newHeight = (int)(original.Height * scale);

            var finalImage = new Bitmap(canvasSize, canvasSize, PixelFormat.Format32bppArgb);
            using (var g = Graphics.FromImage(finalImage))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.Clear(Color.Transparent);
                int offsetX = (canvasSize - newWidth) / 2;
                int offsetY = (canvasSize - newHeight) / 2;
                g.DrawImage(original, new Rectangle(offsetX, offsetY, newWidth, newHeight));
            }

            original.Dispose(); // Libera o bitmap original
            return finalImage;
        }

        #endregion

        internal class NativeMethods
        {
            [DllImport("oleaut32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]

            internal static extern int OleCreatePropertyFrame(
            nint hwndOwner, int x, int y, [MarshalAs(UnmanagedType.LPWStr)] string lpszCaption,
            int cObjects, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] nint[] ppUnk,
            int cPages, nint pPageClsID, int lcid, int dwReserved, nint pvReserved);
        }
    }
}