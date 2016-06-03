using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.IO;

namespace Itemlist
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class ItemList : UserControl
    {
        public string status;
        private uint ID;
        public int Index;
        private bool Indicator;
        private string MsgHistory;
        public event EventHandler<UserItemcontrolArgs> CallClick;     // объявляем событие
        public event EventHandler<UserItemcontrolArgs> MessageClick;  // объявляем событие
        public event EventHandler<UserItemcontrolArgs> ClickAvatar;   // объявляем событие
        public ItemList(string name, int index, uint ID)
        {
            InitializeComponent();
            status = "12345";
            Nick.Text = name;
            this.ID = ID;
            this.Index = index;
            Indicator = false;
            

        }
        public ItemList()
        {
            InitializeComponent();
            status = "12345";
            Index = 0;
            ID = 0;
            Indicator = false;


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
        }              // Метод который вызывает событие

        protected void RaiseMessageClick(string name)              // Метод который вызывает событие
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
                GridPanel.Background = System.Windows.Media.Brushes.White;
            }
            else
            {
                GridPanel.Background = System.Windows.Media.Brushes.Silver;
            }
        }

        protected void RaiseAvatarClick(string name)
        {
            if (ClickAvatar != null)
            {
                ClickAvatar(this, new UserItemcontrolArgs(name));
            }
        }           // Метод который вызывает событие

        private void PART_call_Click(object sender, RoutedEventArgs e)     // Стандартное событие нажатия на кнопку, которое вызывает  RaiseCallClick();
        {
           RaiseCallClick(Nick.Text);
        } 

        private void PART_message_Click(object sender, RoutedEventArgs e)  // Стандартное событие нажатия на кнопку, которое вызывает RaiseMessageClick();
        {
            RaiseMessageClick(Nick.Text);
        }

        private void Avatar_OnMouseDown(object sender, MouseButtonEventArgs e)    // Стандартное событие нажатия на на картинку, которое вызывает RaiseAvatarClick();
        {
         RaiseAvatarClick(Nick.Text);  
        }

        private void GridPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RaiseMessageClick(Nick.Text);
        }
    }

    public class UserItemcontrolArgs : EventArgs   // класс которые мы передаём в качестве параметра события
    {
        public UserItemcontrolArgs(string name)
        {
            this.name = name;
        }

        public  string name { private set; get; }
    }
}
