//using System;
//using UnityEngine;

//using MyBox;

//namespace Survival2D.WorldGen
//{
//    [CreateAssetMenu(fileName = "SceneRoom_Database", menuName = "Custom/WorldGeneration/SceneRoomDatabase")]
//    public class Scriptable_SceneRoomDatabase : ScriptableObject
//    {
//        #region WRAPPERS
//        [Serializable] public class RoomDataCollection : ReorderableList<Scriptable_RoomData> { }

//        [Serializable] public class DirectionalSceneData
//        {
//            [PositiveValueOnly] public int max_rooms; 
//            public RoomDataCollection persistent_rooms;
//        }

//        #endregion

//        public Scriptable_RoomData start_room;
//        public Scriptable_RoomData[] room_collections;

//        [Foldout("LeftSide Data")]
//        [SerializeField] private DirectionalSceneData left_data = null;
//        [Foldout("RightSide Data")]
//        [SerializeField] private DirectionalSceneData right_data = null;
//        [Foldout("UpSide Data")]
//        [SerializeField] private DirectionalSceneData up_data = null;
//        [Foldout("DownSide Data")]
//        [SerializeField] private DirectionalSceneData down_data = null;


//    }
//}