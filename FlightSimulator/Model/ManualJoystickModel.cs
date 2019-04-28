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

        

        
    }
}
