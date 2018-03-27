using Industry4Control.Constants;
using System;

namespace Industry4Control.EventArguments
{
    internal class DataAvailableEventArgs : EventArgs
    {

        public DataAvailableEventArgs(AvailableDataType type, short[] data, byte controlByte)
        {
            AvailableDataType = type;
            Data = data;
            ControlByte = controlByte;
        }

        public DataAvailableEventArgs(AvailableDataType type, byte controlByte)
        {
            AvailableDataType = type;
            ControlByte = controlByte;
        }

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
        
    }
}
