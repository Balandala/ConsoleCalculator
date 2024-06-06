using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Rpn.Wpf
{
    public class CoordinatesConverter
    {
        public static Point ToMathCords(Point UIcords, Canvas canvas, double scale)
        {
            return new Point((UIcords.X - canvas.ActualWidth / 2) / scale, (canvas.ActualHeight / 2 - UIcords.Y) / scale);
        }
        public static Point ToUiCords(Point Mathcords, Canvas canvas, double scale)
        {
            return new Point((canvas.ActualWidth / 2 + Mathcords.X * scale), (-Mathcords.Y * scale + canvas.ActualHeight / 2));
        }
    }
    public class CanvasDrawer
    {
        private double _scale;
        private double _step;
        private double _fEnd;
        private double _fStart;

        private Canvas _canvas;
        private Brush _brush = Brushes.Black;
        public CanvasDrawer(Canvas canvas, double fStart, double fEnd, double step, double scale)
        {
            _canvas = canvas;
            _fStart = fStart;
            _fEnd = fEnd;
            _step = step;
            _scale = scale;
        }
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
        void DrawScale()
        {
            for (double i = _fStart; i < _fEnd; i += _step)
            {
                double scaleHeight = 0.5;
                Point top = new Point(i, scaleHeight);
                Point bottom = new Point(i, -scaleHeight);
                top = CoordinatesConverter.ToUiCords(top, _canvas, _scale);
                bottom = CoordinatesConverter.ToUiCords(bottom, _canvas, _scale);
                Line line = new Line
                {
                    X1 = bottom.X,
                    Y1 = bottom.Y,
                    X2 = top.X,
                    Y2 = top.Y,
                };
                line.Stroke = _brush;
                _canvas.Children.Add(line);
            }
        }
        public void DrawGraph(List<Point> points)
        {
            for (int i = 0; i < points.Count - 1; i++)
            {
                Point point1 = CoordinatesConverter.ToUiCords(points[i], _canvas, _scale);
                Point point2 = CoordinatesConverter.ToUiCords(points[i + 1], _canvas, _scale);
                Line graphPieace = new Line
                {
                    X1 = point1.X,
                    Y1 = point1.Y,
                    X2 = point2.X,
                    Y2 = point2.Y,
                };
                if (point2.X < _canvas.ActualWidth && point2.Y < _canvas.ActualHeight)
                {
                    graphPieace.Stroke = Brushes.Red;
                    _canvas.Children.Add(graphPieace);
                }
            }
        }
    }
}
