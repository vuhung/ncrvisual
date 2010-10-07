using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace NCRVisual.RelationDiagram
{
    public partial class RelationDiagramControl : UserControl
    {       
        DiagramController _myDiagramController;

        private Collection<EntityControl> _entityControlCollection;
        private ScaleTransform _scale;                
        private Point _mouseStartingPoint;
        private bool _mouseLeftButtonDown;
        private StarField _starField;
        private DateTime _lastUpdate = DateTime.Now;
        private ColorfulFireworks _colourfulFirework;                

        /// <summary>
        /// Dictionary for maping entity and entityControl
        /// </summary>
        public Dictionary<IEntity, EntityControl> EntityRegistry { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public  RelationDiagramControl()
        {
            InitializeComponent();

            _entityControlCollection = new Collection<EntityControl>();
            EntityRegistry = new Dictionary<IEntity, EntityControl>();
            _scale = new ScaleTransform();

            _myDiagramController = new DiagramController();
            _myDiagramController.InputReadingComplete += new EventHandler(_myDiagramController_InputReadingComplete);

            MainGrid.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(MainGrid_MouseLeftButtonDown);

            MainGrid.MouseMove += new System.Windows.Input.MouseEventHandler(CircleField_MouseMove);

            // add the controls to the back          
            _colourfulFirework = new ColorfulFireworks();
            MainGrid.Children.Add(_colourfulFirework);
            _colourfulFirework.Start();    
        }

        void MainGrid_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Store the mouse position
            _mouseStartingPoint = e.GetPosition(MainGrid);
            _mouseLeftButtonDown = true;

            Point midPoint = new Point(this.MainGrid.ActualWidth / 2, this.MainGrid.ActualHeight / 2);
            Point difference = new Point(midPoint.X - _mouseStartingPoint.X, midPoint.Y - _mouseStartingPoint.Y);

            double currentLeft = Canvas.GetLeft(this.LayoutRoot);
            double currentTop = Canvas.GetTop(this.LayoutRoot);

            double newLeft = currentLeft + difference.X;
            double newTop = currentTop + difference.Y;

            this.leftAnimation.To = newLeft;
            this.topAnimation.To = newTop;

            this.leftAnimationStoryboard.Begin();
            this.topAnimationStoryboard.Begin();

        }

        void LayoutRoot_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            // Get the current mouse position

            if (this._mouseLeftButtonDown)
            {

                Point mousePos = e.GetPosition(null);
                //Point diff = new Point(_mouseStartingPoint.X - mousePos.X, _mouseStartingPoint.Y - mousePos.Y);                
                //_translate.X = diff.X;
                //_translate.Y = diff.Y;
                //this.RenderTransform = _translate;                
            }
        }

        void LayoutRoot_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            
        }

        void LayoutRoot_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this._mouseLeftButtonDown = false;
        }

        /// <summary>
        /// Create a node of the graph ( entity Control )
        /// </summary>
        /// <param name="Display">The title of the node</param>
        /// <param name="left">The distance between the top-left of the node to the left of the browser</param>
        /// <param name="top">The distance between the top-left of the node to the top of the browser</param>
        public void CreateNode(IEntity entity, double left, double top)
        {
            //Create the node control            
            EntityControl ctrl = new EntityControl(entity, this.EntityRegistry);
            //ctrl.Title = entity.Name;            
            ctrl.Title = "1";
            ctrl.Width = ctrl.DesiredWidth;
            ctrl.Height = ctrl.DesiredHeight;

            LayoutRoot.Children.Add(ctrl);
            _entityControlCollection.Add(ctrl);

            EntityRegistry.Add(entity, ctrl);

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
            EdgeControl edge = new EdgeControl(startingPoint, endPoint);
            startingPoint.ConnectedEdges.Add(edge);
            endPoint.ConnectedEdges.Add(edge);
            edge.drawEdge();
            LayoutRoot.Children.Insert(LayoutRoot.Children.IndexOf(_entityControlCollection[0]),edge);
        }

        //Do not touch this 
        #region ZoomSlider
        //private void zoomSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //    if (this.zoomSlider != null)
        //    {
        //        this.DoZoom();
        //    }
        //}

        //void DoZoom()
        //{
        //    this.LayoutRoot.RenderTransform = _scale;
        //    double factor = this.zoomSlider.Value / 50;
        //    this._scale.ScaleX = factor;
        //    this._scale.ScaleY = factor;
        //}
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
            DrawCircular(pointPositions);

            ////Generate node
            //for (int i = 0; i < pointPositions.Count; i++)
            //{
            //    CreateNode(_myDiagramController.entityCollection[i], pointPositions[i].X, pointPositions[i].Y);
            //}

            ////Generate Connection
            //for (int i = 0; i < _entityControlCollection.Count; i++)
            //{
            //    foreach (IEntity entity in _entityControlCollection[i].Entity.Connections)
            //    {
            //        drawConnection(_entityControlCollection[i], EntityRegistry[entity]);
            //    }
            //}
        }
        #endregion


        void CircleField_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this._colourfulFirework.GoFire(e.GetPosition(this).X, e.GetPosition(this).Y);
        }

        private void DrawCircular(Collection<Point> pointPositions)
        {

            double MiddleX = panelStarfield.ActualWidth / 2.0;
            double MiddleY = panelStarfield.ActualHeight / 2.0;            

            for (int i = 0; i < pointPositions.Count; i++)
            {
                double celcius = 360.0 / pointPositions.Count;

                Block block = new Block();
                LayoutRoot.Children.Add(block);
                Canvas.SetLeft(block, MiddleX);
                Canvas.SetTop(block, MiddleY);

                block.TextField.Text = _myDiagramController.entityCollection[i].Email;
                block.Foreground = new SolidColorBrush(Colors.White);

                RotateTransform Rotate = new RotateTransform();
                Rotate.Angle = i * celcius;

                TranslateTransform translate = new TranslateTransform();
                translate.X = 150 * Math.Cos(-i * celcius * Math.PI / 180);
                translate.Y = -150 * Math.Sin(-i * celcius * Math.PI / 180);

                TransformGroup grouptransform = new TransformGroup();
                grouptransform.Children.Add(Rotate);
                grouptransform.Children.Add(translate);

                block.RenderTransform = grouptransform;
            }
        }

        private static bool _initializedAfterScreenSizeChanged = false;

        private void InitIfNeededAfterScreenSizeIsKnown()
        {
            if (_initializedAfterScreenSizeChanged) return;
            _initializedAfterScreenSizeChanged = true;

            _starField = new StarField(panelStarfield);
            _lastUpdate = DateTime.Now;            
            CompositionTarget.Rendering += new EventHandler(CompositionTarget_Rendering);
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            double msec = (now - _lastUpdate).TotalMilliseconds;
            if (msec == 0)
            {
                return;
            }

            _starField.UpdateStars(msec);

            _lastUpdate = DateTime.Now;
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Globals.ScreenWidth = MainGrid.ActualWidth;
            Globals.ScreenHeight = MainGrid.ActualHeight;

            InitIfNeededAfterScreenSizeIsKnown();
        }
    }
}
