//using System;
//using System.Collections.Generic;
//using UnityEngine;

//using MyBox;

//namespace Survival2D.WorldGen
//{

//    public enum Orientation
//    {
//        None    = 0,
//        Left    = -1,
//        Right   = 1,
//        Up      = -2,
//        Down    = 2
//    }

//    public class RoomProbData
//    {
//        public string tag;
//        /// <summary>
//        /// Number between 0 - 100
//        /// </summary>
//        public int chance;  
//    }



//    public class Anchor
//    {
//        public int Order { get; private set; }
//        public Orientation Orientation { get; private set; }    

//        public bool HasRequired { get; private set; }
//        /// <summary>
//        /// Used by all specific rooms that need to generate another specific rooms
//        /// Used to create unique layouts
//        /// Pre: Summation of all [i].chance must be 100
//        /// </summary>
//        public RoomProbData[] RoomGenProbabilityCollection { get; private set; }

//        public Anchor(Orientation orientation, int order = -1, RoomProbData[] roomProbs = null)
//        {
//            Orientation = orientation;
//            Order = order;

//            HasRequired = roomProbs != null;
//            RoomGenProbabilityCollection = roomProbs;
//        }
//    }

//    /// <summary>
//    /// 
//    /// 
//    /// For anchors: all anchors that have the same orientation
//    /// Given a list of all anchor of same orientation in the same order as AnchorList follow this ruleset, depending on the orientation
//    /// Horizontal: from down to up 
//    /// Vertical:   from left to right
//    /// </summary>
//    //public class RoomData
//    //{
//    //    private Anchor[] anchor_collection;
//    //    private int[] array_data;

//    //    public bool HasTag { get; private set; }
//    //    public string Tag { get; private set; }
//    //    public Anchor[] RemainingAnchors 
//    //    { 
//    //        get
//    //        {
//    //            var remaining_list = new List<Anchor>();
//    //            AddAnchorRangeToList(remaining_list, anchor_collection, array_data[0], array_data[1]);
//    //            AddAnchorRangeToList(remaining_list, anchor_collection, array_data[2], array_data[3]);
//    //            AddAnchorRangeToList(remaining_list, anchor_collection, array_data[4], array_data[5]);
//    //            AddAnchorRangeToList(remaining_list, anchor_collection, array_data[6], array_data[7]);

//    //            return remaining_list.ToArray();
//    //        }
//    //    }

//    //    public int RemainingAnchorCount { get; private set; }

//    //    /// <summary>
//    //    /// 
//    //    /// </summary>
//    //    /// <param name="anchors"></param>
//    //    /// <param name="anchor_collection_data">
//    //    /// [0] = left_start_index
//    //    /// [1] = left_count
//    //    /// [2] = right_start_index
//    //    /// [3] = right_count
//    //    /// [4] = up_start_index
//    //    /// [5] = up_count
//    //    /// [6] = down_start_index
//    //    /// [7] = down_count
//    //    /// Pre: length = 8
//    //    /// </param>
//    //    /// <param name="tag"></param>
//    //    public RoomData(Anchor[] anchors, int[] collection_data, string tag = null)
//    //    {
//    //        HasTag = tag != null;
//    //        Tag = tag;

//    //        anchor_collection = anchors;
//    //        array_data = collection_data;

//    //        RemainingAnchorCount = anchor_collection.Length;
//    //    }

//    //    // TODO: Can be improved 
//    //    public void RemoveAnchor(Anchor anchor)
//    //    {
//    //        bool is_found = false;
//    //        int i = 0;
//    //        while (!is_found && i < anchor_collection.Length)
//    //        {
//    //            if (anchor == anchor_collection[i])
//    //            {
//    //                // Determine where range it is
//    //                int range_type = 0;
//    //                bool is_range_found = false;
//    //                while (!is_range_found && range_type < 8)
//    //                {
//    //                    int start = array_data[range_type];
//    //                    int end = start + array_data[range_type + 1] - 1;
//    //                    if (i >= start && i < end)
//    //                    {
//    //                        is_range_found = true;
//    //                    }
//    //                    else
//    //                    {
//    //                        range_type += 2;
//    //                    }
//    //                }

//    //                // Update the array data
//    //                UpdateRoomData(range_type, i);
//    //                is_found = true;
//    //            }
//    //            else
//    //            {
//    //                i++;
//    //            }
//    //        }

//    //    }

//    //    /// <summary>
//    //    /// 
//    //    /// </summary>
//    //    /// <param name="type">
//    //    /// 0 left,
//    //    /// 2 right,
//    //    /// 4 up,
//    //    /// 6 down
//    //    /// </param>
//    //    /// <param name="index_modified"></param>
//    //    private void UpdateRoomData(int type, int index_modified)
//    //    {
//    //        int start = array_data[type];
//    //        int end = start + array_data[type + 1] - 1;

//    //        for (int i = index_modified; i <= end; i++)
//    //        {


//    //        }
//    //    }

//    //    private static void AddAnchorRangeToList(List<Anchor> list_toModify, Anchor[] array, int start_index, int count)
//    //    {
//    //        if (count == 0) return;
//    //        Anchor[] sub_range = new Anchor[count];
//    //        Array.Copy(array, start_index, sub_range, 0, count);

//    //        list_toModify.AddRange(sub_range);
//    //    }
//    //}

//    public class RoomData
//    {
//        private Tuple<bool, Anchor>[] anchor_collection;
//        /// <summary>
//        /// [0] = left_start_index
//        /// [1] = left_count
//        /// [2] = right_start_index
//        /// [3] = right_count
//        /// [4] = up_start_index
//        /// [5] = up_count
//        /// [6] = down_start_index
//        /// [7] = down_count
//        /// Pre: length = 8
//        /// </summary>
//        private int[] array_data;

//        public bool HasTag { get; private set; }
//        public string Tag { get; private set; }
//        public Anchor[] RemainingAnchors
//        {
//            get
//            {
//                var remaining_list = new List<Anchor>();
//                for (int i = 0; i < anchor_collection.Length; i++)
//                {
//                    var pair = anchor_collection[i];
//                    if (pair.Item1)
//                    {
//                        remaining_list.Add(pair.Item2);
//                    }
//                }

//                return remaining_list.ToArray();
//            }
//        }

//        public int LeftAnchorCount { get { return array_data[1]; } }
//        public int RightAnchorCount { get { return array_data[3]; } }
//        public int UpAnchorCount { get { return array_data[5]; } }
//        public int DownAnchorCount { get { return array_data[7]; } }
//        public int RemainingAnchorsCount { get; private set; }

//        public RoomData(Anchor[] anchors, int[] collection_data, string tag = null)
//        {
//            HasTag = tag != null;
//            Tag = tag;

//            anchor_collection = new Tuple<bool, Anchor>[anchors.Length];
//            for (int i = 0; i < anchors.Length; i++)
//            {
//                anchor_collection[i] = new Tuple<bool, Anchor>(true, anchors[i]);
//            }

//            array_data = collection_data;
//            RemainingAnchorsCount = anchor_collection.Length;
//        }

//        ///// <summary>
//        ///// 
//        ///// </summary>
//        ///// <param name="anchor"></param>
//        ///// <returns>The index of the anchor in the subarray of the specific orientation </returns>
//        //public int GetAnchorSelected(Anchor anchor)
//        //{
//        //    int output = -1;
//        //    int i = 0;
//        //    while (output == -1 && i < anchor_collection.Length)
//        //    {
//        //        var pair = anchor_collection[i];
//        //        if (!pair.Item1 || pair.Item2 != anchor)
//        //        {
//        //            i++;
//        //        }
//        //        else
//        //        {
//        //            anchor_collection[i] = new Tuple<bool, Anchor>(false, null);
//        //            RemainingAnchorsCount--;
//        //            output = CalculateSubIndex(array_data, i);
//        //        }
//        //    }

//        //    return output;
//        //}

//        public int LinkToRoom(RoomData room_toLink, Anchor anchor_selected)
//        {
//            int output = -1;
//            int i = 0;
//            while (i < anchor_collection.Length)
//            {
//                var pair = anchor_collection[i];
//                if (!pair.Item1 || pair.Item2 != anchor_selected)
//                {
//                    i++;
//                }
//                else
//                {
//                    anchor_collection[i] = new Tuple<bool, Anchor>(false, null);
//                    RemainingAnchorsCount--;
//                    output = CalculateSubIndex(array_data, i, out var orientation);



//                    room_toLink.anchor_collection = 
//                }
//            }
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="collection_data"></param>
//        /// <param name="index"></param>
//        /// <param name="orientation">0 - left 2 - right 4 - up 6 - down</param>
//        /// <returns></returns>
//        private static int CalculateSubIndex(int[] collection_data, int index, out int orientation)
//        {
//            for (int i = 0; i < 8; i += 2)
//            {
//                int start = collection_data[i];
//                int end = start + collection_data[i + 1];
                
//                if (index >= start && index < end)
//                {
//                    orientation = i;
//                    return index - start;
//                }

//            }

//            orientation = -1;
//            return orientation;
//        }


//        private static Anchor[] GetSubArray(Tuple<bool, Anchor>[] anchor_collection, int[] collection_data, int orientation)
//        {
//            int start = collection_data[orientation];
//            int count = collection_data[orientation + 1];

//            int index = 0;
//            for (int i = start; i < start + count; i++)
//            {
//                output[index++] = anchor_collection[i].Item2; 
//            }

//            return output;
//        }
//    }

//    public class RoomGenerationData
//    {
//        public int index;
//        public RoomData room_data;
//        /// <summary>
//        /// Used by the room generation to see how is likely to spawn the room
//        /// </summary>
//        public int chance;

//    }

//}