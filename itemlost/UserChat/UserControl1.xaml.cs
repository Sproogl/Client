using System.Windows.Controls;
using System;
using System.Windows;

namespace UserChat
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class Userchat : UserControl
    {
        private string Nick;
        public uint ID;
        public Userchat()
        {
            InitializeComponent();
            ID = 322;
        }
        public Userchat(string name)
        {
            InitializeComponent();
            FirstName.Text = name;
            ID = 322;
        }

        public void Copy(Userchat copy)
        {
            FirstName.Text = copy.FirstName.Text;
            MessageList.Text = copy.MessageList.Text;
            Email = copy.Email;
            Avatar = copy.Avatar;
            ID = copy.ID;
        }
        public void setText(string text)
        {
            FirstName.Text = text;
        }

        public string getNewMessage()
        {
            return NewmessageBox.Text;
        }

        public void clearNewMessageBox()
        {
            NewmessageBox.Clear();
        }

        public void AddMessagetoMessageList(string userNick, string message)
        {
            
            MessageList.Text += Environment.NewLine+ userNick + " : " + message;
            
        }

        public event EventHandler<ChatItemcontrolArgs> ClickCall; // объявляем событие
        public event EventHandler<ChatItemcontrolArgs> ClickMessage; // объявляем событие
        public event EventHandler<ChatItemcontrolArgs> ClickSend; // объявляем событие
        public event EventHandler<ChatItemcontrolArgs> ClickSmile; // объявляем событие

        protected void RaiseCallClick(string name)
        {
            if (ClickCall != null)
            {
                ClickCall(this, new ChatItemcontrolArgs(name));
            }
        } // Метод который вызывает событие

        protected void RaiseMessageClick(string name) // Метод который вызывает событие
        {
            if (ClickMessage != null)
            {
                ClickMessage(this, new ChatItemcontrolArgs(name));
            }
        }

        protected void RaiseSendClick(string name)
        {
            if (ClickSend != null)
            {
                ClickSend(this, new ChatItemcontrolArgs(name));
            }
        } // Метод который вызывает событие

        protected void RaiseSmileClick(string name)
        {
            if (ClickSmile != null)
            {
                ClickSmile(this, new ChatItemcontrolArgs(name));
            }
        } // Метод который вызывает событие

        private void PART_call_Click(object sender, RoutedEventArgs e)
            // Стандартное событие нажатия на кнопку, которое вызывает  RaiseCallClick();
        {
            RaiseCallClick(FirstName.Text);
        }

        private void PART_message_Click(object sender, RoutedEventArgs e)
            // Стандартное событие нажатия на кнопку, которое вызывает RaiseMessageClick();
        {
            RaiseMessageClick(FirstName.Text);
        }


        private void Bsend_OnClick(object sender, RoutedEventArgs e)
        {
            RaiseSendClick(FirstName.Text);
        }

        private void Bsmile_OnClick(object sender, RoutedEventArgs e)
        {
            RaiseSmileClick(FirstName.Text);
        }

      
    }

    public class ChatItemcontrolArgs : EventArgs // класс которые мы передаём в качестве параметра события
    {
        public ChatItemcontrolArgs(string name)
        {
            this.name = name;
        }

        public string name { private set; get; }
    }
}

