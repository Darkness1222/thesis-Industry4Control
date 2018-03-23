using Industry4Control.Constants;
using System;

namespace Industry4Control.EventArguments
{
    public class UIActionEventArgs : EventArgs
    {
        public UIActionEventArgs(UIActionType action)
        {
            Action = action;
        }

        public UIActionType Action
        {
            get;
            set;
        }
    }
}
