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
        public int ID_SRC;
        public int ID_DEST;
        public int MSG_LEN;

            public msginfo(byte i)
        {
            type = 100;
            ID_SRC = i;
            ID_DEST = i;
            MSG_LEN = i;

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
        }



    }
}
