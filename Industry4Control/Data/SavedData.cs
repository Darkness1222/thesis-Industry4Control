using Industry4Control.Constants;
using System;
using System.Collections.Generic;

namespace Industry4Control.Data
{
    [Serializable]
    class SavedData
    {
        public SavedData()
        {
            SavedFunctions = new Dictionary<ControlFunction, Voice>();
        }

        public IDictionary<ControlFunction, Voice> SavedFunctions
        {
            get;set;
        }

    }
}
