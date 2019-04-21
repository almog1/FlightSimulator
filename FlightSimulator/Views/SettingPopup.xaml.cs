using FlightSimulator.Model;
using FlightSimulator.ViewModels.Windows;
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
    /// Interaction logic for SettingPopup.xaml
    /// </summary>
    public partial class SettingPopup : Window
    {
        private SettingsWindowViewModel setVm;

        public SettingPopup()
        {
            InitializeComponent();
            setVm = new SettingsWindowViewModel(ApplicationSettingsModel.Instance);
            this.DataContext = setVm;
        }
    }
}
