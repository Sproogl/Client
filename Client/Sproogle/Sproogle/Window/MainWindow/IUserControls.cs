using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserChat;
using Itemlist;

namespace sp
{
    interface IUserControls
    {


        /// <summary>
        /// Add message to userchat
        /// </summary>
        /// <param name = "Unick"> uuser nick </param>
        /// <param name = "mesg"> uuser nick </param>
        /// <returns>
        /// </returns>
        void addMessage(string Unick, string mesg);



        /// <summary>
        /// set online status on Title
        /// </summary>
        /// <param name = "st"> true = ONLINE or false = OFFLINE </param>
        /// <returns>
        /// </returns>
        void setIndicatorConnected(bool st);



        /// <summary>
        /// set indicator on ItemList
        /// </summary>
        /// <param name = "st"> true = ONLINE or false = OFFLINE </param>
        /// <returns>
        /// </returns>
        void setIndicatorNewMesg(bool ind);

    }
}
