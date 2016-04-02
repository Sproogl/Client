using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;
using WpfControlLibrary2;
using UserChat;
namespace sp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<UserControl1> Auser = new List<UserControl1>();
        private bool online;
        public MainWindow()
        {
            online = false;
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


            SetLoginStatus(true);
        }

        private void AddUser(string name)
        {
            int index = Auser.Count;
            var newUser = new UserControl1(name, index);
            newUser.CallClick += UserControl1_OnCallClick;
            newUser.MessageClick += UserControl1_OnMessageClick;
            newUser.ClickAvatar += UserControl1_OnClickAtatar;
            Auser.Add(newUser);
            UserList.Items.Add(newUser);
            
            
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

        private void UserControl1_OnClickAtatar(object sender, UserItemcontrolArgs e)
        {
            UserControl1 control = (UserControl1)sender;
            MessageBox.Show(control.Index.ToString() + "   " + e.name + "  ClickAvatar");
        }

        private void SendName_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void UIElement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           this.DragMove();
        }

        private void BClose_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddfrendButton_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            AddUser("Test");
        }

        private void SetLoginStatus(bool status)
        {
            if (status)
            {
                LoginStatus.Text = "Online";
                LoginStatus.Background = Brushes.LightGreen;

            }
            else
            {

                LoginStatus.Text = "Offline";
                LoginStatus.Background = Brushes.DarkGray;

            }
        }

        private void LoginStatus_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            SetLoginStatus(online);
            if (online)
            {
                online = false;
            }
            else
            {

                online = true;

            }
        }
    }
}
