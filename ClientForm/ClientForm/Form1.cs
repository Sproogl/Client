using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.InteropServices;

namespace ClientForm
{
    public partial class Form1 : Form , IClient
    {

        bool movemu;

        public Form1()
        {
            InitializeComponent();
            //string ip = "192.168.1.5";
            //int port = 1332;
            SetConfig();
            
            movemu = false;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            uint ID_DEST = 0;
            try
            {
               ID_DEST = Convert.ToUInt32(textBox3.Text);
            }
            catch(FormatException ex)
            {
                MessageBox.Show("Введите id");
               
                return;
            }
            sendMessage(textBox1.Text, ID_DEST);
            textBox2.Text += Environment.NewLine + textBox1.Text;
            textBox1.Clear();
            
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {


            if (e.KeyChar == 13)
            {
                uint ID_DEST = 0;
                try
                {
                    ID_DEST = Convert.ToUInt32(textBox3.Text);
                }
                catch (FormatException ex)
                {
                    MessageBox.Show("Введите id");

                    return;
                }
                sendMessage(textBox1.Text, ID_DEST);
                textBox2.Text += Environment.NewLine + textBox1.Text;
                textBox1.Clear();
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sendMessage("disconnect", 0000);
            this.Close();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            movemu = true;
            
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if(movemu)
            {
                //this.Location = new Point((Cursor.Position.X - this.Location.X), (Cursor.Position.Y - this.Location.Y));
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            movemu = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listenMesg();
        }

    }
}
