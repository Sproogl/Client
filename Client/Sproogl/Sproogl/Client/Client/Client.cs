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


    public partial class Client : IClient
    {
        protected ActionClient actionClient { get; }

        public uint getId()
        {
            return actionClient.getId();
        }

        public void disconnectToserver()
        {
            actionClient.disconnectFromserver();
        }
        public void sendMessage(string message, uint ID_DEST)
        {
            actionClient.sendMessage(message,ID_DEST);
        }
        public void SendRequestOnFriend(string name, uint id)
        {
            actionClient.SendRequestOnFriend(name, id);
        }

        public void SendAcceptOnFriend(string name, uint id)
        {
            actionClient.SendAcceptOnFriend(name, id);
        }

        public Client(MainWindow window , uint id, string login)
        {
            actionClient = new ActionClient(window,id,login);
        }
        public void SendSearchFriend(string name)
        {
            actionClient.SendSearchFriend(name);
        }

    }
}