using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sp
{
    interface IActionClient
    {





        /// <summary>
        /// Disconnect from server and the server sends a message of type DISCONNECT
        /// </summary>
        /// <returns>
        /// </returns>
        void disconnectFromserver();


        /// <summary>
        /// the server sends a message of type MESSAGE
        /// </summary>
        /// <param name = "message"> text message </param>
        /// <param name = "ID_DEST"> id destination </param>
        /// <returns>
        /// </returns>
        void sendMessage(string message, uint ID_DEST);


        /// <summary>
        /// method creates a new thread with the method listenMesgThread
        /// </summary>
        /// <returns>
        /// </returns>
        void listenMesg();


        /// <summary>
        /// method of listening to and processing messages from the server
        /// </summary>
        /// <returns>
        /// </returns>
        void listenMesgThread();


        /// <summary>
        /// returns id of the byte array
        /// </summary>
        /// <param name = "arr"> message byte[] </param>
        /// <returns>
        /// uint id
        /// </returns>
        uint getIDfromByte(byte[] arr);
    }
}
