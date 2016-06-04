using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;
using Itemlist;
using UserChat;
using System.IO;

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


        private bool connectStatus;


        private System.Windows.Threading.DispatcherTimer timer;

        Client client;
        public MainWindow(uint id)
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
            Itemlist.ItemList[] AUser = new Itemlist.ItemList[10];
            Userchat[] AChat = new Userchat[10];
            
            Anick[0] = "1488";
            Aid[0] = 1488;
            Anick[1] = "322";
            Aid[1] = 322;
            Anick[2] = "Roma";
            Aid[2] = 333;
            Anick[3] = "Pasha";
            Aid[3] = 444;
            Anick[4] = "Sveta";
            Aid[4] = 555;
            Anick[5] = "Sasha";
            Aid[5] = 666;
            Anick[6] = "Tom";
            Aid[6] = 777;
            Anick[7] = "Lera";
            Aid[7] = 888;
            Anick[8] = "Ann";
            Aid[8] = 999;
            Anick[9] = "Alena";
            Aid[9] = 000;
            InitializeComponent();
            client = new Client(this);
            
            for (int i = 0; i < 9; i++)
            {
                if (Aid[i] != client.getId())
                {
                    AddUser(Anick[i], Aid[i]);
                }
            }
           
            

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

        public void SetUserConnect(bool st,uint id)
        {         
                for (int i = 0; i < Auser.Count; i++)
                {
                    if (Auser[i].getID() == id)
                    {
                        Auser[i].setIndicatorConnected(st); 
                    }
                }         
        }

        private void AddUser(string name, uint ID)
        {
            int index = Auser.Count;
            var newUser = new ItemList(name, index, ID);
            var newChat = new Userchat(name,ID);
            newChat.setText(name);
            var newUseritem = new UserControls(newUser,newChat,index,ID);
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
            AddUser("Test",(uint)0);
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
    }
}
