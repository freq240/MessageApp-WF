using System;
using System.Threading;
using System.Windows.Forms;

namespace Message
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Global catch
            Application.ThreadException += new ThreadExceptionEventHandler(GlobalCatch);

            // Run Form
            Application.Run(new Form());
        }

        private static void GlobalCatch(object sender, ThreadExceptionEventArgs e)
        {
            // mbox with full info about exception
            MessageBox.Show(e.Exception.ToString());
        }
    }
}
