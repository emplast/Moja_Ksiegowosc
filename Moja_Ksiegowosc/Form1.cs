using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Moja_Ksiegowosc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void wyjdżToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void nowaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Form2 form2 = new Form2();
            form2.ShowDialog();
            
        }

        private void dodajToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Form6 form6 = new Form6();
            form6.ShowDialog();
        }

        private void usuńToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Form7 form7 = new Form7();
            form7.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            timer1.Start();
            toolStripStatusLabel3.Text = DateTime.Now.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = DateTime.Now.ToString();
        }

        private void archiwizacjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Magazyn_podstawowy.Form3_mag form3 = new Magazyn_podstawowy.Form3_mag();
            form3.ShowDialog();
        }
    }
}
