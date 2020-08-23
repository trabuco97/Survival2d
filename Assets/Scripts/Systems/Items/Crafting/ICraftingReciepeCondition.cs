using System.Collections.Generic;

namespace Survival2D.Systems.Item.Crafting
{
    public interface ICraftingReciepeCondition
    {
        bool CheckCondition(List<ItemObject> item_toCheck);
    }
}