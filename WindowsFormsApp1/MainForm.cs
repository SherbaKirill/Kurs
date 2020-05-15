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
    public partial class MainForm : Form
    {
        List<Node> nodes;
        Form2 form2;
        Form3 form3;
        Form1 form1;
        Form4 form4;
        public MainForm()
        {
            InitializeComponent();
            form2 = new Form2();
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            if (form2.ShowDialog() == DialogResult.OK)
            {
                nodes = form2.nodes;
                this.button2.Enabled = true;
                form3 = new Form3(nodes);
                form1 = new Form1(nodes);
                form4 = new Form4(nodes);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {          
            if (form3.ShowDialog() == DialogResult.OK)
                this.button3.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {            
            if (form1.ShowDialog() == DialogResult.OK)
                this.button4.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            form4.ShowDialog();
        }

        
    }
}
