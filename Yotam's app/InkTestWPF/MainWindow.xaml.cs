using System;
using System.Collections.Generic;
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

namespace InkTestWPF
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void penButton_Click(object sender, RoutedEventArgs e)
        {
            inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
            inkCanvas.EditingModeInverted = InkCanvasEditingMode.EraseByPoint;
        }

        private void inkCanvas_Gesture(object sender, InkCanvasGestureEventArgs e)
        {
            var x = e.GetGestureRecognitionResults();
            if (x.Count > 0)
            {
                //MessageBox.Show(x[0].ApplicationGesture.ToString());
            }
            e.Handled = false;
        }

        private void eraseButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
