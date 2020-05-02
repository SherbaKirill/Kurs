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
        public class Node
        {
            public string character;
            public string law;
            public int numChanell;
            public string name;
            public int[] communication;
            public Node(string character,string law,string Name)
            {
                this.name = Name;
                this.character = character;
                this.law = law;
                communication = new int[_numNodes];
            }
            public Node(string character, string law, int numChanell,string Name)
            {
                this.name = Name;
                this.character = character;
                this.law = law;
                this.numChanell = numChanell;
                communication = new int[_numNodes];
            }

        }
        bool _character = false;
        int _numElem;
        public List<Node> nodes;
        static int _numNodes;
        static int _numDevice = 0;
        static int _numGT = 0;
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!_character)
            {
                if (maskedTextBox1.TextLength > 0)
                {
                    _numNodes = Convert.ToInt32(maskedTextBox1.Text);
                    nodes = new List<Node>();
                    _character = true;
                    _numElem = 1;
                    maskedTextBox1.Text = null;
                    label2.Visible = true;
                    radioButton1.Visible = true;
                    radioButton2.Visible = true;
                    radioButton3.Visible = true;
                    maskedTextBox1.Visible = false;
                    label1.Text = "Узел "+_numElem;                    
                }
            }
            else
            {
                if (radioButton1.Checked)
                {
                    Node node = new Node("Одноканал", "","b"+_numDevice);
                    _numDevice++;
                    nodes.Add(node);
                    label1.Text = "Узел " + ++_numElem;
                    radioButton1.Checked = false;
                }
                if(radioButton2.Checked)
                {
                    if (Convert.ToInt32(maskedTextBox1.Text) > 1)
                    {
                        Node node = new Node("Многоканал", "", Convert.ToInt32(maskedTextBox1.Text), "b" + _numDevice);
                        _numDevice++;
                        nodes.Add(node);
                        label1.Text = "Узел " + ++_numElem;
                        radioButton2.Checked = false;
                        label2.Text = null;
                        maskedTextBox1.Text = null;
                        maskedTextBox1.Visible = false;
                    }
                   else MessageBox.Show("Количество каналов больше 1","Error", MessageBoxButtons.OK);
                }
                if(radioButton3.Checked)
                {
                    if (Convert.ToInt32(maskedTextBox1.Text)==1||Convert.ToInt32(maskedTextBox1.Text)==2)
                    {
                        Node node;
                        if (Convert.ToInt32(maskedTextBox1.Text) == 1)
                            node = new Node("Генератор", "", "S" + _numGT);
                        else
                            node = new Node("Приемник", "", "S" + _numGT);
                        _numGT++;
                        nodes.Add(node);
                        label1.Text = "Узел " + ++_numElem;
                        radioButton3.Checked = false;
                        label2.Text = null;
                        maskedTextBox1.Text = null;
                        maskedTextBox1.Visible = false;
                    }
                   else  MessageBox.Show("1 или 2","Error", MessageBoxButtons.OK);
                }
                if(_numElem>_numNodes)
                {
                    Close();
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label2.Text = null;
            maskedTextBox1.Visible = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label2.Text = "Количество каналов больше 1";
            maskedTextBox1.Visible = true;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            label2.Text = "1-Generator 2-terminal";
            maskedTextBox1.Visible = true;
        }
    }
}
