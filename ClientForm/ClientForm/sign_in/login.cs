using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace ClientForm
{
    public partial class login : Form
    {
        int x, y,xx;
        int locX, locY;
        int max;
        bool pressed;
        Form1 parent;
        string password;

        public login(Form1 form)
        {
            InitializeComponent();
            x = 616;
            xx = -20;
            y = 24;
            max = 420;
            parent = form;
            pressed = false;
            password = null;
            button1.FlatAppearance.BorderSize = 0;
            int typeL = Application.CurrentInputLanguage.Culture.LCID;
            if (typeL == 1049)
                label2.Text = "RUS";
            if (typeL == 1033)
                label2.Text = "ENG";
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void pictureBox5_MouseClick(object sender, MouseEventArgs e)
        {
            parent.logIn = false;
            this.Close();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            if (y <= max)
            {
                y = -xx * xx + 425;
                this.Size = new Size(x, y);
                xx++;
            }
            else
            {
                timer1.Enabled = false;
            }
        }


        private void pictureBox4_MouseUp(object sender, MouseEventArgs e)
        {
            pressed = false;
        }



        private void pictureBox4_MouseDown_1(object sender, MouseEventArgs e)
        {
            locX = e.X;
            locY = e.Y;
            pressed = true;
        }

        private void pictureBox4_MouseMove(object sender, MouseEventArgs e)
        {
            if (pressed)
            {
                
                this.Location = new Point(Cursor.Position.X - locX, Cursor.Position.Y - locY);
            }
        }



        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
           
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex log = new Regex(@"\w");
            if (e.KeyChar == 13)
            {
                parent.logIn = true;
                this.Close();
            }
            if (e.KeyChar == 8)
            {
                if (password.Length != 0)
                {
                    textBox2.Text = textBox2.Text.Remove(textBox2.Text.Length - 1);
                    password = password.Remove(password.Length - 1);

                }
            }

            else
            {
                if (log.IsMatch(e.KeyChar.ToString()))
                {
                    textBox2.Text += (char)65517;
                    textBox2.SelectionStart = textBox2.Text.Length;
                    password += e.KeyChar;


                    textBox2.SelectionStart = textBox2.Text.Length;
                   
                }
            }
            e.Handled = true;
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
           // textBox2.Clear();
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
          //  textBox1.Clear();
        }

        private void login_InputLanguageChanged(object sender, InputLanguageChangedEventArgs e)
        {
            int typeL = Application.CurrentInputLanguage.Culture.LCID;
            if (typeL == 1049)
                label2.Text = "RUS";
            if(typeL == 1033)
                label2.Text = "ENG";
            
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            textBox2.Clear();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void login_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void login_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                parent.logIn = true;
                this.Close();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            parent.logIn = true;
            this.Close();
        }


    }
}
