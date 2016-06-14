using System;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using System.IO;
using System.Windows.Input;
using System.Windows.Threading;

namespace sp
{

    public partial class login : Window , ILoginWindow
    {
        Client.Xml configXml;
        private System.Windows.Threading.DispatcherTimer timer;
        double _AnimationSlide_height;
        double _AnimationSlide_x;
        uint ID;
        int port;
        string ip;
        Socket socketRecv , socketSend;
        public login()
        {
            InitializeComponent();
            ID = 0;
            configXml = new Client.Xml("config.xml");
            _AnimationSlide_x = -21;
            _AnimationSlide_height = 24;
            Window.Height = 24;
            port = configXml.getPortfromXML();
            ip = configXml.getIpfromXML();
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 30);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Window.Height = _AnimationSlide_height;
            _AnimationSlide_height = -(_AnimationSlide_x*_AnimationSlide_x)+500;
            _AnimationSlide_x++;
            if(_AnimationSlide_x>0)
            {
                timer.Stop();
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            registrationOnLink();
        }

       public void registrationOnLink()
        {
            socketRecv = new Socket(SocketType.Stream, ProtocolType.Tcp);
            byte[] recvmessange = new byte[520];
            sendMessage(loginbox.Text + "/" + passwordbox.Password + "\\" , 0);
            try
            {
                socketRecv.Receive(recvmessange);
            }
            catch (SocketException e)
            {
                socketRecv.Close();
                return;
            }
            switch (recvmessange[0])
            {
                case (100):
                    {
                        ID = getIDfromByte(recvmessange);
                        if (ID != 0)
                        {
                            Dispatcher.BeginInvoke(new Action(() => CreateMainWindow(ID)));
                            socketRecv.Close();
                            return;
                        }
                        break;
                    }
                default: break;
            }
            socketRecv.Close();
            return;
        }

        void CreateMainWindow(uint ID)
        {
            MainWindow window = new MainWindow(ID,loginbox.Text);
            window.Show();
            this.Close();
        }

        private void title_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void BClose_Click(object sender, RoutedEventArgs e)
        {
            if (socketRecv != null)
            {
                socketRecv.Close();
            }
            this.Close(); 
        }

        public void sendMessage(string message, uint ID_DEST)
        {
            System.Threading.Thread.Sleep(1000);
            Client.WATF watfMessage = new Client.WATF(Client.MegType.REGISTRATION);
            watfMessage.info.MSG_LEN = message.Length;
            watfMessage.MSG = message;
            watfMessage.info.ID_DEST = ID_DEST;
            watfMessage.info.ID_SRC = this.ID;          
            try
            {
                socketRecv.Connect(ip, port);
                socketRecv.Send(watfMessage.StructToBytes());

            }
            catch (SocketException e)
            {
                // ERROR sending message
            }
        }

        public uint getIDfromByte(byte[] arr)
        {
            Client.WATF message = new Client.WATF();
            uint id;
            message.BytesToStruct(arr);
            id = message.info.ID_SRC;
            return id;
        }
    }
}
