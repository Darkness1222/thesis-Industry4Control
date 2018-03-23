﻿using Industry4Control.Constants;
using Industry4Control.EventArguments;
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

        #endregion

        #region Public fields

        public event EventHandler<DataAvailableEventArgs> DataAvailable;

        #endregion

        #region Constructors

        public CommunicationLogic(int port)
        {
            m_Port = 50005;
#pragma warning disable CS0618 // Type or member is obsolete
            m_TcpListener = new TcpListener(m_Port);
#pragma warning restore CS0618 // Type or member is obsolete
            m_ListenerThread = new Thread(ExecuteInBackground);
            m_Listening = false;
        }

        #endregion

        #region Public methods

        public void StartListening()
        {
            if (!m_Listening)
            {
                m_Listening = true;
                m_TcpListener.Start();
                m_ListenerThread.Start();
            }
        }

        public void StopListening()
        {
            if (m_Listening)
            {
                m_Listening = false;
                m_ListenerThread.Join();
                m_TcpListener.Stop();
            }
        }

        #endregion

        #region Private methods

        private void ExecuteInBackground()
        {
            while (true)
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
                        ReceiveData();
                        break;
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

        private void ReceiveData()
        {
            TcpClient client = m_TcpListener.AcceptTcpClient();
            NetworkStream networkStream = client.GetStream();
            byte[] dataInBytes = new byte[44100];
            networkStream.Read(dataInBytes, 0, dataInBytes.Length);

            short[] dataInShort = new short[22050];
            for(int i = 0; i < dataInBytes.Length; i += 2)
            {
                dataInShort[i / 2] = (short)(dataInBytes[i] | dataInBytes[i + 1] << 8);
            }

            DataAvailable?.Invoke(this, new DataAvailableEventArgs(AvailableDataType.Voice, dataInShort));
        }

        #endregion

    }
}