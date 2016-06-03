using System;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using System.IO;
using System.Windows.Input;

namespace sp
{


    public partial class login : Window
    {
        
        private System.Windows.Threading.DispatcherTimer timer;
        double height;
        double x;
        uint ID;
        int port;
        string ip;
        Socket socketRecv;
        public login()
        {
            InitializeComponent();
            ID = 0;
            x = -22.36;
            height = 25;
            Window.Height = 25;
            port = getPortfromXML();
            ip = getIpfromXML();
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 30);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Window.Height = height;
            height = -x*x+500;
            x++;
            if(x>0)
            {
                timer.Stop();
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            socketRecv = new Socket(SocketType.Stream, ProtocolType.Tcp);

            byte[] recvmessange = new byte[520];
            
            sendMessage(loginbox.Text + " " + passwordbox.Password, 0);
            try
            {
                socketRecv.Receive(recvmessange);
            }
            catch (SocketException ex)
            {
                return;
            }
            switch (recvmessange[0])
            {
                case (100):
                    {
                        ID = getIDfromByte(recvmessange);
                        if (ID != 0)
                        {
                            MainWindow window = new MainWindow(ID);
                            window.Show();
                            socketRecv.Close();
                            this.Close();
                        }
                        break;
                    }
                default: break;   
            }
            socketRecv.Close();
            return;
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
        public string getIpfromXML()
        {
            string fileName = "config.xml";
            string ip;
            XDocument docin = XDocument.Load(fileName);
            XElement element = docin.Root.Element("ip");
            try
            {
                ip = element.Value;
            }
            catch (NullReferenceException e)
            {
                return null;
            }
            return ip;
        }
        public int getPortfromXML()
        {
            string fileName = "config.xml";
            int port = 12345;
            XDocument docin = XDocument.Load(fileName);
            XElement element = docin.Root.Element("port");
            try
            {
                port = Convert.ToInt32(element.Value);

            }
            catch (NullReferenceException e)
            {
                return 0;
            }
            return port;
        }

        public void sendMessage(string message, uint ID_DEST)
        {
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
                // Console.WriteLine("Eror send message");
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
