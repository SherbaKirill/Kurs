using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Generator
    {
        public List<Node> nodes { get; set; }
        public void Main()
        {
            List<List<string>> noSortList = new List<List<string>>();
            foreach(Node item in nodes)
                switch(item.type)
                {
                    case 2:
                        {
                            noSortList.Add(generate(item));
                            break;
                        }
                    case 3:
                        {
                            noSortList.Add(terminate(item));
                            break;
                        }
                }
        }
        private List<string> generate(Node node)
        {
            List<string> result = new List<string>();
            result.Add("GENERATE (" + node.law + ") ;");
            double sum = 1;
            for(int i=0;i<nodes.Count;i++)
            {
                if(node.communication[i]!=(double)0)
                {
                    double chance = Math.Round(node.communication[i]/sum,4);
                    if (chance < 1)
                    {
                        result.Add("TRANSFER  " + chance + ",,Label" + nodes[i].name+" ;");
                        sum *=(1- chance);
                    }
                    else
                    {
                        result.Add("TRANSFER  , Label ;" + nodes[i].name);
                        break;
                    }
                }
            }
            return result;
        }
        private List<string> terminate(Node node)
        {
            List<string> result = new List<string>();



            result.Add("TERMINATE 1 ;");
            return result;
        }
 
    }
}
