using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Windows.Forms;

namespace ClientForm
{
    public partial class setting : Form
    {
        public setting()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            setIpToXML(textBox1.Text);
            setPortToXML(textBox2.Text);
            this.Close();
        }
        public void setPortToXML(string lport)
        {

            string fileName = "config.xml";

            XDocument docin = XDocument.Load(fileName);

            XElement element = docin.Root.Element("port");
            element.Value = lport;
            docin.Save(fileName);



        }

        public void setIpToXML(string lip)
        {

            string fileName = "config.xml";

            XDocument docin = XDocument.Load(fileName);

            XElement element = docin.Root.Element("ip");
            element.Value = lip;
            docin.Save(fileName);



        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
