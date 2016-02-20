using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.InteropServices;
namespace ConsoleApplication5
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



    public class Client
    {

        Socket socketSend, socketRecv;
        string ip;
        int port;

       public Client(string ip , int port)
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
            catch(SocketException e)
            {
                Console.WriteLine("Eror send message");
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
            int size = Marshal.SizeOf(typeof(WATF));

            IntPtr buffer = Marshal.AllocHGlobal(size);
            Marshal.Copy(arr, 0, buffer, size);
            var myStruct = (WATF)Marshal.PtrToStructure(buffer, typeof(WATF));
            Marshal.FreeHGlobal(buffer);

            return myStruct;
        }





        public void listenMesg()
        {

            Thread sendThread = new Thread(listenMesgThread);
            sendThread.Start();


        }

       private void listenMesgThread()
        {

            WATF watfMessage = new WATF(1);

            watfMessage.MSG = "0";
            watfMessage.MSG_LEN = 0;

            byte[] buffer = StructToBytes(watfMessage);

            socketRecv = new Socket(SocketType.Stream, ProtocolType.Tcp);

            try
            {

                socketRecv.Connect(ip,port);

                socketRecv.Send(buffer);


                while(true)
                {

                    socketRecv.Receive(buffer);
                   WATF buff = BytesToStruct(buffer);
                    if(buff.MSG_LEN > 500 || buff.MSG_LEN < 0)
                    {
                        continue;
                    }

                   Console.WriteLine(buff.MSG);

                }


            }
             catch(SocketException e)
            {
                Console.WriteLine("Eror listen message");
            }
            socketRecv.Close();
        }
       
    }
}
