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

    public partial class Form1 : Form
    {

        public byte[] status = { 100, 101, 102 };





        Socket socketSend, socketRecv;
        string ip;
        int port;
        int ID;

        public void SetConfig(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
        }
        public void sendMessage(string message)
        {
            WATF watfMessage = new WATF(1);


            watfMessage.info.MSG_LEN = message.Length;
            watfMessage.MSG = message;



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


        private static byte[] StructToBytes(WATF myStruct1)   // in Byte[]
        {

            byte[] byData = System.Text.Encoding.ASCII.GetBytes(myStruct1.MSG+'\0');

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


        private static WATF BytesToStruct(byte[] arr)   // in WATF
        {
            IntPtr pnt = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(msginfo)));
            WATF myStruct = new WATF(1);
            try
            {
                int sizeinfo = Marshal.SizeOf(typeof(msginfo));
                int sizemsg = arr.Length - sizeinfo;

                byte[] byteinfo = new byte[sizeinfo];

                byte[] msgbyte = new byte[sizemsg];

                for (int i = 0; i < sizeinfo; i++ )
                {

                    byteinfo[i] = arr[i];

                }

                for (int i = sizeinfo; i < (arr.Length); i++ )
                {

                    msgbyte[i-sizeinfo] = arr[i];

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

        private void listenMesgThread()
        {



            WATF watfMessage = new WATF(100);
            WATF newmessage;

          
     

            socketRecv = new Socket(SocketType.Stream, ProtocolType.Tcp);


            if(getIdfromXML() == 0)
            {
                watfMessage = new WATF(status[1]);
            }
            else
            {
                ID = getIdfromXML();
                watfMessage = new WATF(status[2]);
            }

            byte[] recvmessange = new byte[520];

                                try
                                {

                                     socketRecv.Connect(ip, port);
                                     byte[] bytemessange = StructToBytes(watfMessage);
                                     socketRecv.Send(bytemessange);

                                }

                                catch (SocketException e)
                                {
                                    textBox2.Invoke(new Action(() => textBox2.Text = "Error conection to server " + ip));
                                    // Console.WriteLine("Eror listen message");
                                    socketRecv.Close();
                                    return;
                                }

            while (true)
            {

                socketRecv.Receive(recvmessange);

                switch (recvmessange[0])
                {

                    case (100):
                        {
                            ID = getIDfromByte(recvmessange);
                            if (ID != 0)
                            {
                                setIdtoXML(ID);
                                textBox2.Invoke(new Action(() => textBox2.Text = "Connect to server " + ip));
                            }
                          
                            break;
                        }
                    case (101):
                        {
                            textBox2.Invoke(new Action(() => textBox2.Text = "Connect to server " + ip));
                            break;
                        }
                    case (102):
                        {
                            newmessage = BytesToStruct(recvmessange);
                            textBox2.Invoke(new Action(() => textBox2.Text += Environment.NewLine + newmessage.MSG));
                            
                            break;

                        }


                    default: break;       
                }
                if (ID == 0) break;
            }

            socketRecv.Close();
            textBox2.Invoke(new Action(() => textBox2.Text = "Error conection to server " + ip));
        }
                
            
        public int getIDfromByte(byte [] arr)
        {
            WATF message;
            int id;

            message = BytesToStruct(arr);
           try
           {

            id = Convert.ToInt32(message.MSG);

           }

            catch(FormatException e)
           {
               return 0;
           }

            return id;
        }

        protected void setIdtoXML(int ID)
        {

            string fileName = "config.xml";
         

            XDocument doc = new XDocument(new XElement("person",
                                                        new XElement("id", ID)));
            doc.Save(fileName);

        }


        protected int getIdfromXML()
        {

            string fileName = "config.xml";
            int id = 12345;

            XDocument docin = XDocument.Load(fileName);

            XElement element = docin.Root.Element("id");
            try
            {

                id = Convert.ToInt32(element.Value);

            }

            catch (FormatException e)
            {
                return 0;
            }

            return id;
        }


    }


}
