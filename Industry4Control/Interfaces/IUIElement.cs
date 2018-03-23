using Industry4Control.EventArguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Industry4Control.Interfaces
{
    public interface IUiElement
    {
        event EventHandler<UIActionEventArgs> UIAction;
    }
}
