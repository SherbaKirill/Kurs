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
  
    public partial class Form3 : Form
    {
        Form2 form2;
        bool continuation = false;
        public Form3(Form2 form2)
        {
            this.form2 = form2;
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            foreach (Node node in form2.nodes)
                if (node.type != 3)
                {
                    comboBox2.Items.Add(node.name);
                }
            comboBox2.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    label1.Text = "a";
                    label2.Text = "b";
                    label3.Visible = false;
                    maskedTextBox3.Visible = false;
                    label4.Visible = false;
                    comboBox3.Visible =false;
                    break;
                case 1:
                    label1.Text = "сдвиг";
                    label2.Text = "величина";
                    label4.Visible = true;
                    comboBox3.Visible = true;
                    label3.Visible = false;
                    maskedTextBox3.Visible = false;
                    break;
                case 2:
                    label1.Text = "мат.ожид";
                    label2.Text = "ср.кв.откл";
                    label3.Visible = false;
                    maskedTextBox3.Visible = false;
                    label4.Visible = true;
                    comboBox3.Visible = true;
                    break;
                case 3:
                    label1.Text = "min";
                    label2.Text = "max";
                    label4.Visible = true;
                    comboBox3.Visible = true;
                    label3.Visible = false;
                    maskedTextBox3.Visible = false;
                    break;
                case 4:
                    label1.Text = "min";
                    label2.Text = "max";
                    label4.Visible = true;
                    comboBox3.Visible = true;
                    label3.Visible = true;
                    maskedTextBox3.Visible = true;
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            int item=form2.nodes.FindIndex(x => x.name == this.comboBox2.SelectedItem.ToString());
            int law = this.comboBox1.SelectedIndex;
            if (form2.nodes[item].type == 1)
            {
                int result = Convert.ToInt32(maskedTextBox4.Text);
                if (result < 2)
                    MessageBox.Show("Количество каналов больше 2", "Error", MessageBoxButtons.OK);
                else form2.nodes[item].countOfChanell = result;
            }
            switch(law)
            {
                case 0:
                    form2.nodes[item].law = "UNIFORM(" + this.maskedTextBox1.Text + ',' + this.maskedTextBox2.Text + ')';
                    break;
                case 1:
                    form2.nodes[item].law = "Exponential(" + this.comboBox3.SelectedText + ',' + this.maskedTextBox1.Text + ','+this.maskedTextBox2.Text+')';
                    break;
                case 2:
                    form2.nodes[item].law = "NORMAL(" + this.comboBox3.SelectedText + this.maskedTextBox1.Text + ',' + this.maskedTextBox2.Text + ')';
                    break;
                case 3:
                    form2.nodes[item].law = "DUNIFORM(" + this.comboBox3.SelectedText + this.maskedTextBox1.Text + ',' + this.maskedTextBox2.Text + ')';
                    break;
                case 4:
                    form2.nodes[item].law = "TRIANGULAR(" + this.comboBox3.SelectedText + this.maskedTextBox1.Text + ',' + this.maskedTextBox2.Text +','+this.maskedTextBox3.Text +')';
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            continuation = true;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!continuation)
                Application.Exit();
            continuation = false;
            for (int i = comboBox2.Items.Count - 1; i >= 0; i--)
                comboBox2.Items.RemoveAt(i);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            continuation = true;
            this.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int item = form2.nodes.FindIndex(x => x.name == this.comboBox2.SelectedItem.ToString());
            if (form2.nodes[item].type == 1)
            {
                label5.Visible = true;
                maskedTextBox4.Visible = true;
            }
            else
            {
                label5.Visible = false;
                maskedTextBox4.Visible = false;
            }
        }
    }
}
