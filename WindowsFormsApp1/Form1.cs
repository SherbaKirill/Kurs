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
    public partial class Form1 : Form
    {
        bool continuation = true;
        Form2 form2 = new Form2();
        Form3 form3;
        Form4 form4;
        public Form1( )
        {
            InitializeComponent();
            Form2_Call();
            form3 = new Form3(form2);
            form4 = new Form4(form2);
            Form3_Call();
        }
        private void Form2_Call()
        {
            form2.ShowDialog();
            if(form3!=null)
             Form3_Call();
        }
        private void Form3_Call()
        {
            form3.ShowDialog();
            if (DialogResult.OK == form3.DialogResult)
                Form2_Call();
            this.Visible = true;
            Form1_Load(this,new EventArgs());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.AllowUserToDeleteRows = true;
            dataGridView1.RowCount = form2.nodes.Count()+1;
            dataGridView1.ColumnCount =form2.nodes.Count();
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Columns[i].HeaderCell.Value = form2.nodes[i].name;
                dataGridView1.Rows[i].HeaderCell.Value = form2.nodes[i].name;
            }
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    dataGridView1.Rows[i].Cells[j].Value = form2.nodes[i].communication[j];
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                double sum = 0;
                for (int j = 0; j < dataGridView1.ColumnCount; j++) {
                    if (dataGridView1.Rows[i].Cells[j].Value!=null)
                    {
                        if (double.TryParse(dataGridView1.Rows[i].Cells[j].Value.ToString(), out double value))
                        {
                            sum += value;
                            form2.nodes[i].communication[j] = value;
                        }
                        else
                        {
                            MessageBox.Show("Вводить разрешенно положительные числа от 0 до 1 строка " + (i + 1) + " столбец " + (j + 1), "Error", MessageBoxButtons.OK);
                            continuation = false;
                        }
                    }
                }
                if(sum!=1)
                {
                    MessageBox.Show("Сумма всех соединенией должно равняться 1 строка "+(i+1), "Error", MessageBoxButtons.OK);
                    continuation = false;
                }
            }
            if (continuation)
            {
                this.Visible = false;
                if (form4.ShowDialog() == DialogResult.OK)
                    this.Visible = true;
            }
            continuation = true;
        }
        

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            
            TextBox tb = (TextBox)e.Control;
            tb.KeyPress += new KeyPressEventHandler(tb_KeyPress);
        }
        void tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)) && (e.KeyChar != ','))
            {
                if (e.KeyChar != (char)Keys.Back)
                { e.Handled = true; }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Form3_Call();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form6 form6= new Form6(1);
            form6.Show();
        }
    }
}
