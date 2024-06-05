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
            RpnCalculator calculator = new RpnCalculator();
            if (int.TryParse(tbVariable.Text, out int result))
            {
                calculator.SetVariable(result);
                lbOutput.Content = calculator.Calculate(tbExpression.Text);
            }
            else if (!tbExpression.Text.Contains("x"))
            {
                lbOutput.Content = calculator.Calculate(tbExpression.Text);
            }
            else
            {
                lbOutput.Content = "Переменная указана некорректно";
            }
        }
    }
}
