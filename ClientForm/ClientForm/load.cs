using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientForm
{
    
    public partial class load : Form
    {
        PictureBox[] box;
        int tick;
        int max;
        int rep;
        public load()
        {
            InitializeComponent();
            box = new PictureBox[5];
            box[0] = pictureBox1;
            box[1] = pictureBox2;
            box[2] = pictureBox3;
            box[3] = pictureBox4;
            box[4] = pictureBox5;
            for (int i = 1; i < 5;i++)
            {
                box[i].Visible = false;
            }
                tick = 0;
            max = 15;
            rep = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(tick < 5)
            {
                box[tick].Visible = true;
                if(tick == 0)
                {
                    box[4].Visible = false;
                    tick++;
                }
                else
                {
                    box[tick - 1].Visible = false;
                    tick++;
                }
            }
            else
            {
                tick = 0;
            }
         
            if (rep == max)
            {
                this.Close();
            }
            rep++;
        }
    }
}
