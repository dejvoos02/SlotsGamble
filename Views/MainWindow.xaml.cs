using SlotsGamble.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SlotsGamble.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ProgramViewModel();
        }

        private void btn_start_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btn_stop_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}