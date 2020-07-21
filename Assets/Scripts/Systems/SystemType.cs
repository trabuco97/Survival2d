using System;

using Survival2D.Systems.Item.Inventory;
using Survival2D.Systems.Item.Equipment;
using Survival2D.Systems.HealthArmor;

namespace Survival2D.Systems
{
    public enum SystemType
    {
        None        = 0 << 0,
        Health      = 1 << 0, 
        Inventory   = 1 << 1,
        Equipment   = 1 << 2
    }


    public static class SystemToTypeConverter
    {
        public static Type GetSystemType(SystemType type)
        {
            switch (type)
            {
                case SystemType.Health:
                    return typeof(HealthArmorSystem);
                case SystemType.Inventory:
                case SystemType.Equipment:
                default:
                    return null;
            }
        }
    }
}

