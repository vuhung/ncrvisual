using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TestAlgo
{
    public partial class EdgeControl : UserControl
    {
        public Point StartingPoint { get; set; }
        public Point EndPoint { get; set; }

        public EdgeControl()
        {
            InitializeComponent();
            DrawPath();
        }

        private void DrawPath()
        {
            // Create a blue and a black Brush
            SolidColorBrush blueBrush = new SolidColorBrush();
            blueBrush.Color = Colors.Blue;
            SolidColorBrush blackBrush = new SolidColorBrush();
            blackBrush.Color = Colors.Black;

            // Create a Path with black brush and blue fill
            Path bluePath = new Path();
            bluePath.Stroke = blackBrush;
            bluePath.StrokeThickness = 3;
            bluePath.Fill = blueBrush;

            // Create a line geometry
            LineGeometry blackLineGeometry = new LineGeometry();
            blackLineGeometry.StartPoint = StartingPoint;
            blackLineGeometry.EndPoint = EndPoint;

            // Add all the geometries to a GeometryGroup.
            GeometryGroup blueGeometryGroup = new GeometryGroup();
            blueGeometryGroup.Children.Add(blackLineGeometry);            

            // Set Path.Data
            bluePath.Data = blueGeometryGroup;
            MainCanvas.Children.Add(bluePath);
        }
    }
}
