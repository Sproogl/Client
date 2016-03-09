using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientForm
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

      public  msginfo info;
       public string MSG;


        public WATF(byte i)
        {
            info = new msginfo(i);
            MSG = "connect";
            info.MSG_LEN = MSG.Length;
        }



    }
}
