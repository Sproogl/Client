using System.Windows.Controls;

namespace UserChat
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class Userchat : UserControl
    {
        public Userchat()
        {
            InitializeComponent();
        }

        public void setText(string text)
        {
            Text1.Text = text;
        }
    }
}
