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

        private string _aileron;
        private string _elevator;
        private string _rudder;
        private string _throttle;

        public string Aileron
        {
            get { return _aileron; }
            set
            {
                _aileron = value;
            }
        }

        public string Elevator
        {
            get { return _elevator; }
            set
            {
                _elevator = value;
            }
        }

        public string Rudder
        {
            get { return _rudder; }
            set
            {
                _rudder = value;
            }
        }

        public string Throttle
        {
            get { return _throttle; }
            set
            {
                _throttle = value;
            }
        }
    }
}
