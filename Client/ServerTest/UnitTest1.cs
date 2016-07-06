using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sp;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Threading;

namespace ServerTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Client.WATF watfMessage;
            byte[] buffer = new byte[1024];
           Socket socketRecv = new Socket(SocketType.Stream, ProtocolType.Tcp);
            watfMessage = new Client.WATF(Client.MegType.CONNECT);
            watfMessage.info.ID_SRC = 15;
            watfMessage.MSG = "admin";

            socketRecv.Connect("127.0.0.1", 1332);
            long s = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;
            watfMessage.MSG = s.ToString();
            byte[] bytemessange = watfMessage.StructToBytes();
            socketRecv.Send(bytemessange);
            int length = socketRecv.Receive(buffer);
            Assert.AreEqual(buffer[0], 104, "breaking");
        }
    }
}
