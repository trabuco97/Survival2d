using System;
using System.Collections.Generic;

namespace Survival2D.Saving
{
    [Serializable]
    public class ObjectSavedData
    {
        public string id;
        public Dictionary<string, object> componentData_database;
    }
}