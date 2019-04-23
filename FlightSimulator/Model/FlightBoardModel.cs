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
    class FlightBoardModel : BaseNotify
    {
        private static FlightBoardModel m_Instance = null;
        TcpClient client;
        TcpListener listener;
        double lon, lat;
        bool shouldStop = false;

        //todo -> changed by the server values
        public double Lon {
            get
            {
                return lon;
            }
            set
            {
                lon = value;
                NotifyPropertyChanged("Lon");
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
                NotifyPropertyChanged("Lat");
            }
        }

        public bool ShouldStop
        {
            get
            {
                return shouldStop;
            }

            set
            {
                shouldStop = value;
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



        public void getMesFromPlane(TcpClient client, TcpListener listener)
        {
            //getting the info about the server and the ip
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ApplicationSettingsModel.Instance.FlightServerIP),
                ApplicationSettingsModel.Instance.FlightInfoPort);
            listener = new TcpListener(ep);
            listener.Start();
            Console.WriteLine("Waiting for client connections...");
            client = listener.AcceptTcpClient();
            Console.WriteLine("Info channel: Client connected");
            Thread threadI = new Thread(() =>
            {
                Byte[] bytes;
                String data = null; //the mess will be here
                NetworkStream stream = client.GetStream(); // Get a stream object for reading and writing

                while (!shouldStop)
                {
                    if (client.ReceiveBufferSize > 0)
                    {
                        bytes = new byte[client.ReceiveBufferSize];
                        stream.Read(bytes, 0, client.ReceiveBufferSize);
                        data = Encoding.ASCII.GetString(bytes); //the message incoming
                        string[] splitMs = data.Split(',');
                        if (data.Contains(","))
                        {
                            Lon = double.Parse(splitMs[0]);
                            Lat = double.Parse(splitMs[1]);
                        }
                    }
                    data = null;
                }
                stream.Close();
                client.Close();
                listener.Stop();
            });
            threadI.Start();
            
        }
    }
}
