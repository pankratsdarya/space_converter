using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace transfers
{
    public partial class Form1 : Form
    {
        private VKP formvkp = new VKP();
        private DateTime formDT = new DateTime();
        private StarTime formST = new StarTime();

        public Form1()
        {
            InitializeComponent();
            AddOwnedForm(formvkp);
            AddOwnedForm(formDT);
            AddOwnedForm(formST);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            formvkp.Show();
            Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            formDT.Show();
            Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            formST.Show();
            Hide();
        }
    }
}
