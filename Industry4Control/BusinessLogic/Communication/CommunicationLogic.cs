using Industry4Control.Constants;
using Industry4Control.EventArguments;
using Industry4Control.Interfaces;
using System;
using System.Net.Sockets;
using System.Threading;

namespace Industry4Control.BusinessLogic.Communication
{
    internal class CommunicationLogic
    {
        #region Private fields

        private readonly int m_ControlPort;
        private readonly int m_PlcPort;
        private TcpListener m_TcpListener;
        private Thread m_ListenerThread;
        private bool m_Listening;
        private IUiElement m_Ui;

        #endregion

        #region Public fields

        public event EventHandler<DataAvailableEventArgs> DataAvailable;

        #endregion

        #region Constructors

        public CommunicationLogic(IUiElement ui, int controlPort, int plcPort)
        {
            m_ControlPort = controlPort;
            m_PlcPort = plcPort;
            m_Listening = false;
            m_Ui = ui;
        }

        #endregion

        #region Public methods

        public void StartListening()
        {
            if (!m_Listening)
            {
#pragma warning disable CS0618 // Type or member is obsolete
                m_TcpListener = new TcpListener(m_ControlPort);
#pragma warning restore CS0618 // Type or member is obsolete
                m_ListenerThread = new Thread(ExecuteInBackground);
                m_Listening = true;
                m_TcpListener.Start();
                m_ListenerThread.Start();

                m_Ui.SetStatusMessage("Listening");
            }
        }

        public void StopListening()
        {
            if (m_Listening)
            {
                m_Listening = false;
                m_TcpListener.Stop();
                m_TcpListener = null;
                m_ListenerThread.Join();
                m_ListenerThread = null;
                m_Ui.SetStatusMessage("Stopped");
            }
        }

        public void Send(MessageBase message, TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            
            switch (message.MessageType)
            {
                case MessageType.OverallStatus:
                    OverallStatusMessage overallStatus = message as OverallStatusMessage;
                    if(overallStatus != null)
                    {
                        byte[] bytes = new byte[2];
                        bytes[0] = 0xD;
                        bytes[1] |= (byte)((overallStatus.Function3 ? 1 : 0) << 2);
                        bytes[1] |= (byte)((overallStatus.Function2 ? 1 : 0) << 1);
                        bytes[1] |= (byte)((overallStatus.Function1 ? 1 : 0 ));
  
                        stream.Write(bytes, 0, 2);
                    }
                    break;
                case MessageType.ProcessStatus:
                    ProcessStatusMessage processStatus = message as ProcessStatusMessage;
                    if(processStatus != null)
                    {
                        byte[] bytes = new byte[2];
                        bytes[0] = 0xE;
                        bytes[1] = (byte)((processStatus.ProcessType == ProcessType.Save ? 1 : 0 << 1) 
                            | (processStatus.Status ? 1:0));
                        
                        stream.Write(bytes, 0, 2);
                    }
                    break;
            }
        }

        public void SendToPLC(string ipAddress, PLCControlMessage message)
        {
            TcpClient client = new TcpClient(ipAddress, m_PlcPort);
            NetworkStream stream = client.GetStream();
            byte[] bytes = new byte[2];
            bytes[0] = (byte)message.Function;
            bytes[1] = message.Value ? (byte)1 : (byte)0;

            stream.Write(bytes, 0, 2);
        }

        #endregion

        #region Private methods

        private void ExecuteInBackground()
        {
            try
            {
                while (m_Listening)
                {
                    TcpClient client = m_TcpListener.AcceptTcpClient();
                    NetworkStream networkStream = client.GetStream();
                    byte[] leaderBytes = new byte[2];
                    networkStream.Read(leaderBytes, 0, leaderBytes.Length);

                    switch (leaderBytes[0])
                    {
                        case 0xA:
                            DoCommand(leaderBytes[1], client);  
                            break;
                        case 0xB:
                            HandleRequest(leaderBytes[1], client);
                            break;
                        case 0xC:
                            ReceiveData(leaderBytes[1]);
                            break;
                    }
                }
            }catch(SocketException se)
            {
                // Check whether the listening is canceled or there is another exception
                if(se.ErrorCode != 10004)
                {
                    throw se;
                }
            }
        }

        private void DoCommand(byte command, TcpClient client)
        {
            DataAvailable?.Invoke(this,new DataAvailableEventArgs(AvailableDataType.Command, command, client));
        }

        private void HandleRequest(byte request, TcpClient client)
        {
            DataAvailable?.Invoke(this, new DataAvailableEventArgs(AvailableDataType.Request, request, client));
        }

        private void ReceiveData(byte control)
        {
            // receive the first part of the data (~10kb)
            TcpClient client = m_TcpListener.AcceptTcpClient();
            client.ReceiveBufferSize = 22050;
            NetworkStream networkStream = client.GetStream();
            byte[] dataInBytes = new byte[22050];
            networkStream.Read(dataInBytes, 0, dataInBytes.Length);

            short[] dataInShort = new short[22050];
            for(int i = 0; i < dataInBytes.Length; i += 2)
            {
                dataInShort[i / 2] = (short)(dataInBytes[i] | dataInBytes[i + 1] << 8);
            }

            // receive the second part of the data (~10kb)
            dataInBytes = new byte[22050]; // to be clear the array
            client = m_TcpListener.AcceptTcpClient();
            client.ReceiveBufferSize = 22050;
            networkStream = client.GetStream();
            networkStream.Read(dataInBytes, 0, dataInBytes.Length);

            for (int i = 0, j= dataInBytes.Length; i < dataInBytes.Length; i += 2, j += 2)
            {
                dataInShort[j / 2] = (short)(dataInBytes[i] | dataInBytes[i + 1] << 8);
            }
            
            DataAvailable?.Invoke(this, new DataAvailableEventArgs(AvailableDataType.Voice, control, client, dataInShort));
        }

        #endregion

    }
}
