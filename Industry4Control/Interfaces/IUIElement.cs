using Industry4Control.BusinessLogic;
using Industry4Control.EventArguments;
using System;

namespace Industry4Control.Interfaces
{
    internal interface IUiElement
    {
        event EventHandler<UIActionEventArgs> UIAction;
        
        void SetFunctionStatus(CompareResult result);

        void SetStatusMessage(string message);
    }
}
