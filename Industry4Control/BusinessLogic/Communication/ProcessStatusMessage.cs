using Industry4Control.Constants;

namespace Industry4Control.BusinessLogic.Communication
{
    internal class ProcessStatusMessage : MessageBase
    {
        public bool Status;

        public ProcessType ProcessType; 

        public ProcessStatusMessage(bool status, ProcessType type) : base(MessageType.ProcessStatus)
        {
            Status = status;
            ProcessType = type;
        }
    }
}