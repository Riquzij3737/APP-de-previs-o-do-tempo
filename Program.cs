using System;
using System.Windows.Forms;

namespace APDT
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new Form2()); // Lan�ando o Form2
        }
    }
}
