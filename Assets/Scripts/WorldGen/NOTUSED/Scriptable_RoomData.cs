//using System;
//using System.Collections.Generic;
//using UnityEngine;

//using MyBox;

//namespace Survival2D.WorldGen
//{
//    [Serializable]
//    public class RoomGenNode
//    {
//        [PositiveValueOnly] public int IndexToGenerate;
//        [Range(0, 100)] public float GenerationProbability;
//    }

//    // Each 
//    [CreateAssetMenu(fileName = "RoomData_", menuName = "Custom/WorldGeneration/RoomData")]
//    public class Scriptable_RoomData : ScriptableObject
//    {
//        #region WRAPPERS
//        [Serializable] private class AnchorRoomGen : ReorderableList<RoomGenNode> { }
//        [Serializable] private class DirectionalAnchorGen : ReorderableList<AnchorRoomGen> { }
//        #endregion

//        [Separator("Description Fields For Designers")]
//        [SerializeField] private string room_name;
//        [TextArea] [SerializeField] private string room_description;
//        [Separator]

//        [PositiveValueOnly] public int SubSceneIndex;
//        [SerializeField] private SubGenOrientation gen_orientation_mask;
//        public bool IsEndRoom = false;



//        [Foldout("LeftSide")]
//        [Tooltip("From down to up")]
//        [SerializeField] private DirectionalAnchorGen left_anchor_collection = null;
//        [Foldout("RightSide")]
//        [Tooltip("From down to up")]
//        [SerializeField] private DirectionalAnchorGen right_anchor_collection = null;
//        [Foldout("UpSide")]
//        [Tooltip("From left to right")]
//        [SerializeField] private DirectionalAnchorGen up_anchor_collection = null;
//        [Foldout("DownSide")]
//        [Tooltip("From left to right")]
//        [SerializeField] private DirectionalAnchorGen down_anchor_collection = null;

//        public Tuple<SubGenOrientation,List<RoomGenNode>[]>[] GetAnchorGenProbability()
//        {
//            var output_list = new List<Tuple<SubGenOrientation, List<RoomGenNode>[]>>();
//            if (gen_orientation_mask.HasFlag(SubGenOrientation.Left))
//            {
//                var side_anchor_collection = new List<RoomGenNode>[left_anchor_collection.Collection.Count];
//                for (int i = 0; i < side_anchor_collection.Length; i++)
//                {
//                    side_anchor_collection[i] = left_anchor_collection.Collection[i].Collection;
//                }

//                var tuple = new Tuple<SubGenOrientation, List<RoomGenNode>[]>(SubGenOrientation.Left, side_anchor_collection);
//                output_list.Add(tuple);
//            }

//            if (gen_orientation_mask.HasFlag(SubGenOrientation.Right))
//            {
//                var side_anchor_collection = new List<RoomGenNode>[right_anchor_collection.Collection.Count];
//                for (int i = 0; i < side_anchor_collection.Length; i++)
//                {
//                    side_anchor_collection[i] = right_anchor_collection.Collection[i].Collection;
//                }

//                var tuple = new Tuple<SubGenOrientation, List<RoomGenNode>[]>(SubGenOrientation.Right, side_anchor_collection);
//                output_list.Add(tuple);
//            }

//            if (gen_orientation_mask.HasFlag(SubGenOrientation.Up))
//            {
//                var side_anchor_collection = new List<RoomGenNode>[up_anchor_collection.Collection.Count];
//                for (int i = 0; i < side_anchor_collection.Length; i++)
//                {
//                    side_anchor_collection[i] = up_anchor_collection.Collection[i].Collection;
//                }

//                var tuple = new Tuple<SubGenOrientation, List<RoomGenNode>[]>(SubGenOrientation.Up, side_anchor_collection);
//                output_list.Add(tuple);
//            }

//            if (gen_orientation_mask.HasFlag(SubGenOrientation.Down))
//            {
//                var side_anchor_collection = new List<RoomGenNode>[down_anchor_collection.Collection.Count];
//                for (int i = 0; i < side_anchor_collection.Length; i++)
//                {
//                    side_anchor_collection[i] = down_anchor_collection.Collection[i].Collection;
//                }

//                var tuple = new Tuple<SubGenOrientation, List<RoomGenNode>[]>(SubGenOrientation.Down, side_anchor_collection);
//                output_list.Add(tuple);
//            }



//            return output_list.ToArray();
//        }


//    }
//}