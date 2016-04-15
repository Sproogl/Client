using System;
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
        List<UserControls> Auser = new List<UserControls>();
        
        private bool online;
        private System.Windows.Threading.DispatcherTimer timer;
        private int MerginRight;
       
        public MainWindow()
        {
            online = false;
            MerginRight = 0;
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval= new TimeSpan(0,0,0,0,10);
            timer.Tick += Timer_Tick;
            string[] Anick = new string[10];
            UserControl1[] AUser = new UserControl1[10];
            Userchat[] AChat = new Userchat[10];
            
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
            SetConfig();
            listenMesg();

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (MerginRight >= 0)
            {
                MasterPanel.Margin = new Thickness(MerginRight, 20, 0,0);
                MerginRight -= 20;
            }
            else
            {
                
                timer.Stop();    
                
            }
        }

        private void AddUser(string name)
        {
            int index = Auser.Count;
            var newUser = new UserControl1(name, index);
            var newChat = new Userchat(name);
            newChat.setText(name);
            var newUseritem = new UserControls(newUser,newChat,index);
            newUser.CallClick += UserControl1_OnCallClick;
            newUser.MessageClick += UserControl1_OnMessageClick;
            newUser.ClickAvatar += UserControl1_OnClickAtatar;
            Auser.Add(newUseritem);
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
            MerginRight = 600;
            UserControl1 send = (UserControl1) sender;
            
            Userchat  Userchat = Auser[send.Index].getitemchat();
            MasterPanel.Margin = new Thickness(MerginRight, 20, 0, 0);
            Userchat1.Copy(Userchat);
            timer.Start();

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



        }

        private void Userchat1_OnClickSend(object sender, ChatItemcontrolArgs e)
        {
            Userchat chat = (Userchat) sender;
            sendMessage(chat.getNewMessage(),chat.ID);
            chat.AddMessagetoMessageList(e.name,chat.getNewMessage());
            chat.clearNewMessageBox();
            
        }
    }

    public class UserControls
    {
        UserControl1 itemlist;
        Userchat itemchat;
        private int index;

        public UserControls(UserControl1 itemList, Userchat itemChat , int ind)
        {
            itemlist = itemList;
            itemchat = itemChat;
            index = ind;
        }

        public UserControl1 getitemlist()
        {
            return itemlist;
        }

        public Userchat getitemchat()
        {
            return itemchat;
        }

        public int getIndex()
        {
            return index;
        }
}
}
