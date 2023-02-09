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
    public partial class StarTime : Form
    {
        private spr2 spravka=new spr2();

        int[] mmon = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        int[] myear = { 366, 365, 365, 365 };
        bool green;
        int tday = 86400;
        int thour = 3600;
        double C = 0.48481368E-5;

        public StarTime()
        {
            InitializeComponent();
            green = checkBox2.Checked;
            AddOwnedForm(spravka);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Owner.Show();
            Hide();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                label3.Enabled = true;
                label4.Enabled = true;
                label5.Enabled = true;
                label6.Enabled = true;
                label7.Enabled = true;
                label8.Enabled = true;

                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox5.Enabled = true;
                textBox6.Enabled = true;
            }
            else
            {
                label3.Enabled = false;
                label4.Enabled = false;
                label5.Enabled = false;
                label6.Enabled = false;
                label7.Enabled = false;
                label8.Enabled = false;

                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
                textBox6.Enabled = false;
            }
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

            if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Delete || e.KeyChar == (char)Keys.Left || e.KeyChar == (char)Keys.Right)
                    return;
            }
            e.Handled = true;
        }

        private void textBox_KeyPress2(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.TextLength == textBox1.MaxLength)
            {
                textBox2.Focus();
            }  
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.TextLength == textBox2.MaxLength)
            {
                textBox3.Focus();
            }  
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.TextLength == textBox3.MaxLength)
            {
                textBox4.Focus();
            }  
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.TextLength == textBox4.MaxLength)
            {
                textBox5.Focus();
            }  
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.TextLength == textBox5.MaxLength)
            {
                textBox6.Focus();
            }  
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            green = checkBox2.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int dday, mmonth, yyear, hhour, mmin, j, n, k, intresult;
            double ssec;
            double result = 0;
            double ST, STGR, So, T, t, d, c1, c2, c3, e0, nu;
            int SThour, STmin, STGG, STGM;
            double STsec, STGS;
            bool ifok = true;
            if (checkBox1.Checked)
            {
                dday = Convert.ToInt16(textBox1.Text);
                mmonth = Convert.ToInt16(textBox2.Text);
                yyear = Convert.ToInt16(textBox3.Text);
                hhour = Convert.ToInt16(textBox4.Text);
                mmin = Convert.ToInt16(textBox5.Text);
                ssec = Convert.ToDouble(textBox6.Text);
                if (2000 > yyear)
                {
                    MessageBox.Show("Неправильно введен год.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ifok = false;
                }
                if (mmonth > 12)
                {
                    MessageBox.Show("Неправильно введен месяц.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ifok = false;
                }
                else if (dday > mmon[mmonth])
                {
                    MessageBox.Show("Неправильно введен день.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ifok = false;
                }
                if (hhour > 23)
                {
                    MessageBox.Show("Неправильно введен час.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ifok = false;
                }
                if (mmin > 59)
                {
                    MessageBox.Show("Неправильно введены минуты.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ifok = false;
                }
                if (ssec >= 60)
                {
                    MessageBox.Show("Неправильно введены секунды.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ifok = false;
                }
                if (yyear == 2000 && mmonth == 1 && dday == 1 && hhour < 3 && green)
                {
                    MessageBox.Show("Неправильно введена дата.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ifok = false;
                }

                if (ifok)
                {
                    n = Math.DivRem(yyear - 2000, 4, out j);
                    if (j == 0 && mmonth > 2)
                        result = result + tday;
                    for (int i = 0; i < n; i++)
                        result = result + 1461 * tday;
                    for (int i = 0; i < j; i++)
                        result = result + tday * myear[i];
                    for (int i = 0; i < (mmonth - 1); i++)
                        result = result + tday * mmon[i];

                    result = result + (dday - 1) * tday;
                    result = result + hhour * thour;
                    result = result + mmin * 60;
                    result = result + ssec;
                }
            }
            else
            {
                result = Convert.ToDouble(textBox7.Text);
                if (result < 10800 && !green)
                {
                    MessageBox.Show("Неправильно введено время.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ifok = false;
                }
            }
            if (ifok)
            {
                if (!green)
                    result = result - 10800;
                intresult = Convert.ToInt32(result / 86400);
                d = intresult - 0.5;
                T = d / 36525;
                t = result - intresult * 86400;
                e0 = (84381.448 - 46.815 * T) * C;
                c1 = (450160.280 - 6962890.539 * T + 7.455 * T * T) * C;
                c2 = (-286322.15 + 129602771.27 * T + 1.089 * T * T) * C;
                c3 = (1287099.804 + 12959681.224 * T - 0.577 * T * T) * C;
                nu = (-0.83386013896E-4 * Math.Sin(c1) + 0.9996858E-6 * Math.Sin(2 * c1) - 0.6393238E-5 * Math.Sin(2 * c2) + 0.691344309E-6 * Math.Sin(c3)) * Math.Cos(e0);
                So = 1.753368559233 + 0.01720279180525 * d + 0.677E-5 * T * T + nu;
                ST = So + 0.729211585E-4 * t;
                k = Convert.ToInt32(ST / (2 * Math.PI));
                ST = ST - k * 2 * Math.PI;
                if (ST < 0)
                    ST = ST + 2 * Math.PI;

                STGR = ST;
                textBox11.Text = ST.ToString();
                ST = ST / C / 15;
                SThour = Convert.ToInt32(Math.Floor(ST / 3600));
                ST = ST - SThour * 3600;
                STmin = Convert.ToInt32(Math.Floor(ST / 60));
                STsec = ST - STmin * 60;
                textBox8.Text = SThour.ToString();
                textBox9.Text = STmin.ToString();
                textBox10.Text = STsec.ToString();
                STGR = STGR / C;
                textBox15.Text = Convert.ToString(STGR / 3600);
                STGG = Convert.ToInt32(Math.Floor(STGR / 3600));
                STGR = STGR - STGG * 3600;
                STGM = Convert.ToInt32(Math.Floor(STGR / 60));
                STGS = STGR - STGM * 60;
                textBox14.Text = STGG.ToString();
                textBox13.Text = STGM.ToString();
                textBox12.Text = STGS.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            spravka.Show();
        }                         







    }
}
