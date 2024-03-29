﻿using UnityEngine;
using System.Collections;

using Survival2D.Systems.Tools;

namespace Survival2D.Systems.Item.Harpoon
{
    public class HarpoonObject : ItemObject, IToolItemObject
    {
        private Scriptable_HarpoonData harpoon_data = null;

        public Scriptable_HarpoonData HarpoonData 
        { 
            get
            {
                if (harpoon_data == null)
                {
                    harpoon_data = ItemData as Scriptable_HarpoonData;
                }

                return harpoon_data;
            }
        }

        public ItemType ToolType => Type;
        public HarpoonObject(ItemType type, int id, uint current_stack, bool inicialize_data = true) : base(type, id, current_stack, inicialize_data) { }
    }
}