using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    class ManualJoystickModel
    {
        #region Singleton
        private static ManualJoystickModel m_Instance = null;
        public static ManualJoystickModel Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new ManualJoystickModel();
                }
                return m_Instance;
            }
        }
        #endregion

        private double _aileron;
        private double _elevator;
        private double _rudder;
        private double _throttle;

        public double Aileron
        {
            get { return _aileron; }
            set
            {
                _aileron = value;
            }
        }

        public double Elevator
        {
            get { return _elevator; }
            set
            {
                _elevator = value;
            }
        }

        public double Rudder
        {
            get { return _rudder; }
            set
            {
                _rudder = value;
            }
        }

        public double Throttle
        {
            get { return _throttle; }
            set
            {
                _throttle = value;
            }
        }
    }
}
