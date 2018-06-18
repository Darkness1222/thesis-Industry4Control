using Industry4Control.Constants;

namespace Industry4Control.BusinessLogic.Communication
{
    internal abstract class MessageBase
    {
        public MessageType MessageType;

        public MessageBase(MessageType type)
        {
            MessageType = type;
        }
    }
}
