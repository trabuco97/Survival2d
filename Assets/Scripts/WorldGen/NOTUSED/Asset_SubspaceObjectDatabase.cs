//using System.Collections.Generic;
//using UnityEngine;

//using MyBox;

//namespace Survival2D.WorldGeneration
//{
//    [CreateAssetMenu(fileName = "SubspaceDatabase", menuName = "Custom/WorldGen/SubspaceDatabase")]
//    public class Asset_SubspaceObjectDatabase : ScriptableObject, ISerializationCallbackReceiver
//    {
//        #region WRAPPERS
//        [System.Serializable]
//        private class SubsceneWrapper
//        {
//            public int id;
//            public GameObject prefab;
//        }

//        [System.Serializable]
//        private class SubsceneWrapperCollection : ReorderableList<SubsceneWrapper> { }
//        #endregion

//        [SerializeField] private SubsceneWrapperCollection collection = null;
//        private GameObject[] subsceneObject_collection = null;


//        public void OnAfterDeserialize()
//        {
//            InitializeDatabase();
//        }

//        public void OnBeforeSerialize()
//        {
//        }


//        public GameObject GetPrefabByID(int id)
//        {
//            return subsceneObject_collection[id];
//        }

//        public SubspaceData GetDataByID(int id)
//        {
//            return GetPrefabByID(id).GetComponent<SubspaceGeneratedBehaviour>().GetData();
//        }

//        [ButtonMethod]
//        private void InitializeDatabase()
//        {
//            // evaluate the indexes of collection
//            if (!TryPopulateCollection())
//            {
//                Debug.LogError("ERROR: Assigning invalid id in field <collection>");
//            }
//        }

//        private bool TryPopulateCollection()
//        {
//            var list = collection.Collection;
//            subsceneObject_collection = new GameObject[list.Count];

//            foreach (var wrapper in list)
//            {
//                if (wrapper.id < 0 || wrapper.id >= list.Count || list[wrapper.id] == null) return false;

//                subsceneObject_collection[wrapper.id] = wrapper.prefab;
//            }

//            return true;
//        }



//    }
//}