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
using System.Globalization;

namespace ClientForm
{

    public partial class Form1 : Form, IClient
    {

        byte[] status = { 100, 101, 102 };
        readonly int REGISTRATION = 0;
        readonly int CONNECT = 1;
        readonly int MESSANGE = 2;
        Socket socketSend, socketRecv;
        string ip { get; set; }
        int port;
        uint ID;

        public void SetConfig()
        {
            this.ip = getIpfromXML();
            this.port = getPortfromXML();

        }
        public  void sendMessage(string message, uint ID_DEST)
        {
            WATF watfMessage = new WATF(1);

            watfMessage.info.type = status[MESSANGE];
            watfMessage.info.MSG_LEN = message.Length;
            watfMessage.MSG = message;
            watfMessage.info.ID_DEST = ID_DEST;
            watfMessage.info.ID_SRC = this.ID;


            socketSend = new Socket(SocketType.Stream, ProtocolType.Tcp);
            try
            {

                socketSend.Connect(ip, port);

                socketSend.Send(StructToBytes(watfMessage));

            }
            catch (SocketException e)
            {
                // Console.WriteLine("Eror send message");
            }
            socketSend.Close();
        }


        public byte[] StructToBytes(WATF myStruct1)   // in Byte[]
        {

            byte[] byData = System.Text.Encoding.ASCII.GetBytes(myStruct1.MSG + '\0');

            myStruct1.info.MSG_LEN = byData.Length;

            msginfo myStruct = myStruct1.info;
            int size = Marshal.SizeOf(myStruct);
            byte[] arr = new byte[size];

            IntPtr buffer = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(myStruct, buffer, false);
            Marshal.Copy(buffer, arr, 0, size);
            Marshal.FreeHGlobal(buffer);



            byte[] bytemessange = arr.Concat(byData).ToArray();

            return bytemessange;
        }


        public  WATF BytesToStruct(byte[] arr)   // in WATF
        {
            IntPtr pnt = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(msginfo)));
            WATF myStruct = new WATF(1);
            try
            {
                int sizeinfo = Marshal.SizeOf(typeof(msginfo));
                int sizemsg = arr.Length - sizeinfo;

                byte[] byteinfo = new byte[sizeinfo];

                byte[] msgbyte = new byte[sizemsg];

                for (int i = 0; i < sizeinfo; i++)
                {

                    byteinfo[i] = arr[i];

                }

                for (int i = sizeinfo; i < (arr.Length); i++)
                {

                    msgbyte[i - sizeinfo] = arr[i];

                }

                Marshal.StructureToPtr(myStruct.info, pnt, false);
                Marshal.Copy(byteinfo, 0, pnt, Marshal.SizeOf(typeof(msginfo)));
                myStruct.info = (msginfo)Marshal.PtrToStructure(pnt, typeof(msginfo));

                myStruct.MSG = System.Text.Encoding.UTF8.GetString(msgbyte);


            }
            finally
            {

                Marshal.FreeHGlobal(pnt);
            }



            return myStruct;




        }





        public void listenMesg()
        {

            Task t = Task.Run(() => listenMesgThread());

            // new Thread(listenMesgThread) { IsBackground = true }.Start(); 



        }

        public void listenMesgThread()
        {

       

            WATF watfMessage = new WATF(100);
            WATF newmessage;




            socketRecv = new Socket(SocketType.Stream, ProtocolType.Tcp);


            if (getIdfromXML() == 0)
            {
                watfMessage = new WATF(status[REGISTRATION]);
                watfMessage.MSG = "registration";
            }
            else
            {

                ID = getIdfromXML();
              
                label2.Invoke(new Action(() => label2.Text = ID.ToString()));
                watfMessage = new WATF(status[CONNECT]);
                watfMessage.info.ID_SRC = ID;
            }

            byte[] recvmessange = new byte[520];

            try
            {

                socketRecv.Connect(ip, port);
                byte[] bytemessange =StructToBytes(watfMessage);
                socketRecv.Send(bytemessange);

            }

            catch (SocketException e)
            {
                textBox4.Invoke(new Action(() => textBox4.Text = "Error conection to server " + ip));
                // Console.WriteLine("Eror listen message");
                socketRecv.Close();
                return;
            }
            textBox4.Invoke(new Action(() => textBox4.Text = "Connect " + ip));
            while (true)
            {

                try
                {
                    socketRecv.Receive(recvmessange);
                }
                catch(SocketException e)
                {
                    textBox4.Invoke(new Action(() => textBox4.Text = "Error conection to server " + ip));
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
                                setIdtoXML(ID);
                                label2.Invoke(new Action(() => label2.Text = ID.ToString()));
                                textBox4.Invoke(new Action(() => textBox4.Text = "Connect to server " + ip));
                            }

                            break;
                        }
                    case (101):
                        {
                            textBox4.Invoke(new Action(() => textBox4.Text = "Connect to server " + ip));
                            break;
                        }
                    case (102):
                        {
                            newmessage = BytesToStruct(recvmessange);
                            textBox2.Invoke(new Action(() => textBox2.Text += Environment.NewLine +newmessage.info.ID_SRC +" : "+ newmessage.MSG));

                            break;

                        }


                    default: break;
                }
                if (ID == 0) break;
            }

            socketRecv.Close();
            textBox4.Invoke(new Action(() => textBox4.Text = "Error conection to server " + ip));
        }


        public  uint getIDfromByte(byte[] arr)
        {
            WATF message;
            uint id;

            message = BytesToStruct(arr);


                id = message.info.ID_SRC;

 

            return id;
        }

        public  void setIdtoXML(uint ID)
        {

            string fileName = "config.xml";


            XDocument doc = new XDocument(new XElement("person",
                                                   new XElement("id", ID),
                                                   new XElement("ip", port),
                                                   new XElement("Port", ip)));
            doc.Save(fileName);

        }


        public uint getIdfromXML()
        {

            string fileName = "config.xml";
            uint id = 12345;

            XDocument docin = XDocument.Load(fileName);

            XElement element = docin.Root.Element("id");
            try
            {

                id = Convert.ToUInt32(element.Value);

            }

            catch (NullReferenceException e)
            {
                return 0;
            }

            return id;
        }

        public void setPortToXML(int lport)
        {

            string fileName = "config.xml";


            XDocument doc = new XDocument(new XElement("person",
                                                        new XElement("id", ID),
                                                        new XElement("ip", lport),
                                                        new XElement("Port", ip)));
            doc.Save(fileName);



        }

        public void setIpToXML(string lip)
        {

            string fileName = "config.xml";


            XDocument doc = new XDocument(new XElement("person",
                                                        new XElement("id", ID),
                                                        new XElement("ip", port),
                                                        new XElement("Port", lip)));
            doc.Save(fileName);



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

    }


}

