using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Rpn.Wpf
{
    class CoordinatesConverter
    {
        Point ConvertToMathCords(Point UIcords, Canvas canvas, double _scale)
        {
            return new Point(UIcords.X + (int)(canvas.ActualWidth/2/_scale), UIcords.Y + (int)(canvas.ActualHeight/2/_scale));
        }
    }
    class CanvasDrawer
    {
        private double _scale;
        private double _step;
        private double _fEnd;
        private double _fStart;
        private Canvas _canvas;
        private Brush _brush = Brushes .Black;
        CanvasDrawer(Canvas _canvas,double _fStart, double _fEnd, double _Step, double _scale) { }
        public void DrawAxis()
        {
            Line xAxis = new Line();
            xAxis.X1 = 0;
            xAxis.X2 = _canvas.ActualWidth;
            xAxis.Y1 = xAxis.Y2 = _canvas.ActualHeight / 2;
            xAxis.Stroke = _brush;
            
            Line yAxis = new Line();
            yAxis.X1 = yAxis.X2 = _canvas.ActualWidth / 2;
            yAxis.Y1 = _canvas.ActualHeight;
            yAxis.Y2 = 0;
            yAxis.Stroke = _brush;

            _canvas.Children.Add(xAxis);
            _canvas.Children.Add(yAxis);

            DrawScale();
        }        
        private void DrawScale()
        {
            for (double i = _fStart; i < _fEnd;  i += _step) 
            {

            }
        }
    }
}
