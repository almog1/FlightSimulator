using FlightSimulator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.ViewModels
{
    class ManualJoystickViewModel : BaseNotify
    {
        private ManualJoystickModel manualModel;

        public ManualJoystickViewModel()
        {
            manualModel = ManualJoystickModel.Instance;
        }

        public double Aileron
        {
            get { return manualModel.Aileron; }
            set
            {
                manualModel.Aileron = value;
                NotifyPropertyChanged("Aileron");
            }
        }

        public double Elevator
        {
            get { return manualModel.Elevator; }
            set
            {
                manualModel.Elevator = value;
                NotifyPropertyChanged("Elevator");
            }
        }

        public double Rudder
        {
            get { return manualModel.Rudder; }
            set
            {
                manualModel.Rudder = value;
                NotifyPropertyChanged("Rudder");
            }
        }

        public double Throttle
        {
            get { return manualModel.Throttle; }
            set
            {
                manualModel.Throttle = value;
                NotifyPropertyChanged("Throttle");
            }
        }

    }
}
