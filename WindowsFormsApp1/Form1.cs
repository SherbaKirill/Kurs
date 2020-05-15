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
        List<Node> nodes;
        public Form1(List<Node> nodes )
        {
            this.nodes = nodes;
            InitializeComponent();            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.AllowUserToDeleteRows = true;
            dataGridView1.RowCount = nodes.Count()+1;
            dataGridView1.ColumnCount =nodes.Count();
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Columns[i].HeaderCell.Value = nodes[i].name;
                dataGridView1.Rows[i].HeaderCell.Value = nodes[i].name;
            }
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    dataGridView1.Rows[i].Cells[j].Value =nodes[i].communication[j];
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
                            nodes[i].communication[j] = value;
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
                this.DialogResult = DialogResult.OK;
                this.Close();
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

       
        private void button3_Click(object sender, EventArgs e)
        {
            Form6 form6= new Form6(1);
            form6.Show();
        }
    }
}
