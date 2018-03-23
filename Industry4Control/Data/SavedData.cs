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
            SavedFunctions = new Dictionary<ControlFunction, ParameterVector[]>();
        }

        public IDictionary<ControlFunction, ParameterVector[]> SavedFunctions
        {
            get;set;
        }

    }
}
