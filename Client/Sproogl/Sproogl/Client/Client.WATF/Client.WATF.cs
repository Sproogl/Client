using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace sp
{
    public partial class Client
    {

        public struct msginfo
        {

            public byte type;
            public uint ID_SRC;
            public uint ID_DEST;
            public int MSG_LEN;

            public msginfo(byte i)
            {
                type = i;
                ID_SRC = 0;
                ID_DEST = 0;
                MSG_LEN = 0;

            }

        }

        public struct WATF
        {

            public msginfo info;
            public string MSG;
            public WATF(MegType type)
            {
                info = new msginfo((byte)type);
                MSG = "connect";
                info.MSG_LEN = MSG.Length;
            }

            public WATF(byte i)
            {
                info = new msginfo(i);
                MSG = "connect";
                info.MSG_LEN = MSG.Length;
            }




            /// <summary>
            /// watf translates into an array of byte
            /// </summary>
            /// <returns>
            /// byte[]
            /// </returns>
            public byte[] StructToBytes()   // in Byte[]
            {

                byte[] byData = System.Text.Encoding.UTF8.GetBytes(MSG + '\0');

                info.MSG_LEN = byData.Length;

                msginfo myStruct = info;
                int size = Marshal.SizeOf(myStruct);
                byte[] arr = new byte[size];

                IntPtr buffer = Marshal.AllocHGlobal(size);
                Marshal.StructureToPtr(myStruct, buffer, false);
                Marshal.Copy(buffer, arr, 0, size);
                Marshal.FreeHGlobal(buffer);
                byte[] bytemessange = arr.Concat(byData).ToArray();
                return bytemessange;
            }



            /// <summary>
            /// converts an array of everyday life in WATF
            /// </summary>
            /// <param name = "arr"> message in byte[] </param>
            /// <returns>
            /// byte[]
            /// </returns>
            public void BytesToStruct(byte[] arr)   // in WATF
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
                this = myStruct;              
            }
        }
    }
}