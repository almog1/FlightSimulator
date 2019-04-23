using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulator.Model.TCP
{
    //for sending as client to the simulator
    class CommandsChannel
    {
          //  private int portCommand;
            private IClientHandler ch;
        private TcpListener tcpListen;
            private bool _isConnect;
            private TcpClient _client;

        #region Singleton
        private static CommandsChannel m_Instance = null;
        public static CommandsChannel Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new CommandsChannel();
                }
                return m_Instance;
            }
        }

        public bool IsConnect
        {
            get {
                return _isConnect;
            }
            set
            {
                _isConnect = value;
            }
        }
        #endregion
        private CommandsChannel()
        {
            IsConnect = false; //not connected yet
            //flight command port
        }

         public void ConnectToServer()
         {
            //take it from the settings
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ApplicationSettingsModel.Instance.FlightServerIP),
                ApplicationSettingsModel.Instance.FlightCommandPort);

           // Thread thread = new Thread(() =>
            //{
              //  while (true)
               // {
                    try
                    {
                        Client = new TcpClient();
                        Client.Connect(ep); //connecting as client to the server
                                             //change to connected
                        IsConnect = true;

                        // TcpClient client = listener.AcceptTcpClient();
                    }
                    catch (SocketException)
                    {
                        //break;
                    }
                //}
                Console.WriteLine("connect in command channel");
           // });

           // thread.Start(); // here the real connection
            }

        public void Dissconnect()
            {
            //if connected
            if (IsConnect == true)
            {
                Client.Close();
                IsConnect = false;
            }
                //listener.Stop();
            }

        public TcpClient Client
        {
            get { return _client; }
            set
            {
                _client = value;
            }
        }

        public void SendMessage(string text)
        {
            //check the c
            if (IsConnect == true)
            {
                new Thread(() =>
                {
                    byte[] sendBuffer = new byte[1024];

                // int bytesToRead = 0, nextReadCount, rc;
                NetworkStream stream = Client.GetStream();
                //  StreamWriter writer = new StreamWriter(stream); //open for writing
                string[] lines = text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                    string line, newLine = " \r\n";

                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (lines[i] != "")
                        {
                            line = lines[i] + newLine;
                            sendBuffer = (Encoding.ASCII).GetBytes(line);
                        //  byteCount = BitConverter.GetBytes(sendBuffer.Length);
                        stream.Write(sendBuffer, 0, sendBuffer.Length);

                        //                        writer.Write(line);
                        // Send the actual data
                        Console.WriteLine("TCP client: Sending the actual data...");
                        Task.Delay(2000);
                        }
                    }
                }).Start();
            }
        }

        //get line , add "\r\n" and send it to client
        public void send(string line)
        {

        }
    }
}
        
