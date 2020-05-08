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
    public partial class Form2 : Form
    {
       
        public List<Node> nodes;
        int numOfElements = 0;
        int countOfElements = 0;
        bool continuation = false;
        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int positionY;
            if (this.panel1.Controls.Count == 0)
                positionY = -20;
            else positionY = this.panel1.Controls[this.panel1.Controls.Count - 1].Location.Y;
            numOfElements++;
            countOfElements++;
            Label label = new Label();
            label.Text = 'b'+numOfElements.ToString();
            label.Location = new Point(0, positionY+30);
            label.Size = new Size(30, 20);
            this.panel1.Controls.Add(label);
            ComboBox comboBox = new ComboBox();
            comboBox.Items.AddRange(new object[] { (object)"одноканальное устройство", (object)"многоканальное устройство", (object)"генератор", (object)"приемник" });
            comboBox.Location = new Point(30, positionY + 30);
            comboBox.Size = new Size(170, 20);
            comboBox.SelectedIndex = 0;
            comboBox.SelectedIndexChanged += SelectedIndexChanged;
            this.panel1.Controls.Add(comboBox);
            Button button = new Button();
            button.Text = "Удалить узел";
            button.Location = new Point(200, positionY + 30);
            button.Size = new Size(80,20);
            button.Click += Button_Click;
            this.panel1.Controls.Add(button);
            this.panel1.VerticalScroll.Value = this.panel1.VerticalScroll.Maximum;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            int numOfRemove = this.panel1.Controls.IndexOf((Button)sender);
            countOfElements--;
            for (int i = 0; i <= 2; i++)
                this.panel1.Controls.Remove(this.panel1.Controls[numOfRemove - i]);
            for (int i = numOfRemove - 2; i < this.panel1.Controls.Count; i++)
            {
                Control control = this.panel1.Controls[i];
                control.Location = new Point(control.Location.X, control.Location.Y - 30);
            }
        }

        private void SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            Label label=(Label)this.panel1.Controls[this.panel1.Controls.IndexOf(comboBox)-1];
            char symb;
            if (comboBox.SelectedIndex < 2)
                symb = 'b';
            else symb = 'S';
            label.Text = symb +label.Text[1].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool generator = false;
            bool terminate = false;         
            nodes = new List<Node>();
            for(int i=0;i<countOfElements;i++)
            {
                Node node = new Node();
                Label label = (Label)this.panel1.Controls[0 + i * 3];
                node.name = label.Text;
                ComboBox box = (ComboBox)this.panel1.Controls[1 + i * 3];
                node.type = box.SelectedIndex;
                if (node.type == 1)
                    node.countOfChanell = 2;
                if (node.type == 2)
                    generator = true;
                if (node.type == 3)
                    terminate = true;
                node.communication = new double[countOfElements];
                node.law= "UNIFORM(0,0)";
                node.statistic = new List<string>();
                nodes.Add(node);
            }
            if (generator && terminate)
            {
                continuation = true;
                this.Close();
            }
            else MessageBox.Show("Должны быть как минимум один приемник и один генератор", "Error", MessageBoxButtons.OK);
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!continuation)
                Application.Exit();
            continuation = false;
        }
    }
}
