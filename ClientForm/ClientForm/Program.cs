using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientForm
{
    static class Program
    {
       
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool status = false;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 mainwindow = new Form1();

            Application.Run(new login(mainwindow));
                        if(mainwindow.logIn)
                        {
                            Application.Run(new load());
                            Application.Run(new Form1());
                        }




        }
    }
}
