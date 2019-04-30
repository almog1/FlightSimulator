using FlightSimulator.Model;
using FlightSimulator.Model.EventArgs;
using FlightSimulator.Model.TCP;
using FlightSimulator.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.ViewModels
{
    class ManualJoystickViewModel : BaseNotify
    {
        //paths to send message to the simulator
        private string rudderPath = "set controls/flight/rudder";
        private string throttlePath = "set controls/engines/current-engine/throttle";
        private string AileronPath = "set controls/flight/aileron";
        private string ElevatorPath = "set controls/flight/elevator";

        public ManualJoystickViewModel()
        {
           
        }

        public double Aileron
        {
          
            set
            {
                string msg = AileronPath + " " + value + " " + "\r\n";
                CommandsChannel.Instance.SetValAndSendMes(msg);
            }

        }

        public double Elevator
        {
            set
            {
                string msg = ElevatorPath + " " + value + " " + "\r\n";
                CommandsChannel.Instance.SetValAndSendMes(msg);
            }
        }

        public double Rudder
        {
            set
            {
                string msg = rudderPath + " " + value + " " + "\r\n";
                CommandsChannel.Instance.SetValAndSendMes(msg);
            }
        }

        public double Throttle
        {
            set
            {
                string msg = throttlePath + " " + value + " " + "\r\n";
                CommandsChannel.Instance.SetValAndSendMes(msg);
            }
        }

        public void ChangeAileronAndElevator(Joystick sendler, VirtualJoystickEventArgs e)
        {
            string msg1 = AileronPath + " " + e.Aileron + " " + "\r\n";
            string msg2 = ElevatorPath + " " + e.Elevator + " " + "\r\n";
            CommandsChannel.Instance.SetValAndSendMes(msg1);
            CommandsChannel.Instance.SetValAndSendMes(msg2);
        }
    }
}
