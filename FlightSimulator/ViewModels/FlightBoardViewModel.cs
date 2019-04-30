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
    //connect between the view of the Flight board and the simulator
    public class FlightBoardViewModel : BaseNotify
    {
        private FlightBoardModel model;

        public FlightBoardViewModel()
        {
            model = FlightBoardModel.Instance;
            model.updateChangedParamsEvent += NotifyChangedParams;
        }

        //notify that the properties change - so the view know
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
            //sigh to the event that notify when the values of lot and lan changed
            InfoChannel.Instance.ClientParamsChanged += model.updateChanges;

            Task taskI = new Task(() =>
            {
                //if no connections yes
                if (CommandsChannel.Instance.IsConnect == false)
                {
                    // create connection 
                    InfoChannel.Instance.OpenServer();
                    //wait a second untill we connected to the server
                    while (InfoChannel.Instance.InfoChannelConnected == false)
                    {
                        Thread.Sleep(1000);
                        CommandsChannel.Instance.ConnectToServer();
                    }
                }
                //if already connected
                else
                {
                    CommandsChannel.Instance.Dissconnect();
                    InfoChannel.Instance.Dissconnect();

                    // create connection again
                    InfoChannel.Instance.OpenServer();
                    //wait a second untill we connected to the server
                    while (InfoChannel.Instance.InfoChannelConnected == false)
                    {
                        Thread.Sleep(1000);
                        CommandsChannel.Instance.ConnectToServer();
                    }
                }
                //Console.WriteLine("CONNECTED");
            });
            taskI.Start();
        }

        #endregion
        #endregion

        private ICommand _disConnectComand;
        public ICommand DisConnectCommand
        {
            get
            {
                return _disConnectComand ?? (_disConnectComand = new CommandHandler(() => DisConnectClick()));
            }
        }

        //closes the info and the command socket
        private void DisConnectClick()
        {
            InfoChannel.Instance.Dissconnect();
            CommandsChannel.Instance.Dissconnect();
        }

    }
}
