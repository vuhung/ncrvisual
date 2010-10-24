using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using NCRVisual.RelationDiagram.Algo;
using RelationDiagram;
using PersonalInfoAndStatistics;

namespace NCRVisual.RelationDiagram
{
    public partial class RelationDiagramControl : UserControl
    {

        #region private
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

        //Private variable for handling transformation of layoutroot
        private Transform FirstTransform;
        private double firstX;
        private double firstY;

        private Point _mouseDownPoint = new Point();
        private Point _lastMouseDownPoint = new Point();

        #endregion

        #region Properties
        /// <summary>
        /// Dictionary for maping entity and entityControl
        /// </summary>
        public Dictionary<IEntity, EntityControl> EntityRegistry { get; set; }

        /// <summary>
        /// Collection of pointpositions
        /// </summary>
        public Collection<Point> PointPositions { get; set; }

        #endregion

        #region Constructor

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

            //Populate personalStatistic Control
            PopulatePersonalStatistics();
        }        
        #endregion

        private void PopulatePersonalStatistics()
        {
            PersonalStatisticsControl psc = new PersonalStatisticsControl();
            this.DetailBorder.Child = psc;
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

        private void Centerize()
        {
            double middleX = (_myDiagramController.LowRight.X - _myDiagramController.TopLeft.X) / 2.0;
            double middleY = (_myDiagramController.LowRight.Y - _myDiagramController.TopLeft.Y) / 2.0;

            double diffX = middleX - (App.Current.Host.Content.ActualWidth) / 2.0;
            double diffY = middleX - (App.Current.Host.Content.ActualHeight) / 2.0;

            Canvas.SetTop(LayoutRoot, -diffY);
            Canvas.SetLeft(LayoutRoot, -diffX);

            double scaleX = (double)(panelStarfield.ActualWidth) / (double)(_myDiagramController.LowRight.X - _myDiagramController.TopLeft.X);
            double scaleY = (double)(panelStarfield.ActualHeight) / (double)(_myDiagramController.LowRight.Y - _myDiagramController.TopLeft.Y);
            ScaleTransform scale = new ScaleTransform();
            scale.CenterX = middleX;
            scale.CenterY = middleY;
            scale.ScaleX = Math.Min(scaleX, scaleY) - 0.1;
            scale.ScaleY = scale.ScaleX;

            this.LayoutRoot.RenderTransform = scale;
        }

        private void Home_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.LayoutRoot.RenderTransform = FirstTransform;
            Canvas.SetLeft(LayoutRoot, firstX);
            Canvas.SetTop(LayoutRoot, firstY);
        }

        private void ZoomIn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ScaleTransform scale = (ScaleTransform)this.LayoutRoot.RenderTransform;
            scale.ScaleX += 0.1;
            scale.ScaleY += 0.1;
        }

        private void ZoomOut_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ScaleTransform scale = (ScaleTransform)this.LayoutRoot.RenderTransform;
            scale.ScaleX -= 0.1;
            scale.ScaleY -= 0.1;
        }

        void Render_Click(object sender, RoutedEventArgs e)
        {
            this.LayoutRoot.Children.Clear();
            this.EntityRegistry.Clear();
            this._entityControlCollection.Clear();
            count = 0;
            this.LayoutRoot.RenderTransform = new MatrixTransform();
            Canvas.SetLeft(LayoutRoot, 0);
            Canvas.SetTop(LayoutRoot, 0);

            if (this.AlgoList.SelectedItem != null)
            {
                Render.IsEnabled = false;

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

        void bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            PointPositions = (Collection<Point>) e.Result;

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
            }

            Centerize();

            //Deep copy for save the first transform of the diagram
            FirstTransform = new ScaleTransform
            {
                CenterX = double.Parse(((ScaleTransform)LayoutRoot.RenderTransform).CenterX.ToString()),
                CenterY = double.Parse(((ScaleTransform)LayoutRoot.RenderTransform).CenterY.ToString()),
                ScaleX = double.Parse(((ScaleTransform)LayoutRoot.RenderTransform).ScaleX.ToString()),
                ScaleY = double.Parse(((ScaleTransform)LayoutRoot.RenderTransform).ScaleY.ToString()),
            };

            firstX = Canvas.GetLeft(LayoutRoot);
            firstY = Canvas.GetTop(LayoutRoot);

            Render.IsEnabled = true;
        }

        void bg_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = this._myDiagramController.RunAlgo((IAlgorithm) e.Argument);           
        }

        #endregion

        #region Render

        private void DrawCircular()
        {
            double MiddleX = panelStarfield.ActualWidth / 2.0;
            double MiddleY = panelStarfield.ActualHeight / 2.0;

            for (int i = 0; i < _myDiagramController.VertexNumber; i++)
            {
                double celcius = 360.0 / (_myDiagramController.VertexNumber +1);

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

            LayoutRoot.RenderTransform = new ScaleTransform();

            FirstTransform = LayoutRoot.RenderTransform;
            firstX = Canvas.GetLeft(LayoutRoot);
            firstY = Canvas.GetTop(LayoutRoot);

            Render.IsEnabled = true;
        }

        private void DrawGraphLayout(IAlgorithm algo)
        {
            BackgroundWorker bg = new BackgroundWorker();
            bg.WorkerReportsProgress = true;
            bg.WorkerSupportsCancellation = true;
            bg.DoWork += new DoWorkEventHandler(bg_DoWork);
            bg.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bg_RunWorkerCompleted);
            bg.RunWorkerAsync(algo);
        }

        /// <summary>
        /// Create a node of the graph ( entity Control )
        /// </summary>
        /// <param name="Display">The title of the node</param>
        /// <param name="left">The distance between the top-left of the node to the left of the browser</param>
        /// <param name="top">The distance between the top-left of the node to the top of the browser</param>
        public EntityControl CreateNode(IEntity entity, double left, double top)
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

            return ctrl;
        }

        /// <summary>
        /// Draw connection between 2 entity control
        /// </summary>
        /// <param name="startingPoint">Source entity</param>
        /// <param name="endPoint">Final entity</param>
        public void drawConnection(EntityControl startingPoint, EntityControl endPoint)
        {
            Random r = new Random();

            EdgeControl edge = new EdgeControl(startingPoint, endPoint, Color.FromArgb(255, (byte)r.Next(0, 255), (byte)r.Next(0, 255), (byte)r.Next(0, 255)));
            startingPoint.ConnectedEdges.Add(edge);
            endPoint.ConnectedEdges.Add(edge);
            edge.drawEdge();
            LayoutRoot.Children.Insert(LayoutRoot.Children.IndexOf(_entityControlCollection[0]), edge);
        }

        #endregion render
    }
}
