using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TestAlgo.Library;

namespace TestAlgo
{
    public partial class MainPage : UserControl
    {
        private Collection<EntityControl> _entityCollection;

        int[][] a = new int[100][];
        int n;
        private ScaleTransform _scale;

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainPage()
        {
            InitializeComponent();

            //getfile
            WebClient client = new WebClient();
            client.OpenReadAsync(new Uri("output.txt", UriKind.Relative));
            client.OpenReadCompleted += new OpenReadCompletedEventHandler(client_OpenReadCompleted);

            _entityCollection = new Collection<EntityControl>();
            _scale = new ScaleTransform();
            this.LayoutRoot.RenderTransform = _scale;
            CreateTestNode();            
        }

        /// <summary>
        /// Create a node of the graph ( entity Control )
        /// </summary>
        /// <param name="Display">The title of the node</param>
        /// <param name="left">The distance between the top-left of the node to the left of the browser</param>
        /// <param name="top">The distance between the top-left of the node to the top of the browser</param>
        public void CreateNode(string Display, double left, double top)
        {
            //Create the node control
            GenericEntity entity = new GenericEntity(Display);
            EntityControl ctrl = new EntityControl(entity);
            ctrl.Title = entity.Name;
            LayoutRoot.Children.Add(ctrl);
            _entityCollection.Add(ctrl);

            //Set the Anxis of the node
            Canvas.SetLeft(ctrl, left);
            Canvas.SetTop(ctrl, top);
        }

        //By Eledra
        #region TestNode
        //Create Test graph collection - Each Node have 50x50 size
        private void CreateTestNode()
        {
            for (int i = 0; i < 10; i++)
            {
                CreateNode(i.ToString(), i * 70, i * 50);
            }
        }
        #endregion

        //Do not touch this 
        #region ZoomSlider
        private void zoomSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.zoomSlider != null)
            {
                this.DoZoom();
            }
        }

        void DoZoom()
        {
            double factor = this.zoomSlider.Value / 50;
            this._scale.ScaleX = factor;
            this._scale.ScaleY = factor;
        }
        #endregion

        //By HellDevil
        #region LayoutAlgo

        void TestController()
        {

        }

        void client_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            StreamReader reader = new StreamReader(e.Result);
            int i = 0;
            while (!reader.EndOfStream)
            {
                a[i] = new int[100];
                string s = reader.ReadLine().Trim();
                string[] r = s.Split(' ');
                n = r.Length;
                for (int j = 0; j < n; j++)
                    a[i][j] = Int16.Parse(r[j]);
                i++;
            }
            reader.Close();
            //CreateRectangleNode();
        }

        void CreateTreeNode()
        {
            int[] save = new int[100];
            for (int i = 0; i < n; i++)
            {
                save[i] = 0;
            }
            for (int i = 0; i < n; i++)
            {
                int count = 0;
                for (int j = 0; j < n; j++)
                    if (a[i][j] > 0)
                        count++;
                CreateNode(i.ToString(), save[count] * 70, count * 50);
                save[count]++;
            }
        }

        void CreateRectangleNode()
        {
            double distance = 70;
            int vNum = (int)(Math.Ceiling(n / 4));
            double length = distance * vNum;
            double pre;
            int vTemp = vNum;
            if (n % 4 == 1)
                vTemp = vNum + 1;
            int count = 1;
            for (int i = 1; i <= vTemp; i++)
            {

                CreateNode(count.ToString(), i * distance, distance);
                count++;
            }
            pre = vTemp * distance + distance;
            vTemp = vNum;
            if (n % 4 == 2)
                vTemp = vNum + 1;
            for (int i = 1; i <= vTemp; i++)
            {
                CreateNode(count.ToString(), pre, i * distance);
                count++;
            }
            pre = vTemp * distance + distance;
            vTemp = vNum;

            if (n % 4 == 3)
                vTemp = vNum + 1;
            for (int i = vTemp + 1; i >= 2; i--)
            {
                CreateNode(count.ToString(), i * distance, pre);
                count++;
            }

            for (int i = vNum + 1; i >= 2; i--)
            {
                CreateNode(count.ToString(), distance, i * distance);
                count++;
            }

        }

        #endregion

    }
}
