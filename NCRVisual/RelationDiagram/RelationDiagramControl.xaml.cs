using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NCRVisual.RelationDiagram
{
    public partial class RelationDiagramControl : UserControl
    {
        private Collection<EntityControl> _entityControlCollection;        
    
        private ScaleTransform _scale;

        DiagramController _myDiagramController;

        /// <summary>
        /// Default constructor
        /// </summary>
        public RelationDiagramControl()
        {
            InitializeComponent();
           
            _entityControlCollection = new Collection<EntityControl>();
            _scale = new ScaleTransform();
            this.LayoutRoot.RenderTransform = _scale;

            _myDiagramController = new DiagramController();
            _myDiagramController.InputReadingComplete += new EventHandler(_myDiagramController_InputReadingComplete);            
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
            ctrl.Title = Display;
            
            ctrl.Width = ctrl.DesiredWidth;
            ctrl.Height = ctrl.DesiredHeight;
           
            LayoutRoot.Children.Add(ctrl);
            _entityControlCollection.Add(ctrl);

            //Set the Anxis of the node
            Canvas.SetLeft(ctrl, left);
            Canvas.SetTop(ctrl, top);
        }

        /// <summary>
        /// Draw connection between 2 entity control
        /// </summary>
        /// <param name="startingPoint">Source entity</param>
        /// <param name="endPoint">Final entity</param>
        public void drawConnection(EntityControl startingPoint, EntityControl endPoint)
        {
            EdgeControl edge = new EdgeControl(startingPoint,endPoint);
            edge.drawEdge();
            LayoutRoot.Children.Add(edge);
        }
               
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

        #region EventHandler

        /// <summary>
        /// Event handler after reading input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _myDiagramController_InputReadingComplete(object sender, EventArgs e)
        {
            Collection<Point> pointPositions = (Collection<Point>)sender;

            //Generate node
            for (int i = 0; i < pointPositions.Count; i++)
            {
                CreateNode(_myDiagramController.entityCollection[i].Email, pointPositions[i].X, pointPositions[i].Y);

                //Test connection                
                //drawConnection(_entityCollection[0], _entityCollection[i]);
            }                             
        }
        #endregion

    }
}
