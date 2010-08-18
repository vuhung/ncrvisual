using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;

namespace NCRVisual.RelationDiagram
{
    public partial class EntityControl : UserControl
    {
        #region private
        private Color _startColor;
        private Color _highlightColor;
        private IEntity _entity;
        private List<EntityControl> _connectedControls = null;
        private List<Line> _connectionLines = null;
        private Point _position;
        #endregion


        /// <summary>
        /// Get the desired width of the control after render
        /// </summary>
        public double DesiredWidth
        {
            get
            {
                return this.titleBlock.ActualWidth + (this.rectangle.Padding.Left + this.rectangle.BorderThickness.Left) * 2 + 1;
            }
        }

        /// <summary>
        /// Get the desired Height of the control after render
        /// </summary>
        public double DesiredHeight
        {
            get
            {
                return this.titleBlock.ActualHeight + (this.rectangle.Padding.Top + this.rectangle.BorderThickness.Top) * 2 + 1;
            }
        }

        public EntityControl(IEntity entity)
        {
            InitializeComponent();
            this._connectedControls = new List<EntityControl>();
            this._connectionLines = new List<Line>();
            this._entity = entity;
            this._highlightColor = Colors.Yellow;
            this._startColor = this.endStop.Color;
            this.MouseLeftButtonDown += new MouseButtonEventHandler(EntityControl_MouseLeftButtonDown);
        }


        void EntityControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.OnClicked();
        }

        public IEntity Entity
        {
            get { return this._entity; }
            set { this._entity = value; }
        }

        public string Title
        {
            get { return this.titleBlock.Text; }
            set { this.titleBlock.Text = value; }
        }

        public double RectangleOpacity
        {
            get { return this.rectangle.Opacity; }
            set { this.rectangle.Opacity = value; }
        }

        public Color Color
        {
            get { return this.endStop.Color; }
            set
            {
                this.endStop.Color = value;
                this._startColor = this.endStop.Color;
            }
        }

        public List<Line> ConnectionLines
        {
            get { return this._connectionLines; }
            set { this._connectionLines = value; }
        }

        public List<EntityControl> ConnectedControls
        {
            get { return this._connectedControls; }
            set { this._connectedControls = value; }
        }

        public Point Position
        {
            get { return this._position; }
            set { this._position = value; }
        }

        public void Highlight()
        {
            this.highlightStoryboard.Begin();
        }

        void HighlightSecondary()
        {
            this.highlightSecondaryStoryboard.Begin();
        }

        public void UnHighlight()
        {
            this.unHighlightAnimation.To = this._startColor;
            this.unHighlightStoryboard.Begin();
        }

        private void Canvas_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Highlight();
            foreach (EntityControl control in this._connectedControls)
            {
                control.HighlightSecondary();
            }
            foreach (Line line in this._connectionLines)
            {
                line.Opacity = 1;
                line.StrokeThickness = 10;
                SolidColorBrush brush = line.Stroke as SolidColorBrush;
                brush.Color = Colors.Red;
            }
        }

        private void Canvas_MouseLeave(object sender, MouseEventArgs e)
        {
            this.UnHighlight();
            foreach (EntityControl control in this._connectedControls)
            {
                control.UnHighlight();
            }
            foreach (Line line in this._connectionLines)
            {
                line.Opacity = .3;
                line.StrokeThickness = 1;
                SolidColorBrush brush = line.Stroke as SolidColorBrush;
                brush.Color = Colors.Black;
            }
        }

        public event EventHandler<EntityControlClickedEventArgs> Clicked;

        protected void OnClicked()
        {
            if (this.Clicked != null)
            {
                EntityControlClickedEventArgs args = new EntityControlClickedEventArgs(this.Entity);
                this.Clicked(this, args);
            }
        }
    }
}
