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
        Form2 form2;
        public Form4(Form2 form2)
        {
            this.form2 = form2;
            InitializeComponent();
        }
        bool continuation = false;
        private void Form4_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.AllowUserToDeleteRows = true;
            dataGridView1.RowCount = form2.nodes.Count() + 1;
            dataGridView1.ColumnCount = form2.nodes.Count();
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
            if (!(Char.IsLetterOrDigit(e.KeyChar)) && (e.KeyChar != '(')&& (e.KeyChar != ')') && (e.KeyChar != '|')&&e.KeyChar!=','&&e.KeyChar!='#')
            {
                if (e.KeyChar != (char)Keys.Back)
                { e.Handled = true; }
            }
        }

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(!continuation)
                Application.Exit();
            this.DialogResult = DialogResult.OK;
            continuation = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                form2.nodes[i].statistic = new List<string>();
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value!=null&&dataGridView1.Rows[i].Cells[j].Value.ToString() != "-")
                    {
                        string[] item = dataGridView1.Rows[i].Cells[j].Value.ToString().Split('|');
                        form2.nodes[i].statistic.AddRange(item);
                    }
                }
            }
            Generator generator = new Generator();
            generator.nodes = form2.nodes;
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

        private void button2_Click(object sender, EventArgs e)
        {
            continuation = true;
            this.Close();
        }
    }
}
