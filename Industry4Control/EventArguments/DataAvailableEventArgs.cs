using Industry4Control.Constants;
using System;
using System.Net.Sockets;

namespace Industry4Control.EventArguments
{
    internal class DataAvailableEventArgs : EventArgs
    {
        public DataAvailableEventArgs(AvailableDataType type, byte controlByte, TcpClient client, short[] data)
        {
            AvailableDataType = type;
            ControlByte = controlByte;
            TcpClient = client;
            Data = data;
        }

        public DataAvailableEventArgs(AvailableDataType type, byte controlByte, TcpClient client) 
            : this(type, controlByte, client, null) { }

        public AvailableDataType AvailableDataType
        {
            get;
            set;
        }

        public short[] Data
        {
            get;
            set;
        }

        public byte ControlByte
        {
            get;
            set;
        }

        public TcpClient TcpClient
        {
            get;
        }

    }
}
