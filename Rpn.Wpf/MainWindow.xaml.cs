using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Rpn.Logic;
using Rpn.Wpf;

namespace Rpn.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        void btnCalcualate_Click(object sender, RoutedEventArgs e)
        {
            RefreshGraph();
        }
        void cGraphic_MouseMove(object sender, MouseEventArgs e)
        {
            double scale;
            if (double.TryParse(tbScale.Text, out double result))
                scale = result;
            else
                scale = 1.0;
            Point point = Mouse.GetPosition(cGraphic);
            point = CoordinatesConverter.ToMathCords(point, cGraphic, scale);
            Point mathCords = new (Math.Round(point.X, 2),Math.Round(point.Y, 2));
            lbMathCords.Content = mathCords;

        }
        void cWhenLoaded(object sender, RoutedEventArgs e)
        {
            RefreshGraph();
        }
        void RefreshGraph()
        {
            cGraphic.Children.Clear();
            double start = double.Parse(tbStart.Text);
            double end = double.Parse(tbEnd.Text);
            double step = double.Parse(tbStep.Text);
            double scale = double.Parse(tbScale.Text);
            CanvasDrawer drawer = new(cGraphic, start, end, step, scale);
            drawer.DrawAxis();

            List<Point> points = new List<Point>();
            RpnCalculator calculator = new RpnCalculator(tbExpression.Text);
            for (double x = start; x < end; x += step)
            {
               double y = calculator.Calculate(x);
                if (!(double.IsInfinity(y) || double.IsNaN(y)))
                {
                    Point point = new Point(x, y);
                    Point pointInUi = CoordinatesConverter.ToUiCords(point, cGraphic, scale);
                    if (CoordinatesConverter.IsUiPointInsideCanvas(pointInUi, cGraphic))
                        points.Add(point);
                }
            }
            drawer.DrawGraph(points);
        }
    }
}
