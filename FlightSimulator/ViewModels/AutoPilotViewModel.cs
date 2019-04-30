using FlightSimulator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace FlightSimulator.ViewModels
{
    //connect between the auto commands from the pilots to the server
    class AutoPilotViewModel : BaseNotify
    {
        private AutoPilotModel autoPilotModel;
        string text;
        private bool ischange;
 
        public bool IsChanged
        {
            get { return ischange; }
            set
            {
                ischange = value;
                //for the background color change
                NotifyPropertyChanged("IsChanged");
            }
        }

        public AutoPilotViewModel()
        {
            this.autoPilotModel = AutoPilotModel.Instance;
            IsChanged = false;
            text = "";
        }

        public string TextAutoPilot
        {
            get
            {
                return text;
            }
            set
            {
                //if nothing - isChange
                text = value;
                if(text == "")
                {
                    IsChanged = false;
                }
                else
                {
                    IsChanged = true;
                }
                NotifyPropertyChanged("TextAutoPilot");

            }
        }
        
        //Commands for the settings and connect buttoms
        #region Commands
        #region OKCommand
        private ICommand _okCommand;
        public ICommand OkCommand
        {
            get
            {
                return _okCommand ?? (_okCommand = new CommandHandler(() => OKClick()));
            }
        }

        private void OKClick()
        {
           // Console.WriteLine(TextAutoPilot);       
            //send the text - after split
            autoPilotModel.SetValues(TextAutoPilot);
            IsChanged = false;
        }
        #endregion

        #region Clear command
        private ICommand _clearCommand;
        public ICommand ClearCommand
        {
            get
            {
                return _clearCommand ?? (_clearCommand = new CommandHandler(() => OnClear()));
            }
        }
        private void OnClear()
        {
            //clear the screen
            TextAutoPilot = "";
            IsChanged = false; //need to change it
        }
        #endregion
        #endregion
        
    }
}
