using System;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using System.IO;
using System.Threading;
using System.Windows.Threading;

namespace sp
{
    public partial class Client
    {

        public class ActionClient : IActionClient
        {

            public MainWindow window;
            private Xml xml;
            public Dispatcher Dispatcher = Dispatcher.CurrentDispatcher;
            Socket socketSend, socketRecv;
            string ip { get; set; }
            int port;
            uint ID;


            public ActionClient(MainWindow window)
            {
                xml = new Xml("config.xml");
                this.window = window;
                SetConfig();
                listenMesg();
            }
            public void SetConfig()
            {
                this.ip = xml.getIpfromXML();
                this.port = xml.getPortfromXML();
                ID = xml.getIdfromXML();

            }
            public uint getId()
            {
                return ID;
            }
            public void SetConnectedUser()
            {
                for (int i = 0; i < window.Auser.Count; i++)
                {

                    System.Threading.Thread.Sleep(150);
                    WATF message = new WATF(MegType.USERONLINE);
                    message.info.MSG_LEN = 5;
                    message.MSG = "none";
                    message.info.ID_DEST = window.Auser[i].getID();
                    message.info.ID_SRC = this.ID;
                    socketSend = new Socket(SocketType.Stream, ProtocolType.Tcp);
                    try
                    {
                        socketSend.Connect(ip, port);
                        socketSend.Send(message.StructToBytes());
                    }
                    catch (SocketException e)
                    {
                        // Console.WriteLine("Eror send message");
                    }
                    socketSend.Close();
                }
            }
            public void disconnectFromserver()
            {
                WATF message = new WATF(MegType.DISCONNECT);
                message.info.MSG_LEN = 0;
                message.MSG = "none";
                message.info.ID_DEST = 0;
                message.info.ID_SRC = this.ID;
                socketSend = new Socket(SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    socketSend.Connect(ip, port);
                    socketSend.Send(message.StructToBytes());
                }
                catch (SocketException e)
                {
                    // Console.WriteLine("Eror send message");
                }
                socketSend.Close();
            }
            public void sendMessage(string message, uint ID_DEST)
            {
                WATF watfMessage = new WATF(MegType.MESSANGE);
                watfMessage.info.MSG_LEN = message.Length;
                watfMessage.MSG = message;
                watfMessage.info.ID_DEST = ID_DEST;
                watfMessage.info.ID_SRC = this.ID;
                socketSend = new Socket(SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    socketSend.Connect(ip, port);
                    socketSend.Send(watfMessage.StructToBytes());
                }
                catch (SocketException e)
                {
                    // Console.WriteLine("Eror send message");
                }
                socketSend.Close();
            }

            public void listenMesg()
            {
                Task t = Task.Run(() => listenMesgThread());
            }
            public void listenMesgThread() // подключаем слушающий сокет
            {
                WATF watfMessage = new WATF(100);
                WATF newmessage = new WATF();
                socketRecv = new Socket(SocketType.Stream, ProtocolType.Tcp);
                if (ID == 0)  // проверяем, есть ли у нас ID
                {
                    watfMessage = new WATF(MegType.REGISTRATION);
                    watfMessage.MSG = "registration";
                }
                else  // если есть то отправляем его на сервер
                {
                    this.Dispatcher.BeginInvoke(new Action(() => window.SetToMYID(ID.ToString())));
                    watfMessage = new WATF(MegType.CONNECT);
                    watfMessage.info.ID_SRC = ID;
                }
                byte[] recvmessange = new byte[520];
                try
                {
                    socketRecv.Connect(ip, port);
                    byte[] bytemessange = watfMessage.StructToBytes();
                    socketRecv.Send(bytemessange);
                }
                catch (SocketException e)
                {
                    this.Dispatcher.Invoke(new Action(() => window.SetLoginStatus(false)));
                    socketRecv.Close();
                    return;
                }
                this.Dispatcher.Invoke(new Action(() => window.SetLoginStatus(true)));

                Task t = Task.Run(() => SetConnectedUser()); // проверка списка контактов на подключение к серверу

                while (true)
                {
                    try
                    {
                        socketRecv.Receive(recvmessange);
                    }
                    catch (SocketException e)
                    {
                        this.Dispatcher.Invoke(new Action(() => window.SetLoginStatus(false)));
                        // Console.WriteLine("Eror listen message");
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
                                    xml.setIdtoXML(ID);
                                    this.Dispatcher.Invoke(new Action(() => window.SetLoginStatus(true)));
                                }
                                break;
                            }
                        case (101):
                            {
                                this.Dispatcher.Invoke(new Action(() => window.SetLoginStatus(true)));
                                break;
                            }
                        case (102):
                            {
                                newmessage.BytesToStruct(recvmessange);
                                string Lmessage = newmessage.MSG.Remove(newmessage.info.MSG_LEN - 1);
                                this.Dispatcher.Invoke(new Action(() => window.addMessageToAuser(newmessage.info.ID_SRC, newmessage.info.ID_SRC.ToString(), Lmessage)));
                                break;
                            }
                        case (103):
                            {
                                return;
                            }
                        case (105):
                            {
                                newmessage.BytesToStruct(recvmessange);
                                this.Dispatcher.Invoke(new Action(() => window.SetUserConnect(true, newmessage.info.ID_DEST)));
                                break;
                            }
                        case (106):
                            {
                                newmessage.BytesToStruct(recvmessange);
                                this.Dispatcher.Invoke(new Action(() => window.SetUserConnect(false, newmessage.info.ID_DEST)));
                                break;
                            }
                        default: break;
                    }
                    if (ID == 0) break;
                }
                socketRecv.Close();
                this.Dispatcher.Invoke(new Action(() => window.SetLoginStatus(false)));
            }
            public uint getIDfromByte(byte[] arr)
            {
                WATF message = new WATF();
                uint id;
                message.BytesToStruct(arr);
                id = message.info.ID_SRC;
                return id;
            }
        }
    }
}
