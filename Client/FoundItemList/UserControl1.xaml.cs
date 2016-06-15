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

namespace FoundItemList
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class FoundItem : UserControl
    {
        uint id;
        string name;
        public event EventHandler<FoundItemArgs> Addclick;
        public FoundItem(string name, uint id)
        {
            InitializeComponent();
            Nick.Text = name;
            this.name = name;
            this.id = id;

        }
        public FoundItem()
        {
            InitializeComponent();
            id = 0;
        }


        protected void RaiseAddClick(string name, uint id)
        {
            if (Addclick != null)
            {
                Addclick(this, new FoundItemArgs(name,id));
            }
        }

        private void PART_call_Click(object sender, RoutedEventArgs e)
        {
            RaiseAddClick(name,id);
        }
    }
    public class FoundItemArgs : EventArgs
    {
        public uint id;
        public string name { private set; get; }

        public FoundItemArgs(string name,uint id)
        {
            this.name = name;
            this.id = id;
        }

       
    }
}
