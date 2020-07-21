using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Survival2D.Systems.Item.Equipment
{
    public class EquipableSlotEvent : UnityEvent<EquipmentSlot, ItemObject> { }
}
