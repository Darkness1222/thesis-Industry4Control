using Industry4Control.Constants;

namespace Industry4Control.BusinessLogic.Communication
{
    internal class OverallStatusMessage : MessageBase
    {
        public bool Function1;
        public bool Function2;
        public bool Function3;

        public OverallStatusMessage(bool f1, bool f2, bool f3): base(MessageType.OverallStatus)
        {
            Function1 = f1;
            Function2 = f2;
            Function3 = f3;
        }
    }
}
