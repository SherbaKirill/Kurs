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
            Button button2 = (Button)sender;
            numOfElements++;
            countOfElements++;
            Label label = new Label();
            label.Text = 'b'+numOfElements.ToString();
            label.Location = new Point(0, button2.Location.Y);
            label.Size = new Size(20, 20);
            this.Controls.Add(label);
            ComboBox comboBox = new ComboBox();
            comboBox.Items.AddRange(new object[] { (object)"одноканальное устройство", (object)"многоканальное устройство", (object)"генератор", (object)"приемник" });
            comboBox.Location = new Point(30, button2.Location.Y);
            comboBox.Size = new Size(170, 20);
            comboBox.SelectedIndex = 0;
            comboBox.SelectedIndexChanged += SelectedIndexChanged;
            this.Controls.Add(comboBox);
            Button button = new Button();
            button.Text = "Удалить узел";
            button.Location = new Point(200, button2.Location.Y);
            button.Size = new Size(80,20);
            button.Click += Button_Click;
            this.Controls.Add(button);         
            button2.Location = new Point(button2.Location.X, button2.Location.Y + 30);
            this.button1.Location = new Point(this.button1.Location.X, this.button1.Location.Y- button2.Location.Y>20? this.button1.Location.Y: this.button1.Location.Y+30);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            int numOfRemove = this.Controls.IndexOf((Button)sender);
            countOfElements--;
            for (int i = 0; i <= 2; i++)
                this.Controls.Remove(this.Controls[numOfRemove - i]);
            for (int i = numOfRemove - 2; i < this.Controls.Count; i++)
            {
                Control control = this.Controls[i];
                control.Location = new Point(control.Location.X, control.Location.Y - 30);
            }
            this.button1.Location = new Point(this.button1.Location.X,this.button1.Location.Y - button2.Location.Y <30|| this.button1.Location.Y - button2.Location.Y > 150 ? this.button1.Location.Y -30: this.button1.Location.Y);
            this.button2.Location = new Point(this.button2.Location.X, this.button2.Location.Y - 30);
        }

        private void SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            Label label=(Label)this.Controls[this.Controls.IndexOf(comboBox)-1];
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
                Label label = (Label)this.Controls[2 + i * 3];
                node.name = label.Text;
                ComboBox box = (ComboBox)this.Controls[3 + i * 3];
                node.type = box.SelectedIndex;
                if (node.type == 1)
                    node.countOfChanell = 2;
                if (node.type == 2)
                    generator = true;
                if (node.type == 3)
                    terminate = true;
                node.communication = new double[countOfElements];
                node.law= "UNIFORM(0,0)";
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
