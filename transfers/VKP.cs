using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace transfers
{
    public partial class VKP : Form
    { 
        double mu=398600.44;
        double GVR=0.0174532925;
        bool copypaste = false;
        
        public VKP()
        {
            InitializeComponent();          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Owner.Show();
            Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();  
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                return;
            }

            if (e.KeyChar == '.')
            {
                e.KeyChar = ',';
                return;
            }

            if (e.KeyChar == ',')
            {
                e.KeyChar = ',';
                return;
            }

            if (e.KeyChar == '-')
            {
                return;
            }

            if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Delete || e.KeyChar == (char)Keys.Left || e.KeyChar == (char)Keys.Right)
                    return;
            }           
            e.Handled = true;            
        }   

        private void button4_Click(object sender, EventArgs e)
        {
            double A, i, u, ee, bomega, somega;
            double X, Y, Z, Vx, Vy, Vz;
            double V, p, R, Vr, Vn;
            double cu,su,co,so,ci,si;
            bool ifok = true;
            A = Convert.ToDouble(textBox24.Text);
            ee = Convert.ToDouble(textBox23.Text);
            bomega = Convert.ToDouble(textBox21.Text);
            somega = Convert.ToDouble(textBox20.Text);
            i = Convert.ToDouble(textBox22.Text);
            u = Convert.ToDouble(textBox19.Text);
            if (checkBox3.Checked)
            {
                bomega = bomega * GVR;
                somega = somega * GVR;
                i = i * GVR;
                u = u * GVR;
            }

            if (i > Math.PI)
            {
                MessageBox.Show("Введенный угол наклона вне допустимого диапазона.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ifok = false;
            }
            if (u > 2 * Math.PI)
            {
                MessageBox.Show("Введенный аргумент широты КА вне допустимого диапазона.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ifok = false;
            }
            if (bomega > 2 * Math.PI)
            {
                MessageBox.Show("Введенная долгота вне допустимого диапазона.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ifok = false;
            }
            if (somega > 2 * Math.PI)
            {
                MessageBox.Show("Введенный аргумент широты вне допустимого диапазона.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ifok = false;
            }
            if (A < 6400)
            {
                MessageBox.Show("Введенный радиус вне допустимого диапазона.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ifok = false;
            }

            if (ifok)
            {
                cu = Math.Cos(u);
                co = Math.Cos(bomega);
                ci = Math.Cos(i);
                su = Math.Sin(u);
                so = Math.Sin(bomega);
                si = Math.Sin(i);
                V = u - somega;
                if (V < 0)
                    V = V + 2 * Math.PI;
                p = A * (1 - ee * ee);
                R = A * (1 - ee * ee) / (1 + ee * Math.Cos(V));
                X = R * (cu * co - su * so * ci);
                Y = R * (cu * so + su * co * ci);
                Z = R * su * si;
                Vr = Math.Sqrt(mu / p) * ee * Math.Sin(V);
                Vn = Math.Sqrt(mu / p) * (1 + ee * Math.Cos(V));
                Vx = X / R * Vr + (-su * co - cu * so * ci) * Vn;
                Vy = Y / R * Vr + (-su * so + cu * co * ci) * Vn;
                Vz = Z / R * Vr + cu * si * Vn;
                textBox18.Text = X.ToString();
                textBox17.Text = Y.ToString();
                textBox16.Text = Z.ToString();
                textBox15.Text = Vx.ToString();
                textBox14.Text = Vy.ToString();
                textBox13.Text = Vz.ToString();
            }            
      
        }

        public double arctg(double a, double b)
        {
            double atan;
            if (a >= 0)
            {
                if (b >= 0)
                    atan = Math.Atan(a / b);
                else
                    atan = Math.PI - Math.Atan(a / (-b));
            }
            else
                if (b >= 0)
                    atan = 2 * Math.PI - Math.Atan((-a) / b);
                else
                    atan = Math.Atan((-a) / (-b)) + Math.PI;

            return atan;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double A, i, u, ee, bomega, somega;
            double X, Y, Z, Vx, Vy, Vz;
            double c1, c2, c3, C;
            double f1, f2, f3, F;
            double R, p;
            X = Convert.ToDouble(textBox2.Text);
            Y = Convert.ToDouble(textBox3.Text);
            Z = Convert.ToDouble(textBox4.Text);
            Vx = Convert.ToDouble(textBox5.Text);
            Vy = Convert.ToDouble(textBox6.Text);
            Vz = Convert.ToDouble(textBox7.Text);
            R = Math.Sqrt(X * X + Y * Y + Z * Z);
            c1 = Y * Vz - Z * Vy;
            c2 = Z * Vx - X * Vz;
            c3 = X * Vy - Y * Vx;
            C = Math.Sqrt(c1 * c1 + c2 * c2 + c3 * c3);
            f1 = -mu * X / R + c3 * Vy - c2 * Vz;
            f2 = -mu * Y / R + c1 * Vz - c3 * Vx;
            f3 = -mu * Z / R + c2 * Vx - c1 * Vy;
            F = Math.Sqrt(f1 * f1 + f2 * f2 + f3 * f3);
            bomega = arctg(c1, (-c2));
            i = arctg(Math.Sqrt(c1 * c1 + c2 * c2), c3);
            somega = arctg(-C * (f1 * c1 + c2 * f2), -c3 * (f1 * c2 - f2 * c1));
            u = arctg(Z * C, c1 * Y - c2 * X);
            if (checkBox2.Checked)
            {
                bomega = bomega / GVR;
                i = i / GVR;
                somega = somega / GVR;
                u = u / GVR;
            }
            ee = F / mu;
            p = C * C / mu;
            A = p / (1 - ee * ee);
            textBox1.Text = A.ToString();
            textBox8.Text = ee.ToString();
            textBox9.Text = i.ToString();
            textBox10.Text = bomega.ToString();
            textBox11.Text = somega.ToString();
            textBox12.Text = u.ToString();
        }

        public void putout(List<double> vout, object sender)
        {
            if (sender.Equals(button5))
            {
                textBox2.Text = vout[0].ToString();
                textBox3.Text = vout[1].ToString();
                textBox4.Text = vout[2].ToString();
                textBox5.Text = vout[3].ToString();
                textBox6.Text = vout[4].ToString();
                textBox7.Text = vout[5].ToString();
            }
            else
            {
                textBox24.Text = vout[0].ToString();
                textBox23.Text = vout[1].ToString();
                textBox22.Text = vout[2].ToString();
                textBox21.Text = vout[3].ToString();
                textBox20.Text = vout[4].ToString();
                textBox19.Text = vout[5].ToString();
            }
        }
               
        private void button_Click(object sender, EventArgs e)
        {
            string filename, s;
            List<double> fromfile = new List<double>();
           
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog1.FileName;
                using (StreamReader sr = new StreamReader(filename))
                {
                    while ((s = sr.ReadLine()) != null)
                    {
                        List<double> temp = new List<double>();
                        if (s.IndexOf(' ') >= 0)
                        {
                            temp = (from line in s.Split(' ') select double.Parse(line.Trim())).ToList();
                            for (int i = 0; i < temp.Count; i++)
                            {
                                fromfile.Add(temp[i]);
                            }
                        }
                        else
                            fromfile.Add(Convert.ToDouble(s));
                    }
                }
                putout(fromfile, sender);                
            }

        }



        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                if (sender.Equals(textBox2))
                    textBox2.Paste();
                if (sender.Equals(textBox3))
                    textBox3.Paste();
                if (sender.Equals(textBox4))
                    textBox4.Paste();
                if (sender.Equals(textBox5))
                    textBox5.Paste();
                if (sender.Equals(textBox6))
                    textBox6.Paste();
                if (sender.Equals(textBox7))
                    textBox7.Paste();
                if (sender.Equals(textBox24))
                    textBox24.Paste();
                if (sender.Equals(textBox23))
                    textBox23.Paste();
                if (sender.Equals(textBox22))
                    textBox22.Paste();
                if (sender.Equals(textBox21))
                    textBox21.Paste();
                if (sender.Equals(textBox20))
                    textBox20.Paste();
                if (sender.Equals(textBox19))
                    textBox19.Paste();
            }
        }
    }
}
