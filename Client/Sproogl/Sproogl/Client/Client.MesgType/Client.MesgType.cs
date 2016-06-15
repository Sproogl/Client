using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sp
{
    partial class Client
    {
        /// <summary>
        /// type message 
        /// REGISTRATION = 100, 
        /// CONNECT = 101, 
        /// MESSANGE = 102, 
        /// DISCONNECT = 103, 
        /// ERROR = 104, 
        /// USERONLINE = 105
        /// </summary>
        public enum MegType : byte { REGISTRATION = 100, CONNECT, MESSANGE, DISCONNECT, ERROR, USERONLINE,USEROFFLINE,REQUESTONFRIEND,SEARCHUSER,UNACCEPTED,ACCEPTONFRIEND};
    }
}
