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
    public partial class DateTime : Form
    {
        private spr1 spravka = new spr1();

        int[] mmon = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        int[] myear = { 366, 365, 365, 365 };
        bool green1,green2,ifok;
        int tday = 86400;
        int thour = 3600;

        public DateTime()
        {
            InitializeComponent();
            green1 = checkBox1.Checked;
            AddOwnedForm(spravka);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Owner.Show();
            Hide();
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

        private void button1_Click(object sender, EventArgs e)
        {
            int dday,mmonth,yyear,hhour,mmin,epoh,j,n;               
            double ssec;
            double result = 0;
            ifok = true;
            dday = Convert.ToInt16(textBox1.Text);
            mmonth = Convert.ToInt16(textBox2.Text);
            yyear = Convert.ToInt16(textBox3.Text);
            hhour = Convert.ToInt16(textBox4.Text);
            mmin = Convert.ToInt16(textBox5.Text);
            ssec = Convert.ToDouble(textBox6.Text);
            epoh = Convert.ToInt16(textBox7.Text);

            if (epoh > yyear)
            {
                MessageBox.Show("Неправильно введен год или эпоха.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (yyear == epoh && mmonth == 1 && dday == 1 && hhour < 3 && green1)
            {
                MessageBox.Show("Неправильно введена дата.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ifok = false;
            }

            if (ifok)
            {
                n = Math.DivRem(yyear - epoh, 4, out j);
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

                if (!green1)
                    result = result + 10800;

                textBox8.Text = result.ToString();
            }
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            green1 = checkBox1.Checked;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int dday, mmonth, yyear, hhour, mmin, epoh, i;            
            double ssec, fullsec, temp;
            fullsec = Convert.ToDouble(textBox8.Text);
            epoh = Convert.ToInt16(textBox7.Text);
            temp = fullsec;
            if (!green2)
                temp = temp - 10800;

            yyear = epoh;
            mmonth = 1;
            dday = 1;
            hhour = 0;
            mmin = 0;
            
            while ((temp - tday * 1461) >= 0)
            {
                yyear = yyear + 4;
                temp = temp - tday * 1461;
            }
            
            i = 0;
            while ((temp - tday * myear[i]) >= 0)
            {
                yyear++;
                temp = temp - tday * myear[i];
                i++;                
            }
            
            i = 0;
            while ((temp - tday * mmon[i]) >= 0)
            {
                mmonth++;
                temp = temp - tday * mmon[i];
                i++;
            }            
           
            while ((temp - tday) >= 0)
            {
                dday++;
                temp = temp - tday;
            }

            while ((temp - thour) >= 0)
            {
                hhour++;
                temp = temp - thour;
            }

            while ((temp - 60) >= 0)
            {
                mmin++;
                temp = temp - 60;
            }

            ssec = temp;

            textBox1.Text = dday.ToString();
            textBox2.Text = mmonth.ToString();
            textBox3.Text = yyear.ToString();
            textBox4.Text = hhour.ToString();
            textBox5.Text = mmin.ToString();
            textBox6.Text = ssec.ToString();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            spravka.Show();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            green2 = checkBox2.Checked;
        }




    }
}
