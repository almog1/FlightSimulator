using FlightSimulator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulator.Model.TCP
{
    // Infochannel for listening to the server and getting the lan and lot positions 
    class InfoChannel
    {
        private static InfoChannel m_Instance = null;
        TcpClient client;
        TcpListener listener;
        private bool isConnect;

        public bool IsConnect
        {
            get
            {
                return isConnect;
            }

            set
            {
                isConnect = value;
            }
        }

        private InfoChannel()
        {
            IsConnect = false;
        }

        public void DisConnect()
        {
            if(isConnect == true){
                listener.Stop();
            }
            IsConnect = true;
            
        }


        public static InfoChannel Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new InfoChannel();
                }
                return m_Instance;
            }
        }

        //connect to server in order to get the new values that changed
        public void OpenServer()
        {
            //getting the info about the server and the ip
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ApplicationSettingsModel.Instance.FlightServerIP),
                ApplicationSettingsModel.Instance.FlightInfoPort);
            listener = new TcpListener(ep);
            listener.Start();
            IsConnect = true;
            ClientHandler handler = new ClientHandler();

            Thread threadI = new Thread(() =>
            {
                while (IsConnect)
                {
                    try
                    {
                        client = listener.AcceptTcpClient();
                        Console.WriteLine("Got new connection");
                        handler.HandleClient(client);
                    }
                    catch (SocketException)
                    {
                        Console.WriteLine("Error!!!");
                    }
                }
                Console.WriteLine("Server stopped");
            });
        
            threadI.Start();
        }

        //get the messages from the plane about lan and lot
        public void GetMesFromPlane()
        {
            Byte[] bytes;
            String data = null; //the mess will be here
            NetworkStream stream = client.GetStream(); // Get a stream object for reading and writing

            while (true)
            {
                //we got an input from the user
                if (client.ReceiveBufferSize > 0)
                {
                    bytes = new byte[client.ReceiveBufferSize];
                    stream.Read(bytes, 0, client.ReceiveBufferSize);
                    data = Encoding.ASCII.GetString(bytes); //the message incoming
                    string[] splitMs = data.Split(','); //split the mess drom the server

                    FlightBoardModel.Instance.Lon = double.Parse(splitMs[0]);
                    FlightBoardModel.Instance.Lat = double.Parse(splitMs[1]);
                }
                data = null;
            }
            stream.Close();
            client.Close();
            listener.Stop();
        }
    }


}
