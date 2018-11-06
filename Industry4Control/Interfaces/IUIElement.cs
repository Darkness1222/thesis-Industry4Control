using Industry4Control.BusinessLogic;
using Industry4Control.EventArguments;
using System;

namespace Industry4Control.Interfaces
{
    internal interface IUiElement
    {
        event EventHandler<UIActionEventArgs> UIAction;

        string PlcAddress { get; }

        void RefreshFunctionStatus();

        void SetStatusMessage(string message);
    }
}
