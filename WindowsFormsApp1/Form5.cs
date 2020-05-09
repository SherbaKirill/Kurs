using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form5 : Form
    {
        List<string> result;
        public Form5(string name,List<string> result)
        {
            this.result = result;
            InitializeComponent();
            label1.Text = name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string output = label1.Text + " TABLE ";
            bool success = true;
            if (textBox1.Text == null)
            {
                MessageBox.Show("Введите аргумент таблицы", "error", MessageBoxButtons.OK);
                success = false;
            }
            else output += textBox1.Text;
            if (textBox2.Text != null && double.TryParse(textBox2.Text, out double value))
            {
                output += ',' + value.ToString().Replace(',','.');
            }
            else
            {
                MessageBox.Show("Неправильный формат левой границы", "error", MessageBoxButtons.OK);
                success = false;
            }
            if (textBox3.Text != null && double.TryParse(textBox3.Text, out double value2))
                output += ',' + value2.ToString().Replace(',', '.');
            else
            {
                MessageBox.Show("Неправильный формат размера частотных интервалов", "error", MessageBoxButtons.OK);
                success = false;
            }
            if (textBox4.Text != null && int.TryParse(textBox4.Text, out int value3))
                output += ',' + value3.ToString();
            else
            {
                MessageBox.Show("Неправильный формат количества интервалов", "error", MessageBoxButtons.OK);
                success = false;
            }
            if (success)
            {
                result.Add(output);
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6(5);
        }
    }
}
