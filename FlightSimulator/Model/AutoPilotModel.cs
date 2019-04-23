using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    class AutoPilotModel
    {
     //   public bool isChanged{get;set;}

        #region Singleton
        private static AutoPilotModel m_Instance = null;
        public static AutoPilotModel Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new AutoPilotModel();
                }
                return m_Instance;
            }
        }
        #endregion

        //method to send to the simulator the commands
        public void SetValues()
        {
            //send to server with /r/n
            //each line in 2 seconds
        }
    }
}
