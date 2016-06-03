using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sp
{

        interface IClient
        {

        /// <summary>
        /// returned id this person
        /// </summary>
        /// <param></param>
        /// <returns>
        /// uint ID
        /// </returns>
        uint getId();


        /// <summary>
        /// Disconnects from a server socket
        /// </summary>
        /// <param></param>
        /// <returns>
        /// </returns>
        void disconnectToserver();


        /// <summary>
        /// sends a message to a user by id
        /// </summary>
        /// <param name = "message"> this text message </param>
        /// <param name = "ID_DEST"> this ID destination </param>
        /// <returns>
        /// </returns>
        void sendMessage(string message, uint ID_DEST);

        }
    }
