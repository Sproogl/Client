using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfControlLibrary2;

namespace sp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<UserControl1> Auser = new List<UserControl1>();
        public MainWindow()
        {
            string[] Anick = new string[10];
            UserControl1[] AUser = new UserControl1[10];
            
            Anick[0] = "Den";
            Anick[1] = "Pol";
            Anick[2] = "Roma";
            Anick[3] = "Pasha";
            Anick[4] = "Sveta";
            Anick[5] = "Sasha";
            Anick[6] = "Tom";
            Anick[7] = "Lera";
            Anick[8] = "Ann";
            Anick[9] = "Alena";
            InitializeComponent();
            for (int i = 0; i < 9; i++)
            {

                AddUser(Anick[i]);
            }



        }

        private void AddUser(string name)
        {
            int index = Auser.Count;
            var newUser = new UserControl1(name, index);
            newUser.CallClick += UserControl1_OnCallClick;
            newUser.MessageClick += UserControl1_OnMessageClick;
            Auser.Add(newUser);
            ListBox1.Items.Add(newUser);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("slide new panel");
        }

        private void UserControl1_OnCallClick(object sender, UserItemcontrolArgs e)
        {
            UserControl1 control = (UserControl1) sender;
            MessageBox.Show(control.Index.ToString()+"   "+e.name + "  CallClick");
        }
        private void UserControl1_OnMessageClick(object sender, UserItemcontrolArgs e)
        {
            UserControl1 control = (UserControl1)sender;
            MessageBox.Show(control.Index.ToString() +"   "+ e.name + "  MessageClick");
        }

        private void SendName_OnClick(object sender, RoutedEventArgs e)
        {
            if (NewnameBox.Text != null)
            {
                AddUser(NewnameBox.Text);
                NewnameBox.Clear();
            }
        }
    }
}
