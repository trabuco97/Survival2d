using System;
using System.Collections.Generic;

namespace Survival2D.Systems.Item
{
    public static class ItemListReorder
    {
        public static ItemObject[] ReorderItems(ItemObject[] items_toReorder)
        {
            var list = new List<ItemObject>(items_toReorder);
            list.Sort((ItemObject a, ItemObject b) => 
            {
                if ((int)a.Type < (int)b.Type)
                {
                    return -1;
                }
                else if ((int)a.Type > (int)b.Type)
                {
                    return 1;

                }
                else
                {
                    if (a.ID < b.ID)
                        return -1;
                    else if (a.ID > b.ID)
                        return 1;
                    else
                        return 0;
                }
            });

            return list.ToArray();
        }
    }
}