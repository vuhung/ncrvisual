using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace NCRVisual.RelationDiagram
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
