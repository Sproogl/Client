using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserChat;
using Itemlist;

namespace sp
{

    public class UserControls : IUserControls
    {
        ItemList itemlist;
        Userchat itemchat;
        private int index;
        private uint ID;

        public UserControls(ItemList itemList, Userchat itemChat, int ind, uint id)
        {
            itemlist = itemList;
            itemchat = itemChat;
            index = ind;
            ID = id;
        }
        public void addMessage(string Unick, string mesg)
        {
            itemchat.AddMessagetoMessageList(Unick, mesg);

        }
        public void setIndicatorConnected(bool st)
        {
            itemlist.setConnected(st);
        }
        public void setIndicatorNewMesg(bool ind)
        {
            itemlist.setIndictorMessage(ind);
        }
        public uint getID()
        {
            return ID;
        }
        public ItemList getitemlist()
        {
            return itemlist;
        }

        public Userchat getitemchat()
        {
            return itemchat;
        }

        public int getIndex()
        {
            return index;
        }
    }
}
