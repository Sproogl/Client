using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.IO;

namespace Itemlist
{

    public partial class ItemList : UserControl
    {
        public string status;
        private uint ID;
        public int Index;
        bool online;
        private bool Indicator;
        private string MsgHistory;
        public event EventHandler<UserItemcontrolArgs> CallClick;   
        public event EventHandler<UserItemcontrolArgs> MessageClick; 
        public event EventHandler<UserItemcontrolArgs> ClickAvatar;  
        public ItemList(string name, int index, uint ID)
        {
            InitializeComponent();
            status = "12345";
            int size = name.Length;
            int lengtnewNick = 0;

            while (size != 0)
            {
                if (name[size - 1] != '\n' && name[size - 1] != '\0')
                    lengtnewNick++;

                size--;
            }
            string newnick = name.Substring(0, lengtnewNick) + "\n";

            Nick.Text = newnick;
            this.ID = ID;
            this.Index = index;
            Indicator = false;
            online = false;
            

        }
        public ItemList()
        {
            InitializeComponent();
            status = "12345";
            Index = 0;
            ID = 0;
            Indicator = false;


        }
        public string getNick()
        {
            return Nick.Text;
        }

        public bool getIndicatornewMesg()
        {
            return Indicator;
        }
        public uint getID()
        {
            return ID;
        }
        public void setNick(string nick)
        {
            Nick.Text = nick;
        }

        public void setAvatar(BitmapImage avatar)
        {
            Avatar.Source = avatar;
        }

        protected void RaiseCallClick(string name)
        {
            if (CallClick != null)
            {
                CallClick(this, new UserItemcontrolArgs(name));
            }
        }  

        protected void RaiseMessageClick(string name)        
        {
            if (MessageClick != null)
            {
                MessageClick(this, new UserItemcontrolArgs(name));
            }
        }


         public void setIndictorMessage(bool ind)
        {
            Indicator = ind;
            if(ind)
            {
                indicatorMsg.Visibility = Visibility.Visible;
            }
            else
            {
                indicatorMsg.Visibility = Visibility.Hidden;
            }
        }

        public void setConnected(bool st)
        {
            if (st)
            {
                online = true;
                GridPanel.Background = System.Windows.Media.Brushes.White;
            }
            else
            {
                online = false;
                GridPanel.Background = System.Windows.Media.Brushes.Silver;
            }
        }


        public bool isonline()
        {
            return online;
        }

        protected void RaiseAvatarClick(string name)
        {
            if (ClickAvatar != null)
            {
                ClickAvatar(this, new UserItemcontrolArgs(name));
            }
        }    

        private void PART_call_Click(object sender, RoutedEventArgs e)    
        {
           RaiseCallClick(Nick.Text);
        } 

        private void PART_message_Click(object sender, RoutedEventArgs e) 
        {
            RaiseMessageClick(Nick.Text);
        }

        private void Avatar_OnMouseDown(object sender, MouseButtonEventArgs e)   
        {
         RaiseAvatarClick(Nick.Text);  
        }

        private void GridPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RaiseMessageClick(Nick.Text);
        }
    }

    public class UserItemcontrolArgs : EventArgs  
    {
        public UserItemcontrolArgs(string name)
        {
            this.name = name;
        }

        public  string name { private set; get; }
    }
}
