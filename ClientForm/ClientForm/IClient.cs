using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace ClientForm
{
    interface IClient
    {
       void SetConfig(string obj, int obj1);
       void sendMessage(string obj, uint obj1);
       byte[] StructToBytes(WATF obj);
       WATF BytesToStruct(byte[] obj);
       void listenMesg();
       void listenMesgThread();
       void setIdtoXML(uint obj);
       uint getIdfromXML();
    }
}
