//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;

//namespace Survival2D.WorldGen
//{

//    public delegate void TreeNodeDelegate(Orientation origin, int index);

//    public class TreeRoomNode
//    {
//        private Dictionary<Orientation, TreeRoomNode[]> childs_container = new Dictionary<Orientation, TreeRoomNode[]>();

//        public int Index { get; private set; }
//        public bool HasChilds { get; private set; }
//        public ReadOnlyCollection<Tuple<Orientation, TreeRoomNode[]>> ChildsCollection
//        {
//            get
//            {
//                var output = new Tuple<Orientation, TreeRoomNode[]>[4];
//                int i = 0;
//                foreach (var pair in childs_container)
//                {
//                    output[i] = new Tuple<Orientation, TreeRoomNode[]>(pair.Key, pair.Value);
//                    i++;
//                }

//                return new ReadOnlyCollection<Tuple<Orientation, TreeRoomNode[]>>(output);
//            }
//        }

//        public TreeRoomNode(int index, RoomData data)
//        {
//            Index = index;
//            HasChilds = false;

//            childs_container.Add(Orientation.Left, new TreeRoomNode[data.LeftAnchorCount]);
//            childs_container.Add(Orientation.Right, new TreeRoomNode[data.RightAnchorCount]);
//            childs_container.Add(Orientation.Up, new TreeRoomNode[data.UpAnchorCount]);
//            childs_container.Add(Orientation.Down, new TreeRoomNode[data.DownAnchorCount]);
//        }

//        /// <summary>
//        /// Pre: <sub_index> >= 0 && <sub_index> < childs_container[<orientation>].Length
//        /// </summary>
//        /// <param name="orientation"></param>
//        /// <param name="index"></param>
//        public void AddChild(Orientation orientation, TreeRoomNode node, int subArray_index)
//        {
//            HasChilds = true;
//            var collection = childs_container[orientation];
//            collection[subArray_index] = node;
//        }


//        /// <summary>
//        /// Pre: <sub_index> >= 0 && <sub_index> < childs_container[<orientation>].Length
//        /// </summary>
//        /// <param name="orientation"></param>
//        /// <param name="index"></param>
//        public TreeRoomNode AddChild(Orientation orientation, int index, RoomData data, int subArray_index)
//        {
//            var node = new TreeRoomNode(index, data);
//            AddChild(orientation, node, subArray_index);
//            return node;
//        }


//        //public static void IterateThroughTree(TreeRoomNode root, TreeNodeDelegate action, Orientation orientation = Orientation.None)
//        //{
//        //    action(or, root.Index);
//        //    foreach (var pair in childs_container)
//        //    {



//        //    }

//        //}

//    }
//}