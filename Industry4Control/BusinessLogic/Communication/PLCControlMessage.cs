using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Industry4Control.Constants;

namespace Industry4Control.BusinessLogic.Communication
{
    internal class PLCControlMessage : MessageBase
    {
        public ControlFunction Function { get; }

        public bool Value { get; } 

        public PLCControlMessage(ControlFunction function, bool value) : base(MessageType.PLCControl)
        {
            Function = function;
            Value = value;
        }
    }
}
