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
            void newMessage(WATF message);
            void onlineFriend(WATF message);
            void requestOnFriend(WATF message);
            void Searchuser(WATF message);
        }
    }
}
