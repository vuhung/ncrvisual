using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO;
using System.Collections.ObjectModel;

namespace TestAlgo
{
    public class Controller
    {
        public string ProjectDirectory { get; set; }
        public Collection<Point> GetData()
        {
            int [][] a=new int[100][];
            char[] splitArr = { ' ' };
            int n = 0;
            ProjectDirectory = "output.txt";
            StreamReader reader = new StreamReader(ProjectDirectory);
            int i = 0;
            while (!reader.EndOfStream)
            {
                string s=reader.ReadLine();
                string[] r = s.Split(' ');
                n = r.Length;
                for (int j = 0; j < n; j++)
                    a[i][j] = Int16.Parse(r[j]) - 49;
            }
            return null;
        }
        

    }
}
