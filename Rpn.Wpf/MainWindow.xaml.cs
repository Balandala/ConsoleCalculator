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
            
        }
        void cGraphic_MouseMove(object sender, MouseEventArgs e)
        {
            Point point = Mouse.GetPosition(cGraphic);
            lbUICords.Content = point;

        }
        void DrawGraph()
        {
            CanvasDrawer = new CanvasDrawer(cGraphic,
                _fStart = double.,
                tbEnd,
                tb
                )
        }
    }
}
