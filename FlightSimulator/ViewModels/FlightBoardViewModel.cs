using FlightSimulator.Model;
using FlightSimulator.Model.Interface;
using FlightSimulator.Model.TCP;
using FlightSimulator.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FlightSimulator.ViewModels
{
    public class FlightBoardViewModel : BaseNotify
    {
        private FlightBoardModel model;
    
        public double Lon
        {
            get { return FlightBoardModel.Instance.Lon; }
           
        }

        public double Lat
        {
            get { return FlightBoardModel.Instance.Lat; }

        }

        //Commands for the settings and connect buttoms
        #region Commands
        #region SettingsCommand
        private ICommand _settingsCommand;
        public ICommand SettingsCommand
        {
            get
            {
                return _settingsCommand ?? (_settingsCommand = new CommandHandler(() => OnClick()));
            }
        }
        private void OnClick()
        {
            //need to open new window od the settings
            SettingPopup settingPopup = new SettingPopup();
            settingPopup.ShowDialog();
          //  MainWindow win = (MainWindow)Application.Current.MainWindow;
          //  win.Show();
            //model.SaveSettings();
        }
        #endregion

        #region Connect command
        private ICommand _connectCommand;
        public ICommand ConnectCommand
        {
            get
            {
                return _connectCommand ?? (_connectCommand = new CommandHandler(() => OnConnect()));
            }
        }
        private void OnConnect()
        {
            // create connection
           // CommandsChannel.Instance.ConnectToServer2();

            CommandsChannel.Instance.ConnectToServer();
            Console.WriteLine("CONNECTED");
        }
        #endregion
        #endregion
    }
}
