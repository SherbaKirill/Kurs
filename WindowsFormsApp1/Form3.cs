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
        List<Node> nodes;
        public Form3(List<Node> nodes)
        {
            this.nodes = nodes;
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            foreach (Node node in nodes)
                if (node.type != 3)
                {
                    comboBox2.Items.Add(node.name);
                }
            comboBox2.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;
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
                    maskedTextBox5.Visible =false;
                    break;
                case 1:
                    label1.Text = "сдвиг";
                    label2.Text = "величина";
                    label4.Visible = true;
                    maskedTextBox5.Visible = true;
                    label3.Visible = false;
                    maskedTextBox3.Visible = false;
                    break;
                case 2:
                    label1.Text = "min";
                    label2.Text = "max";
                    label4.Visible = true;
                    maskedTextBox5.Visible = true;
                    label3.Visible = false;
                    maskedTextBox3.Visible = false;
                    break;
                case 3:
                    label1.Text = "min";
                    label2.Text = "max";
                    label4.Visible = true;
                    maskedTextBox5.Visible = true;
                    label3.Visible = true;
                    maskedTextBox3.Visible = true;
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            int item=nodes.FindIndex(x => x.name == this.comboBox2.SelectedItem.ToString());
            int law = this.comboBox1.SelectedIndex;
            if (nodes[item].type == 1)
            {
                int result = Convert.ToInt32(maskedTextBox4.Text);
                if (result < 2)
                    MessageBox.Show("Количество каналов больше 2", "Error", MessageBoxButtons.OK);
                else nodes[item].countOfChanell = result;
            }
            if (this.maskedTextBox1.Text != "" && this.maskedTextBox2.Text != "")
                switch (law)
                {
                    case 0:
                        if (int.Parse(this.maskedTextBox1.Text) < int.Parse(this.maskedTextBox2.Text))
                            nodes[item].law = "UNIFORM(" + this.maskedTextBox1.Text + ',' + this.maskedTextBox2.Text + ')';
                        else MessageBox.Show("Неверный формат", "error", MessageBoxButtons.OK);
                        break;
                    case 1:
                        if (this.maskedTextBox5.Text != "" && int.Parse(this.maskedTextBox5.Text) > 0)
                            nodes[item].law = "Exponential(" + (this.maskedTextBox5.Text) + ',' + this.maskedTextBox1.Text + ',' + this.maskedTextBox2.Text + ')';
                        else MessageBox.Show("Неверный формат", "error", MessageBoxButtons.OK);
                        break;
                    case 2:
                        if (this.maskedTextBox5.Text != "" && int.Parse(this.maskedTextBox5.Text) > 0 && int.Parse(this.maskedTextBox1.Text) < int.Parse(this.maskedTextBox2.Text))
                            nodes[item].law = "DUNIFORM(" + (this.maskedTextBox5.Text) + ',' + this.maskedTextBox1.Text + ',' + this.maskedTextBox2.Text + ')';
                        else MessageBox.Show("Неверный формат", "error", MessageBoxButtons.OK);
                        break;
                    case 3:
                        if (this.maskedTextBox5.Text != "" && int.Parse(this.maskedTextBox5.Text) > 0&& int.Parse(this.maskedTextBox1.Text) < int.Parse(this.maskedTextBox2.Text))
                            nodes[item].law = "TRIANGULAR(" + (this.maskedTextBox5.Text) + ',' + this.maskedTextBox1.Text + ',' + this.maskedTextBox2.Text + ',' + this.maskedTextBox3.Text + ')';
                        else MessageBox.Show("Неверный формат", "error", MessageBoxButtons.OK);
                        break;
                }
            else MessageBox.Show("Поля не могут быть пустыми", "error", MessageBoxButtons.OK);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            for (int i = comboBox2.Items.Count - 1; i >= 0; i--)
                comboBox2.Items.RemoveAt(i);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int item = nodes.FindIndex(x => x.name == this.comboBox2.SelectedItem.ToString());
            if (nodes[item].type == 1)
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

        private void button4_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6(3);
            form6.Show();
        }
    }
}
