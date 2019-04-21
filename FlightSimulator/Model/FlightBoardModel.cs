using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    class FlightBoardModel
    {
        #region Singleton
        private static FlightBoardModel m_Instance = null;
        public static FlightBoardModel Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new FlightBoardModel();
                }
                return m_Instance;
            }
        }
        #endregion
        //todo -> changed by the server values
        public double Lon { get; set; }
        public double Lat { get; set; }
    }
}
