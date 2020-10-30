//using System.Collections.Generic;
//using UnityEngine;

//namespace Survival2D.WorldGen
//{
//    public class WorldGenerationManager
//    {
//        #region WRAPPERS
//        private class RoomFromGenWrappers
//        {
//            public RoomGenerationData toGen_data;
//            public TreeRoomNode toGen_node;
//        }

//        private class SpecificRoomGenPair
//        {
//            public RoomGenerationData specific_room;
//            public List<RoomFromGenWrappers> room_fromGen_collection = new List<RoomFromGenWrappers>();
//        }
//        #endregion

//        private const int CHANCE_CURRENT_ROOM = 40;
//        private const int CHANCE_PREVIOUS_ROOM = 35;
//        private const int CHANCE_CURRENT_AVAILABLE = 25;

//        private RoomGenerationData start_room;
//        /// <summary>
//        /// Pre: sum of all random_roomData_collection[i].chance must be 100
//        /// </summary>
//        private RoomGenerationData[] random_roomData_collection;
//        private RoomGenerationData[] specific_roomData_collection;


//        // TODO: Optimize the algorithm
//        public void GenerateRoomStructure(int seed, int min_rooms, int total_rooms_available)
//        {
//            Random.InitState(seed);

//            var rooms_fromGenerate_collection = new List<RoomGenerationData>();         // Contains all rooms that have anchors with no rooms attached
//            var tree_collection = new TreeRoomNode[total_rooms_available];              // uses RoomGeneration.index as index
//            var specific_dictionary = new Dictionary<string, RoomGenerationData>();

//            var root_node = new TreeRoomNode(start_room.index, start_room.room_data);
//            int rooms_toGenerate = start_room.room_data.RemainingAnchorsCount;
//            int specific_gen_iteration = 0;

//            // Initialize local variables
//            foreach (var specific_room in specific_roomData_collection)
//            {
//                specific_dictionary.Add(specific_room.room_data.Tag, specific_room);
//            }

//            //var potencial_rooms = new SpecificRoomGenPair[specific_roomData_collection.Length];
//            //for (int i = 0; i < potencial_rooms.Length; i++)
//            //{
//            //    potencial_rooms[i] = new SpecificRoomGenPair { specific_room = specific_roomData_collection[i] };
//            //}

//            rooms_fromGenerate_collection.Add(start_room);
//            tree_collection[root_node.Index] = root_node;

//            // Generate all rooms until hiting the min_rooms threshold
//            while (rooms_fromGenerate_collection.Count + rooms_toGenerate < min_rooms)
//            {

//                // Decide where to generate a new room
//                RoomGenerationData room_fromGen = null;
//                bool is_room_found = false;
//                int index_room_fromGen = 0;
//                while (!is_room_found)
//                {
//                    int percent = (int)(Random.value * 100);
//                    // Check the current room
//                    if (percent <= CHANCE_CURRENT_ROOM)
//                    {
//                        index_room_fromGen = rooms_fromGenerate_collection.Count - 1;
//                    }
//                    // Check the previous room
//                    else if (percent <= CHANCE_CURRENT_ROOM + CHANCE_PREVIOUS_ROOM)
//                    {
//                        int previous_room_index = rooms_fromGenerate_collection.Count - 2;
//                        index_room_fromGen = previous_room_index >= 0 ? previous_room_index : 0;

//                    }
//                    // Check generated rooms
//                    else if (percent <= CHANCE_CURRENT_ROOM + CHANCE_PREVIOUS_ROOM + CHANCE_CURRENT_AVAILABLE)
//                    {
//                        int current_available = rooms_fromGenerate_collection.Count;
//                        index_room_fromGen = Random.Range(0, current_available);
//                    }

//                    room_fromGen = rooms_fromGenerate_collection[index_room_fromGen];
//                    is_room_found = room_fromGen.room_data.RemainingAnchorsCount > 0;
//                }

//                // set the local variables for the chosen room
//                TreeRoomNode room_node = tree_collection[room_fromGen.index];

//                // Check if the room can be used to generate an specific room
//                // The room is usable if it doesnt have a matching 
//                //bool is_room_used_forGen = true;
//                //foreach (var anchor_data in room_fromGen.room_data.AnchorList)
//                //{
//                //    var orientation_toValidate = (Orientation)(-(int)anchor_data.Orientation);

//                //    foreach (var potential_data in potencial_rooms)
//                //    {
//                //        foreach (var specific_anchor in potential_data.specific_room.room_data.AnchorList)
//                //        {
//                //            if (!specific_anchor.HasRequired && specific_anchor.Orientation == orientation_toValidate)
//                //            {
//                //                is_room_used_forGen = false;
//                //                potential_data.room_fromGen_collection.Add(room_fromGen);
//                //                break;
//                //            }
//                //        }
//                //    }


//                //}

//                // Decide what anchor to use following these steps:
//                // - whether the anchor has an specific room to generate
//                // - pick a random anchor
//                // Then it is removed from the RoomData
//                Anchor anchor_toUse = null;
//                int anchor_index = 0;

//                // 1-
//                bool has_specific_tag = false;
//                while (!has_specific_tag && anchor_index < room_fromGen.room_data.RemainingAnchors.Length)
//                {
//                    var anchor = room_fromGen.room_data.RemainingAnchors[anchor_index];
//                    if (anchor.HasRequired)
//                    {
//                        anchor_toUse = anchor;
//                        has_specific_tag = true;
//                    }
//                    else
//                    {
//                        anchor_index++;
//                    }
//                }

//                // 2-
//                if (anchor_toUse == null)
//                {
//                    anchor_index = Random.Range(0, room_fromGen.room_data.RemainingAnchors.Length);
//                    anchor_toUse = room_fromGen.room_data.RemainingAnchors[anchor_index];
//                }

//                // Remove anchor and remove the room origin from the list of rooms generated if doesnt have any anchors
//                int anchor_position_childNode = room_fromGen.room_data.GetAnchorSelected(anchor_toUse);
//                if (room_fromGen.room_data.RemainingAnchorsCount <= 0)
//                {
//                    rooms_fromGenerate_collection.Remove(room_fromGen);
//                }


//                // Decide what room to generate following these steps, given an specific anchor:
//                // - whether the anchor has an specific room to generate
//                // - check if can generate an specific room and check whether there is a probability to do it
//                // - generating an random index and getting it from the random room collection
//                RoomGenerationData room_toGen = null;

//                // 1-
//                if (anchor_toUse.HasRequired)
//                {
//                    // Determine the room to generate and if it isnt generated already
//                    int new_chance = Random.Range(0, 101);
//                    int specific_chance = 0;
//                    int i = 0;
//                    bool is_room_chosen = false;
//                    while (!is_room_chosen && i < anchor_toUse.RoomGenProbabilityCollection.Length)
//                    {
//                        specific_chance += anchor_toUse.RoomGenProbabilityCollection[i].chance;
//                        if (specific_chance <= new_chance)
//                        {
//                            is_room_chosen = true;
//                        }
//                        else
//                        {
//                            i++;
//                        }
//                    }

//                    var chosen_room = anchor_toUse.RoomGenProbabilityCollection[i];
//                    if (specific_dictionary.ContainsKey(chosen_room.tag))
//                    {
//                        room_toGen = specific_dictionary[chosen_room.tag];
//                        specific_dictionary.Remove(chosen_room.tag);
//                    }
//                }

//                // 2-
//                if (room_toGen == null)
//                {
//                    string room_toRemove = string.Empty;

//                    foreach (var specific_room in specific_dictionary.Values)
//                    {
//                        // Check if the specific room has an anchor that matches with the room to gen orientation
//                        foreach (var specific_anchor in specific_room.room_data.RemainingAnchors)
//                        {
//                            if (!specific_anchor.HasRequired && IsRoomMatchingWithAnchor(specific_room, anchor_toUse))
//                            {
//                                // Calculate the current chance to know if the room can be generated
//                                int chance_toSpe = GetSpecificRoomChance(specific_gen_iteration + 1);

//                                // When the specific can be generated, breaks the loop
//                                if (chance_toSpe <= Random.Range(0, 101))
//                                {
//                                    room_toGen = specific_room;
//                                    room_toRemove = specific_room.room_data.Tag;
//                                    specific_gen_iteration = -1;
//                                    break;

//                                }
//                            }
//                        }
//                    }

//                    if (room_toRemove != string.Empty)
//                    {
//                        specific_dictionary.Remove(room_toRemove);
//                    }
//                }


//                // 3-
//                if (room_toGen == null)
//                {
//                    int i = 0;
//                    int random = Random.Range(0, 101);
//                    int chance = 0;

//                    is_room_found = false;

//                    while (!is_room_found && i < random_roomData_collection.Length)
//                    {
//                        var room_data = random_roomData_collection[i];
//                        chance += room_data.chance;

//                        if (chance <= random && IsRoomMatchingWithAnchor(room_data, anchor_toUse))
//                        {
//                            is_room_found = true;
//                            room_toGen = room_data;
//                        }
//                        else
//                        {
//                            i++;
//                        }
//                    }
//                }

//                // Error handling when a room isnt found
//                // ALWAYS HAS TO HAVE A ROOM TO GENERATE
//                // TODO: i dunno

//                if (room_toGen == null) Debug.LogError("error with generation");

//                // Update the variables of the chosen room data 

//                // Update the local variables for the chosen room
//                if (rooms)
//                rooms_fromGenerate_collection.Add(room_toGen);
//                rooms_toGenerate += room_toGen.room_data.RemainingAnchorsCount - 2;

//                var newRoom_node = new TreeRoomNode(room_toGen.index, room_toGen.room_data);
//                room_node.AddChild(anchor_toUse.Orientation, newRoom_node, anchor_position_childNode);
//                tree_collection[newRoom_node.Index] = newRoom_node;

//                specific_gen_iteration++;
//            }



//        }

//        private static bool IsRoomMatchingWithAnchor(RoomGenerationData room, Anchor anchor)
//        {
//            var orientation_toValidate = (Orientation)(-(int)anchor.Orientation);
//            foreach (var anchor_fromRoom in room.room_data.RemainingAnchors)
//            {
//                if (anchor.Orientation == orientation_toValidate)
//                    return true;
//            }

//            return false;
//        }

//        // TODO
//        private static int GetSpecificRoomChance(int iteration)
//        {
//            return iteration * 10;
//        }


//    }
//}