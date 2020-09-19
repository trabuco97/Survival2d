using UnityEngine;


using Survival2D.Saving;
using System.Runtime.Serialization;

namespace DEBUG.Saving
{
    public class DEBUG_TestSaving : MonoBehaviour, IRootComponentSaved
    {
        [System.Serializable]
        private class Wrapper : ISavingData
        {
            public int field;
            public string field2;

            public void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                info.AddValue("field", field, typeof(int));
                info.AddValue("field2", field2, typeof(string));
            }

            public Wrapper()
            {

            }

            public Wrapper(SerializationInfo info, StreamingContext context)
            {
                // Reset the property value using the GetValue method.
                field = (int)info.GetValue("field", typeof(int));
                field2 = (string)info.GetValue("field2", typeof(string));
            }
        }

        public int field = 1;
        public string field2 = "das";
        private SaveData data = new SaveData();



        public string ID => typeof(DEBUG_TestSaving).Name;

        [ContextMenu("Display")]
        private void Display()
        {
        }

        [ContextMenu("Persistent path")]
        private void Persistant()
        {
            Debug.Log(Application.persistentDataPath);
        }

        public void OnLoad(SavingDataNode data_node)
        {
            var data = data_node.Data as Wrapper;

            field = data.field;
            field2 = data.field2;

            //field = (int)data_node.Container[0];
            //field2 = (string)data_node.Container[1];
        }

        public SavingDataNode OnSave()
        {
            var data_node = new SavingDataNode();
            data_node.ID_Node = ID;

            var data = new Wrapper();
            data.field = field;
            data.field2 = field2;
            data_node.Data = data;

            //data_node.Container = new object[2];
            //data_node.Container[0] = field;
            //data_node.Container[1] = field2;

            return data_node;
        }
    }
}