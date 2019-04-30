using FlightSimulator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    //infoChannel
    //this class for the left side - change lon and lat according to what gets from the server (that values we change)
    class FlightBoardModel
    {
        public delegate void UpdateChangedParams();
        public event UpdateChangedParams updateChangedParamsEvent;
        private static FlightBoardModel m_Instance = null;
        double lon, lat;

        //todo -> changed by the server values
        public double Lon {
            get
            {
                return lon;
            }
            set
            {
                lon = value;
               
            }
        }
        public double Lat {
            get
            {
                return lat;
            }
            set
            {
                lat = value;
            }
        }

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

        //update the changes in the lot and lan values
        public void updateChanges(double lon, double lat)
        {
            this.Lat = lat;
            this.Lon = lon;
            updateChangedParamsEvent?.Invoke();
        }
    }
}
