using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace WpfControlLibrary2
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public string status;
        public int Index;
        public event EventHandler<UserItemcontrolArgs> CallClick;     // объявляем событие
        public event EventHandler<UserItemcontrolArgs> MessageClick;  // объявляем событие
        public event EventHandler<UserItemcontrolArgs> ClickAvatar;   // объявляем событие
        public UserControl1(string name, int index)
        {
            InitializeComponent();
            status = "12345";
            Nick.Text = name;
            this.Index = index;

        }
        public UserControl1()
        {
            InitializeComponent();
            status = "12345";
            Index = 0;


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
