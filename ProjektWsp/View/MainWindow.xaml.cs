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
using Project.ViewModel;
namespace Projekt.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new BallViewModel();
        }
        private void GenerateBalls(object sender, RoutedEventArgs e)
        {
            if (DataContext is BallViewModel viewModel)
            {
                if (int.TryParse(NumberInput.Text, out int numberOfBalls))
                {
                    viewModel.InitializeBalls(numberOfBalls);
                }
                else
                {
                    MessageBox.Show("Please enter a valid number");
                }
            }
        }
    }
}