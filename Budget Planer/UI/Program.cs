using System;
using System.Windows.Forms;

namespace UI.WinForms
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            var testdata = new TestData();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new Form1(testdata.Project, testdata.Services));
        }
    }
}