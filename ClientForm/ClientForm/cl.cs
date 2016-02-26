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



            WATF watfMessage = new WATF(1);
            WATF newmessage;

            watfMessage.info.MSG_LEN = 0;
            int size = Marshal.SizeOf(watfMessage);
            byte[] buffer = StructToBytes(watfMessage);
            byte[] recvmessange = new byte[520];
      

            socketRecv = new Socket(SocketType.Stream, ProtocolType.Tcp);

            try
            {

                socketRecv.Connect(ip, port);
                textBox2.Invoke(new Action(() => textBox2.Text = "Connect to server " + ip));



                byte[] bytemessange = StructToBytes(watfMessage);

                socketRecv.Send(bytemessange);


                while (true)
                {

                    socketRecv.Receive(recvmessange);



                    if (recvmessange[0] != 101)
                    {
                        continue;
                    }
                    newmessage = BytesToStruct(recvmessange);




                    textBox2.Invoke(new Action(() => textBox2.Text += Environment.NewLine + newmessage.MSG));

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

        

    }


}
