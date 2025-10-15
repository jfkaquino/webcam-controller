using System;
using System.Windows.Forms;
using WebcamController.Controllers;
using WebcamController.Forms;
using WebcamController.Services;

namespace WebcamController
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Mutex mutex = new Mutex(true, Application.ProductName, out bool isNewInstance); // Inicialize aqui

            if (!isNewInstance)
            {
                return;
            }

            ApplicationConfiguration.Initialize();

            var cameraService = new CameraService();
            var hotkeyService = new HotkeyService();
            var windowsService = new WindowsService();

            var cameraController = new CameraController(cameraService);
            var presetController = new PresetController(cameraController, hotkeyService);

            new AppDbContext().Database.EnsureCreated();

            Application.Run(
                new MainForm(
                cameraService,
                hotkeyService,
                windowsService,
                presetController,
                cameraController
                )
            );
            GC.KeepAlive(mutex);
        }
    }
}