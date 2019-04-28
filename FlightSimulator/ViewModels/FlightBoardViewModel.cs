using FlightSimulator.Model;
using FlightSimulator.Model.Interface;
using FlightSimulator.Model.TCP;
using FlightSimulator.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FlightSimulator.ViewModels
{
    public class FlightBoardViewModel : BaseNotify
    {
        private FlightBoardModel model;

        public FlightBoardViewModel()
        {
            model = FlightBoardModel.Instance;
            model.updateChangedParamsEvent += NotifyChangedParams;
        }

        private void NotifyChangedParams()
        {
            NotifyPropertyChanged("Lon");
            NotifyPropertyChanged("Lat");
        }

        public double Lon
        {
            get { return model.Lon; }

        }

        public double Lat
        {
            get { return model.Lat; }
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
            InfoChannel.Instance.ClientParamsChanged += model.updateChanges;
            // create connection 
            InfoChannel.Instance.OpenServer();

            while (InfoChannel.Instance.InfoChannelConnected == false)
            {
                Thread.Sleep(1000); //wait a second untill we connected to the server
                CommandsChannel.Instance.ConnectToServer();
            }

            Console.WriteLine("CONNECTED");
        }
        #endregion
        #endregion
    }
}
