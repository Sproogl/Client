using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FoundItemList;
using Itemlist;

namespace SearchFriendPanel
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class SearchPanelControl : UserControl
    {
        public event EventHandler<FoundItemArgs> AddFriend;
        public event EventHandler<UserItemcontrolArgs> SearchClick;


        public SearchPanelControl()
        {
            InitializeComponent();
        }

        public void AddUser(string name , uint id)
        {
            FoundItem newitem = new FoundItem(name, id);
            newitem.Addclick += Newitem_Addclick;
            foundUsersList.Items.Add(newitem);
        }


        protected void RaiseAddFriend(FoundItemArgs e)
        {
            if (AddFriend != null)
            {
                AddFriend(this, e);
            }
        }

        protected void RaiseSearcClick(string name)
        {
            if (SearchClick != null)
            {
                SearchClick(this, new UserItemcontrolArgs(name));
            }
        }

        private void Newitem_Addclick(object sender, FoundItemArgs e)
        {
            RaiseAddFriend(e);
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            foundUsersList.Items.Clear();
            RaiseSearcClick(searchTextBox.Text);
        }
    }
}
