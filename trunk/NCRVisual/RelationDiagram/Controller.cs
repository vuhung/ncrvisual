using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Net;
using NCRVisual.RelationDiagram.Algo;

namespace NCRVisual.RelationDiagram
{
    public class DiagramController
    {
        /// <summary>
        /// Event after reading input from data provider
        /// </summary>
        public EventHandler InputReadingComplete;

        /// <summary>
        /// Get or set the LayoutAlgorithm 
        /// </summary>
        public IAlgorithm LayoutAlgorithm { get; set; }


        /// <summary>
        /// Create new instance of Diagram controller
        /// </summary>
        public DiagramController()
        {
            LayoutAlgorithm = new RectangleAlgorithm();

            //getfile from output.txt
            WebClient client = new WebClient();
            client.OpenReadAsync(new Uri("..\\Output\\output.txt", UriKind.Relative));
            client.OpenReadCompleted += new OpenReadCompletedEventHandler(client_OpenReadCompleted);
        }

        void client_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            int[][] _input = new int[100][];
            int _vertexNumber = 0;

            StreamReader reader = new StreamReader(e.Result);
            int i = 0;
            while (!reader.EndOfStream)
            {
                //Read line by line
                _input[i] = new int[100];
                string s = reader.ReadLine().Trim();
                string[] r = s.Split(' ');
                _vertexNumber = r.Length;
                for (int j = 0; j < _vertexNumber; j++)
                {
                    _input[i][j] = Int16.Parse(r[j]);
                }
                i++;
            }
            reader.Close();

            Collection<Point> tempPoints = LayoutAlgorithm.RunAlgo(_input, _vertexNumber);

            if (this.InputReadingComplete != null)
            {
                this.InputReadingComplete(tempPoints, null);
            };
        }
    }
}
