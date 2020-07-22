

namespace Survival2D.Systems.Item
{
    // Usead by systems that use item_objects
    public interface IItemSystem
    {
        bool IsItemUsedInSystem(ItemObject item_toEvaluate);
    }
}