using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Node
    {
        public int type { get; set; }
        public string law { get; set; }
        public int countOfChanell { get; set; }
        public string name { get; set; }
        public double[] communication { get; set; }
        public List<string> statistic { get; set; }
    }
}
