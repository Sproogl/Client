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

namespace ClientForm
{
    public partial class Form1 : Form
    {



        public struct WATF
        {
            public byte type;
            public int ID_SRC;
            public int ID_DEST;
            public int MSG_LEN;
            public string MSG;


            public WATF(int i)
            {
                type = 121;
                ID_SRC = i;
                ID_DEST = 0;
                MSG_LEN = 0;
                MSG = "\0";
            }



        }



         Socket socketSend, socketRecv;
        string ip;
        int port;

        public void SetConfig(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
        }
        public void sendMessage(string message)
        {
            WATF watfMessage = new WATF(1);

            watfMessage.MSG = message;
            watfMessage.MSG_LEN = message.Length;

            byte[] buffer = StructToBytes(watfMessage);

            socketSend = new Socket(SocketType.Stream, ProtocolType.Tcp);
            try
            {

                socketSend.Connect(ip, port);

                socketSend.Send(buffer);

            }
            catch (SocketException e)
            {
                // Console.WriteLine("Eror send message");
            }
            socketSend.Close();
        }


        private static byte[] StructToBytes(WATF myStruct1)   // in Byte[]
        {
            WATF myStruct = myStruct1;
            int size = Marshal.SizeOf(myStruct);
            byte[] arr = new byte[size];

            IntPtr buffer = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(myStruct, buffer, false);
            Marshal.Copy(buffer, arr, 0, size);
            Marshal.FreeHGlobal(buffer);

            return arr;
        }


        private static WATF BytesToStruct(byte[] arr)   // in WATF
        {
            IntPtr pnt = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(WATF)));
            WATF myStruct = new WATF(1);
            try
            {


                Marshal.StructureToPtr(myStruct, pnt, false);
                Marshal.Copy(arr, 0, pnt, Marshal.SizeOf(typeof(WATF)));
                myStruct = (WATF)Marshal.PtrToStructure(pnt, typeof(WATF));


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

      

            WATF watfMessage = new WATF(1);
            WATF buff;
            watfMessage.MSG = "hello";
            watfMessage.MSG_LEN = 0;

            byte[] buffer = StructToBytes(watfMessage);

            buff = BytesToStruct(buffer);

            socketRecv = new Socket(SocketType.Stream, ProtocolType.Tcp);

            try
            {

                socketRecv.Connect(ip, port);
                textBox2.Invoke(new Action(() => textBox2.Text = "Connect to server " + ip));
                socketRecv.Send(buffer);


                while (true)
                {

                    socketRecv.Receive(buffer);


                    if(buffer[0] != 121)
                    {
                        continue;
                    }
                    buff = BytesToStruct(buffer);
                   
                   
                  

                    textBox2.Invoke(new Action(() => textBox2.Text += Environment.NewLine + ip +"  "+ buff.MSG));
                  
                    // Console.WriteLine(buff.MSG);

                }


            }
            catch (SocketException e)
            {
                textBox2.Invoke(new Action(() => textBox2.Text = "Error conection to server " + ip));
                // Console.WriteLine("Eror listen message");
            }
            socketRecv.Close();
        }


        public Form1()
        {
            InitializeComponent();
            string ip = "127.0.0.1";
            int port = 1322;
            SetConfig(ip, port);
            listenMesg();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            sendMessage(textBox1.Text);
            textBox2.Text += Environment.NewLine + textBox1.Text;
            textBox1.Clear();
        }

    }
}
