﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using NCRVisual.RelationDiagram.Algo;
using System.Windows.Shapes;

namespace NCRVisual.RelationDiagram
{
    public partial class RelationDiagramControl : UserControl
    {
        private int count = 0;

        //Private variable for render
        private DiagramController _myDiagramController;
        private Collection<EntityControl> _entityControlCollection;
        private ScaleTransform _scale;

        //Private variable for handling background and mouse
        private StarField _starField;
        private DateTime _lastUpdate = DateTime.Now;
        private ColorfulFireworks _colourfulFirework;

        private bool _mouseDown = false;
        //private double _velocity = 0;        
        private Point _mouseDownPoint = new Point();
        private Point _lastMouseDownPoint = new Point();

        /// <summary>
        /// Dictionary for maping entity and entityControl
        /// </summary>
        public Dictionary<IEntity, EntityControl> EntityRegistry { get; set; }

        /// <summary>
        /// Collection of pointpositions
        /// </summary>
        public Collection<Point> PointPositions { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public RelationDiagramControl()
        {
            InitializeComponent();

            _entityControlCollection = new Collection<EntityControl>();
            EntityRegistry = new Dictionary<IEntity, EntityControl>();
            _scale = new ScaleTransform();

            _myDiagramController = new DiagramController();

            //Event handler
            _myDiagramController.InputReadingComplete += new EventHandler(_myDiagramController_InputReadingComplete);
            _myDiagramController.AlgoRunComplete += new EventHandler(_myDiagramController_AlgoRunComplete);

            MainGrid.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(MainGrid_MouseLeftButtonDown);
            MainGrid.MouseMove += new System.Windows.Input.MouseEventHandler(CircleField_MouseMove);
            MainGrid.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(MainGrid_MouseLeftButtonUp);

            Render.Click += new RoutedEventHandler(Render_Click);

            // add the controls to the back          
            _colourfulFirework = new ColorfulFireworks();
            MainGrid.Children.Add(_colourfulFirework);
            _colourfulFirework.Start();

            //Populate AlgoList
            PopulateAlgoList();
        }

        private void PopulateAlgoList()
        {
            List<string> algoList = new List<string>();
            algoList.Add("Circular");
            algoList.Add("Kamada - Kawai");
            algoList.Add("Fruchterman");
            algoList.Add("Rectangle");
            algoList.Add("TreeNode");

            this.AlgoList.ItemsSource = algoList;
        }

        #region EventHandler

        void MainGrid_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!_mouseDown)
            {
                // get some initial properties
                _mouseDownPoint = e.GetPosition(this);
                _lastMouseDownPoint = e.GetPosition(this);
                _mouseDown = true;
                MainGrid.Cursor = Cursors.Hand;
                //_mouseDownY = Canvas.GetTop(MainGrid);
                //_mouseDownX
            }
        }

        void CircleField_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this._colourfulFirework.GoFire(e.GetPosition(this).X, e.GetPosition(this).Y);

            if (_mouseDown)
            {
                // update the element position
                Point point = e.GetPosition(this);
                Point diff = new Point((point.X - _mouseDownPoint.X) * 0.5, (point.Y - _mouseDownPoint.Y) * 0.5);
                Canvas.SetTop(LayoutRoot, Canvas.GetTop(LayoutRoot) + diff.Y);
                Canvas.SetLeft(LayoutRoot, Canvas.GetLeft(LayoutRoot) + diff.X);

                // update the velocity
                //_velocity += ((point.Y - _lastMouseDownPoint.Y) * SPEED_SPRINGNESS);
                _lastMouseDownPoint = e.GetPosition(this);
            }
        }

        void MainGrid_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (_mouseDown)
            {
                _mouseDown = false;
                MainGrid.Cursor = Cursors.Arrow;
            }
        }

        /// <summary>
        /// Event handler after reading input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _myDiagramController_InputReadingComplete(object sender, EventArgs e)
        {
            this.Render.IsEnabled = true;
        }

        void _myDiagramController_AlgoRunComplete(object sender, EventArgs e)
        {
            PointPositions = (Collection<Point>)sender;

            //Generate node
            for (int i = 0; i < PointPositions.Count; i++)
            {
                CreateNode(_myDiagramController.entityCollection[i], PointPositions[i].X, PointPositions[i].Y);
            }

            //Generate Connection
            for (int i = 0; i < _entityControlCollection.Count; i++)
            {
                int t = 0;
                foreach (IConnection con in _entityControlCollection[i].Entity.Connections)
                {
                    drawConnection(_entityControlCollection[i], EntityRegistry[con.Destination]);
                    t = _entityControlCollection.IndexOf(EntityRegistry[con.Destination]);
                }

                if (i == 72 || i == 73 || i == 74)
                {
                    i.ToString();
                    _entityControlCollection[i].Highlight();
                }
            }

            //ScaleTransform transform = new ScaleTransform();
            //transform.CenterX = (_myDiagramController.LowRight.X - _myDiagramController.TopLeft.X) / 2.0;
            //transform.CenterY = (_myDiagramController.LowRight.Y - _myDiagramController.TopLeft.Y) / 2.0;

            //double scaleX = (double)(panelStarfield.ActualWidth) / (double)(_myDiagramController.LowRight.X - _myDiagramController.TopLeft.X);
            //double scaleY = (double)(panelStarfield.ActualHeight) / (double)(_myDiagramController.LowRight.Y - _myDiagramController.TopLeft.Y);

            //transform.ScaleX = Math.Min(scaleX, scaleY);
            //transform.ScaleY = Math.Min(scaleX, scaleY);

            //MainGrid.RenderTransform = transform;

            Line a = new Line();
            a.X1 = _myDiagramController.TopLeft.X;
            a.Y1 = _myDiagramController.TopLeft.Y;
            a.X2 = _myDiagramController.TopLeft.X;
            a.Y2 = _myDiagramController.LowRight.Y;
            a.StrokeThickness = 5;
            a.Stroke = new SolidColorBrush(Colors.Red);
            LayoutRoot.Children.Add(a);

            Line b = new Line();
            b.X1 = _myDiagramController.TopLeft.X;
            b.Y1 = _myDiagramController.TopLeft.Y;
            b.X2 = _myDiagramController.LowRight.X;
            b.Y2 = _myDiagramController.TopLeft.Y;
            b.StrokeThickness = 5;
            b.Stroke = new SolidColorBrush(Colors.Red);
            LayoutRoot.Children.Add(b);

            Line c = new Line();
            c.X1 = _myDiagramController.TopLeft.X;
            c.Y1 = _myDiagramController.LowRight.Y;
            c.X2 = _myDiagramController.LowRight.X;
            c.Y2 = _myDiagramController.LowRight.Y;
            c.StrokeThickness = 5;
            c.Stroke = new SolidColorBrush(Colors.Red);
            LayoutRoot.Children.Add(c);

            Line d = new Line();
            d.X1 = _myDiagramController.LowRight.X;
            d.Y1 = _myDiagramController.TopLeft.Y;
            d.X2 = _myDiagramController.LowRight.X;
            d.Y2 = _myDiagramController.LowRight.Y;
            d.StrokeThickness = 5;
            d.Stroke = new SolidColorBrush(Colors.Red);
            LayoutRoot.Children.Add(d);
        }

        private void Home_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void ZoomIn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void ZoomOut_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        void Render_Click(object sender, RoutedEventArgs e)
        {
            this.LayoutRoot.Children.Clear();
            this.EntityRegistry.Clear();
            this._entityControlCollection.Clear();
            count = 0;

            if (this.AlgoList.SelectedItem != null)
            {
                IAlgorithm algo = new KKAlgorithm();

                switch (AlgoList.SelectedItem.ToString())
                {
                    case "Circular":
                        DrawCircular();
                        break;
                    case "Kamada - Kawai":
                        algo = new KKAlgorithm();
                        DrawGraphLayout(algo);
                        break;
                    case "Fruchterman":
                        algo = new FruchtermanAlgorithm();
                        DrawGraphLayout(algo);
                        break;
                    case "TreeNode":
                        algo = new TreeNodeAlgorithm();
                        DrawGraphLayout(algo);
                        break;
                    case "Rectangle":
                        algo = new RectangleAlgorithm();
                        DrawGraphLayout(algo);
                        break;
                }
            }
        }
        #endregion

        #region Render
        private void DrawCircular()
        {
            double MiddleX = panelStarfield.ActualWidth / 2.0;
            double MiddleY = panelStarfield.ActualHeight / 2.0;

            for (int i = 0; i < _myDiagramController.VertexNumber; i++)
            {
                double celcius = 360.0 / _myDiagramController.VertexNumber;

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

        private void DrawGraphLayout(IAlgorithm algo)
        {
            this._myDiagramController.RunAlgo(algo);
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
            ctrl.Title = count.ToString();
            count++;
            //ctrl.Title = "1";
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
            LayoutRoot.Children.Insert(LayoutRoot.Children.IndexOf(_entityControlCollection[0]), edge);
        }

        #endregion render

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
            //if (_mouseDown) _velocity *= MOUSE_DOWN_DECAY;
            //else _velocity *= DECAY;

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