using Industry4Control.Constants;
using Industry4Control.EventArguments;
using Industry4Control.Interfaces;
using System;
using System.Net.Sockets;
using System.Threading;

namespace Industry4Control.BusinessLogic
{
    internal class CommunicationLogic
    {
        #region Private fields

        private readonly int m_Port;
        private TcpListener m_TcpListener;
        private Thread m_ListenerThread;
        private bool m_Listening;
        private IUiElement m_Ui;

        #endregion

        #region Public fields

        public event EventHandler<DataAvailableEventArgs> DataAvailable;

        #endregion

        #region Constructors

        public CommunicationLogic(IUiElement ui, int port)
        {
            m_Port = 50005;
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
                m_TcpListener = new TcpListener(m_Port);
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
                m_ListenerThread.Join();
                m_Ui.SetStatusMessage("Stopped");
            }
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
                            DoCommand(leaderBytes[1]);
                            break;
                        case 0xB:
                            HandleRequest(leaderBytes[1]);
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

        private void DoCommand(byte command)
        {
            DataAvailable?.Invoke(this,new DataAvailableEventArgs(AvailableDataType.Command, command));
        }

        private void HandleRequest(byte request)
        {
            DataAvailable?.Invoke(this, new DataAvailableEventArgs(AvailableDataType.Request, request));
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
            client = m_TcpListener.AcceptTcpClient();
            networkStream = client.GetStream();
            networkStream.Read(dataInBytes, 0, dataInBytes.Length);

            for (int i = 0, j= dataInBytes.Length; i < dataInBytes.Length; i += 2, j+=2)
            {
                dataInShort[j / 2] = (short)(dataInBytes[i] | dataInBytes[i + 1] << 8);
            }

            DataAvailable?.Invoke(this, new DataAvailableEventArgs(AvailableDataType.Voice, dataInShort, control));
        }

        #endregion

    }
}
