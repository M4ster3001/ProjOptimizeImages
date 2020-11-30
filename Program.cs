using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageTools
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Session.QtdeNucleos = Convert.ToInt32(Math.Round(Environment.ProcessorCount * 0.8, MidpointRounding.AwayFromZero));

            Application.ThreadException +=
                    new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);

            // Add handler to handle the exception raised by additional threads
            AppDomain.CurrentDomain.UnhandledException +=
            new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        static void Application_ThreadException
        (object sender, System.Threading.ThreadExceptionEventArgs e)
        {// All exceptions thrown by the main thread are handled over this method

            //var message =
            //String.Format(
            //    "Sorry, something went wrong.\r\n" + "{0}\r\n" + "{1}\r\n" + "please contact support.",
            //    e.Exception.Message, e.Exception.StackTrace);
            //MessageBox.Show(message, @"Unexpected error");

            HandleException(e.Exception);
        }

        [Obsolete]
        static void CurrentDomain_UnhandledException
            (object sender, UnhandledExceptionEventArgs e)
        {// All exceptions thrown by additional threads are handled in this method

            //var message =
            //String.Format(
            //    "Sorry, something went wrong.\r\n" + "{0}\r\n" + "{1}\r\n" + "please contact support.",
            //    ((Exception)e.ExceptionObject).Message, ((Exception)e.ExceptionObject).StackTrace);
            //MessageBox.Show(message, @"Unexpected error");

            // Suspend the current thread for now to stop the exception from throwing.
            HandleException(((Exception)e.ExceptionObject));

            Thread.CurrentThread.Suspend();
        }

        internal static void HandleException(Exception ex)
        {
            string LF = Environment.NewLine + Environment.NewLine;
            string title = $"Ops, algo de errado aconteceu às {DateTime.Now}";
            string infos = $"Copiei essa mensagem \n\r\n\r" +
                           $"Message : {LF}{ex.Message}{LF}" +
                           $"Source : {LF}{ex.Source}{LF}" +
                           $"Stack : {LF}{ex.StackTrace}{LF}" +
                           $"InnerException : {ex.InnerException}";

           DialogResult result = MessageBox.Show(infos, title, MessageBoxButtons.OK, MessageBoxIcon.Error); // Do logging of exception details
        }
    }
}
