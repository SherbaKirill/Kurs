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
        Form2 form2 = new Form2();
        public Form1( )
        {
            InitializeComponent();
            form2.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            dataGridView1.RowCount = form2.nodes.Count();
            dataGridView1.ColumnCount =dataGridView1.RowCount;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Columns[i].HeaderCell.Value = form2.nodes[i].name;
                dataGridView1.Rows[i].HeaderCell.Value = form2.nodes[i].name;
            }
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    dataGridView1.Rows[i].Cells[j].Value = 0;
            }
            
        }
    }
}
