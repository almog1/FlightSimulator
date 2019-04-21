using FlightSimulator.ViewModels;
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

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for MyFlightBoard.xaml
    /// </summary>
    public partial class MyFlightBoard : UserControl
    {
        private FlightBoardViewModel viewModel;

        public MyFlightBoard()
        {
            InitializeComponent();
            viewModel = new FlightBoardViewModel();
            this.DataContext = viewModel;
        }

        private void button_click(object sendler, EventArgs e)
        {

        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
