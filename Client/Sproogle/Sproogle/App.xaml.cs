using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace sp
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App1 : Application
    {
        public uint ID;
        App1()
        {
            InitializeComponent();
        }
        public void StartMaintWindow()
        {
            this.StartupUri = new System.Uri("Sproogle/Window/MainWindow/MainWindow.xaml", System.UriKind.Relative);
        }
        private void InitializeComponent()
        {
            
            this.StartupUri = new System.Uri("Sproogle\\Window\\LoginWondow\\login.xaml", System.UriKind.Relative);
            
        }
    }
}
