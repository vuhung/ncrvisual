using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NCRVisual.RelationDiagram
{
    public partial class EntityControl : UserControl
    {
        
        Color startColor;
        Color highlightColor;
        IEntity entity;
        List<EntityControl> connectedControls = null;
        List<Line> connectionLines = null;

        /// <summary>
        /// Get the desired width of the control after render
        /// </summary>
        public double RenderedWidth 
        {
            get
            {
                return this.titleBlock.ActualWidth + (this.rectangle.Padding.Left + this.rectangle.BorderThickness.Left) * 2;
            }
        }

        /// <summary>
        /// Get the desired Height of the control after render
        /// </summary>
        public double RenderedHeight
        {
            get
            {
                return this.titleBlock.ActualHeight + (this.rectangle.Padding.Top + this.rectangle.BorderThickness.Top) * 2;
            }
        }


        public EntityControl(IEntity entity)
        {
            InitializeComponent();
            this.connectedControls = new List<EntityControl>();
            this.connectionLines = new List<Line>();
            this.entity = entity;
            this.highlightColor = Colors.Yellow;
            this.startColor = this.endStop.Color;
            this.MouseLeftButtonDown += new MouseButtonEventHandler(EntityControl_MouseLeftButtonDown);
        }


        void EntityControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.OnClicked();
        }

        public IEntity Entity
        {
            get { return this.entity; }
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
                this.startColor = this.endStop.Color;
            }
        }

        public List<Line> ConnectionLines
        {
            get { return this.connectionLines; }
            set { this.connectionLines = value; }
        }

        public List<EntityControl> ConnectedControls
        {
            get { return this.connectedControls; }
            set { this.connectedControls = value; }
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
            this.unHighlightAnimation.To = this.startColor;
            this.unHighlightStoryboard.Begin();
        }

        private void Canvas_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Highlight();
            foreach (EntityControl control in this.connectedControls)
            {
                control.HighlightSecondary();
            }
            foreach (Line line in this.connectionLines)
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
            foreach (EntityControl control in this.connectedControls)
            {
                control.UnHighlight();
            }
            foreach (Line line in this.connectionLines)
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
