using System;

using Survival2D.Systems.Item.Inventory;
using Survival2D.Systems.Item.Equipment;
using Survival2D.Systems.HealthArmor;
using Survival2D.Physics.Movement;

namespace Survival2D
{
    public enum SystemType
    {
        Health      ,
        Inventory   ,
        Equipment   ,
        Movement    ,
        Tool        ,
        MAX_SYSTEMS ,
    }

    // Implemented by all systems classes
    public interface ISystemBehaviour
    {
        SystemType SystemType { get; }
    }




    //// TODO
    //// This is baddd , need some rework
    //public static class SystemTypeConverter
    //{
    //    public static Type GetTypeFromSystem(SystemType type)
    //    {
    //        switch (type)
    //        {
    //            case SystemType.Health:
    //                return typeof(HealthArmorSystemBehaviour);
    //            case SystemType.Movement:
    //                return typeof(MovementSystemControllerBehaviour);
    //            case SystemType.Inventory:
    //            case SystemType.Equipment:
    //            default:
    //                return null;
    //        }
    //    }

    //    public static SystemType GetSystemFromType(Type type)
    //    {
    //        if (type == typeof(HealthArmorSystem))
    //        {
    //            return SystemType.Health;
    //        }
    //        else if (type == typeof(MovementSystemControllerBehaviour))
    //        {
    //            return SystemType.Movement;
    //        }
    //        else
    //        {
    //            return SystemType.None;
    //        }

    //    }
    //}
}

