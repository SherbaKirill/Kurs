using System;
using System.Collections.Generic;

namespace WindowsFormsApp1
{
    class Generator
    {
        public List<Node> nodes { get; set; }
        delegate List<string> Type(Node node);
        List<string> _sortList;
        string separator = "***************************";
        public List<string> Main()
        {
            _sortList = new List<string>();
            _sortList.Add(separator);
            Type[] function = new Type[] { MonoСhanell, PoliСhanell, Generate, Terminate };
            List<List<string>> noSortList = new List<List<string>>();
            foreach (Node item in nodes)
            {
                Qtable(item);
                noSortList.Add(function[item.type].Invoke(item));
            }
            _sortList.Add(separator);
            Treatment(noSortList);
            return _sortList;
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
            foreach (string item in node.statistic)
                if (item[0] == 'x')
                {
                    string[] arr = item.Split('(', ')');
                    result.Add("QUEUE " + arr[1] + " ;");
                }
            transfer(node, result);
            return result;
        }
        private List<string> Terminate(Node node)
        {
            List<string> result = new List<string>();
            result.Add("Label_" + node.name + " ");
            bool flag = false;
            foreach (string item in node.statistic)
            {
                if (item[0] == 'y')
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
                if(item[0]=='#')
                {
                    string[] arr = item.Split('(', ')');
                    if (!flag)
                    {
                        result[0]+="TABULATE " + arr[1] + " ;";
                        CreateTable(arr[1]);
                        flag = true;
                    }
                    else
                    {
                        result.Add("TABULATE " + arr[1] + " ;");
                        CreateTable(arr[1]);
                    }
                }
            }
            
            if (!flag)
                result[0] += "TERMINATE 1;";
            else result.Add("TERMINATE 1;");
            return result;
        }
        private void Qtable(Node node)
        {
            foreach(string item in node.statistic)
                if(item.Contains("Qt"))
                {
                    string[] arr = item.Split('(', ')');
                    string[] variable = arr[1].Split(',');
                    if(variable.Length==5)
                       _sortList.Add(variable[0] + " QTABLE " + variable[1] + ',' + variable[2] + ',' + variable[3] + ',' + variable[4]+" ;");
                }
        }
        private void before(List<string> result,Node node,out bool flag)
        {
            flag = false;
            foreach (string item in node.statistic)
            {
                string[] arr = item.Split('(', ')');
                string keyWord=null;
                bool create = false;
                if (item.Contains("x0"))
                {
                    keyWord = "QUEUE ";
                    create = true;
                }
                if (item.Contains("y0"))
                {
                    keyWord = "DEPART ";
                    create = true;
                }
                if (item.Contains("#0"))
                {
                    keyWord = "TABULATE ";
                    create = true;
                    CreateTable(arr[1]);
                }
                if (create)
                {
                    if (!flag)
                    {                        
                        result[0] += keyWord + arr[1] + " ;";
                        flag = true;
                    }
                    else
                        result.Add(keyWord + arr[1] + " ;");
                    create = false;
                }
            }
        }
        private void CreateTable(string item)
        {
            bool contains = false;
            foreach (string cheak in _sortList)
            {
                string[] arr = cheak.Split(' ');
                if (arr[0]==item)
                    contains = true;
            }
            if(!contains)
            {
                Form5 form5 = new Form5(item, _sortList);
                form5.ShowDialog();
            }
        }
        private void SeizeDepartTabulate(List<string> result,int x,Node node)
        {
            foreach (string item in node.statistic)
            {
                string[] arr = item.Split('(', ')');
                if (item.Contains("x" + x))                    
                    result.Add("QUEUE " + arr[1] + " ;");
                if (item.Contains("y" + x))
                    result.Add("DEPART " + arr[1] + " ;");
                if (item.Contains("#" + x))
                {
                    result.Add("TABULATE " + arr[1] + " ;");
                    CreateTable(arr[1]);
                }
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
            SeizeDepartTabulate(result, 1, node);
            result.Add("ADVANCE (" + node.law + ") ;");
            result.Add("RELEASE " + node.name + " ;");
            SeizeDepartTabulate(result, 2, node);
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
                        result.Add("TRANSFER  " + chance.ToString().Replace(',','.') + ",,Label_" + nodes[i].name + " ;");
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
            SeizeDepartTabulate(result, 1, node);
            result.Add("ADVANCE (" + node.law + ") ;");
            result.Add("LEAVE " + node.name);
            SeizeDepartTabulate(result, 2, node);
            transfer(node, result);
            return result;
        }
 
    }
}
