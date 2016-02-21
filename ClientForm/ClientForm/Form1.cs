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

            public int ID_SRC;
            public int ID_DEST;
            public int MSG_LEN;
            public string MSG;


            public WATF(int i)
            {
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


        private static byte[] StructToBytes(WATF myStruct)   // in Byte[]
        {
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



            // Создаем объект
            var myStruct = new WATF();
            // Выделяем под него память
            var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(WATF)));
            /* Копируем созданный объект в выделенный
             * участок памяти, и при этом не хотим
             * чтобы блок памяти перед этим очищался */
            Marshal.StructureToPtr(myStruct, ptr, false);
            // Записываем в первые четыре байта выделенной
            // памяти, т.е. в поле i1, значение 10
            Marshal.WriteInt32(ptr, 0xA);
            // Копируем структуру обратно в ex
            myStruct = (WATF)Marshal.PtrToStructure(ptr, typeof(WATF));
            // Освобождаем память!
            Marshal.FreeHGlobal(ptr);



            return myStruct;




        }





        public void listenMesg()
        {

            new Thread(listenMesgThread) { IsBackground = true }.Start(); 
            


        }

        private void listenMesgThread()
        {

      

            WATF watfMessage = new WATF(1);
            WATF buff;
            watfMessage.MSG = "0";
            watfMessage.MSG_LEN = 0;

            byte[] buffer = StructToBytes(watfMessage);

            socketRecv = new Socket(SocketType.Stream, ProtocolType.Tcp);

            try
            {

                socketRecv.Connect(ip, port);

                socketRecv.Send(buffer);


                while (true)
                {

                    socketRecv.Receive(buffer);

                    buff = BytesToStruct(buffer);
                   
                    if (buff.MSG_LEN > 500 || buff.MSG_LEN < 0)
                    {
                        continue;
                    }

                    textBox2.Invoke(new Action(() => textBox2.Text = "Connect to server "+ ip));

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
