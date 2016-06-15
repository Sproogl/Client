using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;
using Itemlist;
using UserChat;
using System.IO;
using SearchFriendPanel;
using FoundItemList;

namespace sp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
      public  List<UserControls> Auser = new List<UserControls>();

        /// <summary>
        /// variables for the animation UserControl
        /// </summary>
        private int _AnimationSlide_MerginRight;
        int _AnimationSlide_X;
        double _AnimationOpasity_X;
        double _AnimationOpasity_opasity;
        bool isdownResize = false;
        bool isdownwindow = false;

        private bool connectStatus;

        private System.Windows.Threading.DispatcherTimer timer;

        Client client;
        public MainWindow(uint id, string login)
        {
            connectStatus = false;
            _AnimationSlide_MerginRight = 0;
            _AnimationSlide_X = -24;
            _AnimationOpasity_X = -1;
            _AnimationOpasity_opasity = 1;
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval= new TimeSpan(0,0,0,0,13);
            timer.Tick += Timer_Tick;
            string[] Anick = new string[10];
            uint[] Aid = new uint[10];

            
            InitializeComponent();
            search_panel.Visibility = Visibility.Hidden;
            client = new Client(this,id,login);
            search_panel.AddFriend += Search_panel_AddFriend;
            search_panel.SearchClick += Search_panel_SearchClick;
             

        }

        private void Search_panel_SearchClick(object sender, UserItemcontrolArgs e)
        {
            client.SendSearchFriend(e.name);
        }

        private void Search_panel_AddFriend(object sender, FoundItemArgs e)
        {
            try {
                UserList.Items.Remove(sender);
                client.SendRequestOnFriend(e.name, e.id);
                search_panel.Visibility = Visibility.Hidden;
            }catch(InvalidOperationException ex)
            {

            }
        }

       

        public void AddrequestInList(string name, uint id)
        {
            FoundItem item = new FoundItem(name, id);
            item.Addclick += Item_Addclick;
            UserList.Items.Add(item);
        }

        public void AddrequestInsearchPanel(string name, uint id)
        {
            search_panel.AddUser(name, id);
        }

        private void Item_Addclick(object sender, FoundItemArgs e)
        {
            
            client.SendAcceptOnFriend(e.name, e.id);
            UserList.Items.Remove(sender);
        }



        /// <summary>
        /// Timer for animation userControl
        /// </summary>
        /// <returns>
        /// </returns>
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_AnimationSlide_X<0)
            {
                MasterPanel_Front.Margin = new Thickness(_AnimationSlide_MerginRight, 20, 0,0);
                _AnimationSlide_MerginRight = _AnimationSlide_X*_AnimationSlide_X;
                MasterPanel_Back.Opacity = _AnimationOpasity_opasity;
                if (_AnimationOpasity_X < 0)
                {
                    _AnimationOpasity_opasity = _AnimationOpasity_X * _AnimationOpasity_X;
                    
                    _AnimationOpasity_X += 0.04;
                }
                _AnimationSlide_X++;
            }
            else
            {
                MasterPanel_Front.Margin = new Thickness(0, 20, 0, 0);
                _AnimationSlide_X = -24;
                _AnimationOpasity_X = -1;
                _AnimationOpasity_opasity = 1;
                timer.Stop();    
                
            }
        }

        public void SetToMYID(string myid)
        {
            MYID.Text = myid;
        }

        public void SetUserConnect(bool st,string name,uint id)
        {
            AddUser(name, id, st);       
        }

        private void AddUser(string name, uint ID , bool online)
        {
            

            int index = Auser.Count;
            for(int i = 0; i <index; i++)
            {
                if (Auser[i].getID() == ID)
                {
                    if(Auser[i].geticatorConnected() == online)
                    {
                        return;
                    }
                    else
                    {
                        Auser[i].setIndicatorConnected(online);
                        return;
                    }
                }
            }

            var newUser = new ItemList(name, index, ID);
            var newChat = new Userchat(name,ID);
            newChat.setText(name);
            var newUseritem = new UserControls(newUser,newChat,index,ID,online);
            newUser.CallClick += UserControl1_OnCallClick;
            newUser.MessageClick += UserControl1_OnMessageClick;
            newUser.ClickAvatar += UserControl1_OnClickAtatar;

            Auser.Add(newUseritem);
            UserList.Items.Add(newUser);
 
        }

        /// <summary>
        /// Save UserChat content in filies
        /// </summary>
        /// <returns>
        /// </returns>
        void savemesgHistory()
        {
            for (int i = 0; i < Auser.Count; i++)
            {
                Userchat chat = Auser[i].getitemchat();
                try
                {
                    StreamWriter WFile = new StreamWriter("data/" + chat.getID() + ".mh", false);
                    WFile.Write(chat.getMsgHistory());
                    WFile.Close();
                }
                catch (Exception e)
                {

                }
                
            }
        }
        private void UserControl1_OnCallClick(object sender, UserItemcontrolArgs e)
        {
            ItemList control = (ItemList) sender;
            MessageBox.Show(control.Index.ToString()+"   "+e.name + "  CallClick");
        }

        private void UserControl1_OnMessageClick(object sender, UserItemcontrolArgs e)
        {
            _AnimationSlide_MerginRight = 600;
            ItemList send = (ItemList) sender;
            if(send.getIndicatornewMesg())
            {
                send.setIndictorMessage(false);
            }
            Userchat  Userchat = Auser[send.Index].getitemchat();
            Userchat.setID(send.getID());
            if (Userchat_Front.getID() != 0)
            {
                MasterPanel_Back.Margin = new Thickness(0, 20, 0, 0);
                Userchat_Back.Copy(Userchat_Front);
                MasterPanel_Back.Opacity = 1;
                
            }
            MasterPanel_Front.Margin = new Thickness(_AnimationSlide_MerginRight, 20, 0, 0);
            Userchat_Front.Copy(Userchat);
            timer.Start();

        }

        private void UserControl1_OnClickAtatar(object sender, UserItemcontrolArgs e)
        {
            ItemList control = (ItemList)sender;
            MessageBox.Show(control.Index.ToString() + "   " + e.name + "  ClickAvatar");
        }

        private void SendName_OnClick(object sender, RoutedEventArgs e)
        { }

        private void UIElement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           this.DragMove();
        }

        private void BClose_OnClick(object sender, RoutedEventArgs e)
        {
            client.disconnectToserver();
            SetLoginStatus(false);
            savemesgHistory();
            this.Close();
        }

        private void AddfrendButton_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if(search_panel.Visibility == Visibility.Hidden)
            search_panel.Visibility = Visibility.Visible;
            else
            {
                search_panel.Visibility = Visibility.Hidden;
            }
        }

        public void SetLoginStatus(bool status)
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
        {}

        private void Userchat_Front_OnClickSend(object sender, ChatItemcontrolArgs e)
        {
            if (LoginStatus.Text == "Online")
            {
                Userchat chat = (Userchat)sender;
                client.sendMessage(chat.getNewMessage(), chat.getID());
                addMessageToAuser(chat.getID(),client.getId().ToString(), chat.getNewMessage());          
                chat.clearNewMessageBox();   
            }  
        }


        private void Bdisconnect_Click(object sender, RoutedEventArgs e)
        {
            client.disconnectToserver();
            SetLoginStatus(false);
            savemesgHistory();
        }


        public void addMessageToAuser(uint ID,string Unick,string mesg)
        {
            for (int i = 0; i < Auser.Count; i++)
            {
               if( Auser[i].getID()== ID)
                {
                    Auser[i].addMessage(Unick, mesg);
                    if (Userchat_Front.getID() == ID)
                    {
                        Userchat_Front.AddMessagetoMessageList(Unick,mesg);
                    }
                    else
                    {
                        Auser[i].setIndicatorNewMesg(true);
                    }
                }    
            }
        }

        private void Userchat_Front_EnterSend(object sender, ChatItemcontrolArgs e)
        {
            if (LoginStatus.Text == "Online")
            {
                Userchat chat = (Userchat)sender;
                client.sendMessage(chat.getNewMessage(), chat.getID());
                addMessageToAuser(chat.getID(), client.getId().ToString(), chat.getNewMessage());
                chat.clearNewMessageBox();
            }
        }

        private void borderResizer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isdownResize = true;
            isdownwindow = true;
        }

        private void borderResizer_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed && isdownResize && isdownwindow)
            {
                Point y = e.GetPosition(this);
                double newy = y.Y+4;
                this.Height = newy;
            }
        }

        private void borderResizer_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isdownResize = false;
        }

        private void Programm_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isdownwindow = true;
        }
    }
}
