using System.Windows.Controls;
using TestAlgo.Library;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows;

namespace TestAlgo
{
    public partial class MainPage : UserControl
    {
        private Collection<EntityControl> _entityCollection;
        private ScaleTransform _scale;

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
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

        //Create Test graph collection - Each Node have 50x50 size
        private void CreateTestNode()
        {
            for (int i = 0; i < 10; i++)
            {
                CreateNode(i.ToString(), i * 70, i * 10);
            }
        }

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

    }
}
