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
        delegate List<string> Type(Node node);
        List<string> _sortList;
        string separator = "############################";
        public void Main()
        {
            _sortList = new List<string>();
            _sortList.Add(separator);
            Type[] function = new Type[] { MonoСhanell, PoliСhanell, Generate, Terminate };
            List<List<string>> noSortList = new List<List<string>>();
            foreach (Node item in nodes)
                noSortList.Add(function[item.type].Invoke(item));
            _sortList.Add(separator);
            Treatment(noSortList);
        }
        private void Treatment(List<List<string>> noSort)
        {
            foreach (List<string> item in noSort)
            {
                if (item[0].Contains("GENERATE"))
                    _sortList.AddRange(item);
            }
            foreach (List<string> item in noSort)
            {
                if(item[0].Contains("Label_b"))
                    _sortList.AddRange(item);
            }
            foreach(List<string> item in noSort)
            {
                if(item[item.Count-1].Contains("TERMINATE"))
                _sortList.AddRange(item);
            }
        }
        private List<string> Generate(Node node)
        {
            List<string> result = new List<string>();
            result.Add("GENERATE (" + node.law + ") ;");
            transfer(node, result);
            return result;
        }
        private List<string> Terminate(Node node)
        {
            List<string> result = new List<string>();
            result.Add("Label_" + node.name+ " TERMINATE 1 ;");
            return result;
        }
        private void before(List<string> result,Node node,out bool flag)
        {
            flag = false;
            foreach (string item in node.statistic)
            {
                if (item.Contains("x0"))
                {
                    if (!flag)
                    {
                        string[] arr = item.Split('(', ')');
                        result[0] += "QUEUE " + arr[1] + " ;";
                        flag = true;
                    }
                    else
                    {
                        string[] arr = item.Split('(', ')');
                        result.Add("QUEUE " + arr[1] + " ;");
                    }
                }
                if (item.Contains("y0"))
                {
                    if (!flag)
                    {
                        string[] arr = item.Split('(', ')');
                        result[0] += "DEPART " + arr[1] + " ;";
                        flag = true;
                    }
                    else
                    {
                        string[] arr = item.Split('(', ')');
                        result.Add("DEPART " + arr[1] + " ;");
                    }
                }
            }
        }
        private void SeizeDepart(List<string> result,int x,Node node)
        {
            foreach (string item in node.statistic)
                if (item.Contains("x"+x))
                {
                    string[] arr = item.Split('(', ')');
                    result.Add("QUEUE " + arr[1] + " ;");
                }
            foreach (string item in node.statistic)
                if (item.Contains("y"+x))
                {
                    string[] arr = item.Split('(', ')');
                    result.Add("DEPART " + arr[1] + " ;");
                }
        }
        private List<string> MonoСhanell(Node node)
        {
            List<string> result = new List<string>();
            result.Add("Label_" + node.name + " ");
            before(result, node, out bool flag);
            if (flag)
                result.Add("SEIZE " + node.name + " ;");
            else result[0] += "SEIZE " + node.name + " ;";
            SeizeDepart(result, 1, node);
            result.Add("ADVANCE (" + node.law + ") ;");
            result.Add("RELEASE " + node.name + " ;");
            SeizeDepart(result, 1, node);
            transfer(node, result);
            return result;
        }
        private void transfer(Node node,List<string>result)
        {
            double sum = 1;
            for (int i = 0; i < nodes.Count; i++)
            {
                if (node.communication[i] != (double)0)
                {
                    double chance = Math.Round(node.communication[i] / sum, 4);
                    if (chance < 1)
                    {
                        result.Add("TRANSFER  " + chance + ",,Label_" + nodes[i].name + " ;");
                        sum *= (1 - chance);
                    }
                    else
                    {
                        result.Add("TRANSFER  , Label_" + nodes[i].name + " ;");
                        break;
                    }
                }
            }
        }
        private List<string> PoliСhanell(Node node)
        {
            List<string> result = new List<string>();
            _sortList.Add(node.name + " STORAGE " + node.countOfChanell + " ;");
            result.Add("Label_" + node.name + " ");
            before(result, node, out bool flag);
            if (flag)
                result.Add("ENTER " + node.name + " ;");
            else result[0] += "ENTER " + node.name + " ;";
            SeizeDepart(result, 1, node);
            result.Add("ADVANCE (" + node.law + ") ;");
            result.Add("LEAVE " + node.name);
            SeizeDepart(result, 2, node);
            transfer(node, result);
            return result;
        }
 
    }
}
