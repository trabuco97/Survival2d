using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Survival2D.Saving
{
    [Serializable]
    public class SaveData : ISerializable
    {
        private List<ObjectSavedData> objectData_collection = new List<ObjectSavedData>();
        private Dictionary<string, ObjectSavedData> objectData_database = new Dictionary<string, ObjectSavedData>();

        public void SaveObjectData(ObjectSavedData data)
        {
            if (objectData_database.ContainsKey(data.id))
            {
                var aux = objectData_database[data.id];
                objectData_collection.Remove(aux);
                objectData_database[data.id] = data;
            }
            else
            {
                objectData_database.Add(data.id, data);
            }

            objectData_collection.Add(data);
        }

        public bool TryLoadObjectData(string id, out ObjectSavedData data)
        {
            data = null;
            if (objectData_database.TryGetValue(id, out var savedData))
            {
                data = savedData;
                return true;
            }

            return false;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(objectData_collection), objectData_collection, typeof(List<ObjectSavedData>));
        }

        public SaveData()
        {
        }


        public SaveData(SerializationInfo info, StreamingContext context)
        {
            objectData_collection = (List<ObjectSavedData>)info.GetValue(nameof(objectData_collection), typeof(List<ObjectSavedData>));

            foreach (var data in objectData_collection)
            {
                objectData_database.Add(data.id, data);
            }
        }
    }
}