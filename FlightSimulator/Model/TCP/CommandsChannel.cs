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
            get
            {
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
        }

        public void ConnectToServer()
        {
            //take it from the settings
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ApplicationSettingsModel.Instance.FlightServerIP),
                ApplicationSettingsModel.Instance.FlightCommandPort);
            try
            {
                Client = new TcpClient();
                Client.Connect(ep); //connecting as client to the server
                                    //change to connected
                IsConnect = true;
            }
            catch (SocketException)
            {
                Console.WriteLine("FAILED CONNECT COMMAND");
                //break;
            }
            
          //  Console.WriteLine("connect in command channel");
          
        }
        public void Dissconnect()
        {
            if (IsConnect == true)
            {
                Client.Close();
            }
            IsConnect = false;
        }

        public TcpClient Client
        {
            get { return _client; }
            set
            {
                _client = value;
            }
        }

        //send the aactual message to the simulator
        public void SendMessage(string text)
        {
            //check the c
            if (IsConnect == true)
            {
                new Thread(() =>
                {
                    byte[] sendBuffer = new byte[1024];

                    NetworkStream stream = Client.GetStream();
                    string[] lines = text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                    string line, newLine = " \r\n";

                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (lines[i] != "")
                        {
                            line = lines[i] + newLine;
                            sendBuffer = (Encoding.ASCII).GetBytes(line);
                            stream.Write(sendBuffer, 0, sendBuffer.Length);

                            Console.WriteLine("TCP client: Sending the actual data...");
                            Thread.Sleep(2000);
                        }
                    }
                }).Start();
            }
        }

        public void SetValAndSendMes(string text)
        {
            //check the connection
            if (IsConnect == true)
            {
                new Thread(() =>
                {
                    byte[] sendBuffer = new byte[1024];

                    NetworkStream stream = Client.GetStream();

                    if (text != "")
                    {
                        sendBuffer = (Encoding.ASCII).GetBytes(text);
                       // Console.WriteLine("the mess from the menual:" + sendBuffer);
                        stream.Write(sendBuffer, 0, sendBuffer.Length);

                        //Console.WriteLine("TCP client: Sending the actual data...");
                        Thread.Sleep(2000);
                    }

                }).Start();
            }
        }
    }
}

