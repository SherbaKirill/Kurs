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
    public partial class Form4 : Form
    {
        List<Node> nodes;
        public Form4(List<Node>nodes)
        {
            this.nodes = nodes;
            InitializeComponent();
        }
        private void Form4_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.AllowUserToDeleteRows = true;
            dataGridView1.RowCount = nodes.Count() + 1;
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
                    dataGridView1.Rows[i].Cells[j].Value = '-';
            }
        }
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

            TextBox tb = (TextBox)e.Control;
            tb.KeyPress += new KeyPressEventHandler(tb_KeyPress);
        }
        void tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsLetterOrDigit(e.KeyChar)) && (e.KeyChar != '(')&& (e.KeyChar != ')') && (e.KeyChar != '|')&&e.KeyChar!=','&&e.KeyChar!='#'&& (e.KeyChar != '_'))
            {
                if (e.KeyChar != (char)Keys.Back)
                { e.Handled = true; }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                nodes[i].statistic = new List<string>();
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value!=null&&dataGridView1.Rows[i].Cells[j].Value.ToString() != "-")
                    {
                        string[] item = dataGridView1.Rows[i].Cells[j].Value.ToString().Split('|');
                        nodes[i].statistic.AddRange(item);
                    }
                }
            }
            Generator generator = new Generator();
            generator.nodes = nodes;
            List<string> output=generator.Main();
            OpenFileDialog OPF = new OpenFileDialog();
            OPF.Filter = "Файлы txt|*.txt";
            if (OPF.ShowDialog() == DialogResult.OK)
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(OPF.FileName, append: false))
                    foreach (string item in output)
                        sw.WriteLine(item);
            }
        }

        
        private void button3_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6(4);
            form6.Show();
        }
    }
}
