using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace sp
{
    public partial class Client
    {
        class MessageHandler : IMessageHandler
        {
            MainWindow window;
            public Dispatcher Dispatcher;
            public MessageHandler(MainWindow window, Dispatcher Dispatcher, byte[] messageByte , int length)
            {
                this.window = window;
                this.Dispatcher = Dispatcher;
                int count = messageByte.Length;
                int size=0;
                int indexStart = 0;
                for(int i = 16; i<= length;i++)
                {
                    size++;
                    if (messageByte[i] == 0)
                    {
                       //messageByte[i] = 10;
                        byte[] newbyte = new byte[size+15];
                        Array.Copy(messageByte, indexStart, newbyte, 0, size+15);
                        handler(newbyte);
                        size = 0;
                        indexStart = i+1;
                        i += 16;
                    }
                }
                
            }


            public void connection()
            {
                this.Dispatcher.Invoke(new Action(() => window.SetLoginStatus(true)));

            }

            public void handler(byte[] ByteMessage)
            {
                WATF newmessage = new WATF();
                switch (ByteMessage[0])
                {
                    case (101):
                        {
                            connection();
                            break;
                        }
                    case (102):
                        {
                           

                            newmessage.BytesToStruct(ByteMessage);
                            newMessage(newmessage);
                            break;
                        }
                    case (105):
                        {

                            newmessage.BytesToStruct(ByteMessage);
                            onlineFriend(newmessage);
                            break;
                           
                        }
                    case (106):
                        {

                            newmessage.BytesToStruct(ByteMessage);
                            onlineFriend(newmessage);
                            break;
                        }
                    case (107):
                        {

                            newmessage.BytesToStruct(ByteMessage);
                            requestOnFriend(newmessage);
                            break;
                        }
                    case (108):
                        {

                            newmessage.BytesToStruct(ByteMessage);
                            Searchuser(newmessage);
                            break;
                        }
                    default: break;
                }
            }

            public void newMessage(WATF message)
            {
                
                this.Dispatcher.Invoke(new Action(() => window.addMessageToAuser(message.info.ID_SRC, message.info.ID_SRC.ToString(), message.MSG,true)));
            }

            public void onlineFriend(WATF message)
            {
                if (message.info.type == 105)
                {
                    this.Dispatcher.Invoke(new Action(() => window.SetUserConnect(true, message.MSG, message.info.ID_SRC)));
                }
                else
                {
                    this.Dispatcher.Invoke(new Action(() => window.SetUserConnect(false, message.MSG, message.info.ID_SRC)));
                }
            }

            public void requestOnFriend(WATF message)
            {
                this.Dispatcher.Invoke(new Action(() => window.AddrequestInList(message.MSG, message.info.ID_SRC)));
            }

            public void Searchuser(WATF message)
            {
                this.Dispatcher.Invoke(new Action(() => window.AddrequestInsearchPanel(message.MSG, message.info.ID_SRC)));
            }
        }
    }
}
