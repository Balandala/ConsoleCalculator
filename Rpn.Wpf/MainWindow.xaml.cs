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
            DrawGraph();
        }
        void cGraphic_MouseMove(object sender, MouseEventArgs e)
        {
            double scale = double.Parse(tbScale.Text);
            Point point = Mouse.GetPosition(cGraphic);
            Point math = CoordinatesConverter.ToMathCords(point, cGraphic, scale);
            lbMathCords.Content = math;
            lbUICords.Content = CoordinatesConverter.ToUiCords(math, cGraphic, scale);

        }
        void DrawGraph()
        {
            cGraphic.Children.Clear();
            Canvas canvas = cGraphic;
            double start = double.Parse(tbStart.Text);
            double end = double.Parse(tbEnd.Text);
            double step = double.Parse(tbStep.Text);
            double scale = double.Parse(tbScale.Text);
            CanvasDrawer drawer = new(canvas, start, end, step, scale);
            drawer.DrawAxis();
        }
    }
}
