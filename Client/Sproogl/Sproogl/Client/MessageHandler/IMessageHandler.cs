using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sp
{
    public partial class Client
    {
        interface IMessageHandler
        {
            void handler(byte[] ByteMessage);
            void connection();
            void newMessage(CPS message);
            void onlineFriend(CPS message);
            void requestOnFriend(CPS message);
            void Searchuser(CPS message);
        }
    }
}
