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

            Application.ThreadException += ExceptionCapture;
            AppDomain.CurrentDomain.UnhandledException += ExceptionCapture;

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

        private static void ExceptionCapture(object sender, UnhandledExceptionEventArgs e) => ExceptionCapture((Exception)e.ExceptionObject);
        private static void ExceptionCapture(object sender, ThreadExceptionEventArgs e) => ExceptionCapture(e.Exception);

        private static void ExceptionCapture(Exception ex)
        {

            TaskDialogCommandLinkButton restartButton = new TaskDialogCommandLinkButton("Reiniciar programa");
            TaskDialogCommandLinkButton exitButton = new TaskDialogCommandLinkButton("Encerrar programa");
            TaskDialogPage page = new TaskDialogPage()
            {
                Caption = Application.ProductName,
                Heading = "Ocorreu um erro inesperado",
                Text = ex.InnerException != null ? ex.InnerException.Message : ex.Message,
                Icon = TaskDialogIcon.Error,
                AllowCancel = true,
                Expander = new TaskDialogExpander()
                {
                    Text = "<a href=\"copy\">Copiar detalhes</a>\n\n" + ex.ToString(),
                    Position = TaskDialogExpanderPosition.AfterFootnote,
                },
                EnableLinks = true,
                Buttons =
                {
                    exitButton,
                    restartButton,
                    TaskDialogButton.Ignore,
                },
            };

            page.LinkClicked += (s, e) =>
            {
                if (e.LinkHref == "copy")
                {
                    Clipboard.SetText(ex.ToString());
                }
            };

            var result = TaskDialog.ShowDialog(page);

            if (result == restartButton)
            {
                Application.Restart();
            }
            else if (result == exitButton)
            {
                Application.Exit();
            }
        }
    }
}