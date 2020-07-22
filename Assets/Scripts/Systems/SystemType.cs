using System;

using Survival2D.Systems.Item.Inventory;
using Survival2D.Systems.Item.Equipment;
using Survival2D.Systems.HealthArmor;

namespace Survival2D.Systems
{
    public enum SystemType
    {
        None        = 0,
        Health      = 1 << 0, 
        Inventory   = 1 << 1,
        Equipment   = 1 << 2
    }

    // TODO
    // This is baddd , need some rework
    public static class SystemTypeConverter
    {
        public static Type GetTypeFromSystem(SystemType type)
        {
            switch (type)
            {
                case SystemType.Health:
                    return typeof(HealthArmorSystemBehaviour);
                case SystemType.Inventory:
                case SystemType.Equipment:
                default:
                    return null;
            }
        }

        public static SystemType GetSystemFromType(Type type)
        {
            if (type == typeof(HealthArmorSystem))
            {
                return SystemType.Health;
            }
            else
            {
                return SystemType.None;
            }

        }
    }
}

