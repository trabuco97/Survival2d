using System;
using System.Collections.Generic;
using UnityEngine;


using Survival2D.Systems.Item;
using Survival2D.Systems.Item.Suit;
using Survival2D.Systems.Item.Equipment;

using Survival2D.Systems.Statistics;
using Survival2D.Systems.Statistics.Status;

namespace Survival2D.Systems.HealthArmor
{
    public class HealthArmorSystem : ISystemWithStatus, IDisposable
    {
        // fields
        private EquipmentSystem equipment_system = null;
        private StatusSystem status_system = null;
        private SuitObject suit_equipped = null;

        // events
        public event ArmorMethods OnZeroArmorRating;
        public event ArmorMethods OnArmorRatingModified;
        public event ArmorMethods OnArmorEquipped;

        public event HealthMethods OnHealthModified;
        public event HealthMethods OnZeroHealth;


        // stats that the system manages
        public Stat Armor { get; private set; } = new Stat(0f);
        public IncrementalStat ArmorRating { get; private set; } = new IncrementalStat(0f);
        public IncrementalStat Health { get; private set; } = new IncrementalStat(0f);

        public HealthArmorSystem(StatusSystem status_system, EquipmentSystem equipment_system, HealthArmorSystemData data = null)
        {
            this.status_system = status_system;
            this.equipment_system = equipment_system;

            var health_base = 0f;

            if (data != null)
            {
                health_base = data.Health;
            }

            Health = new IncrementalStat(health_base);

            if (!equipment_system.TryAddMethodToEquipType(ItemType.Suit, Handler_SuitEquipped))
            {
#if UNITY_EDITOR
                Debug.LogError("Error trying to get handlerlist at equipment sysyem at healtharmorsysytem");
#endif
            }
        }


        /// Mask:   0 = health
        ///         1 = armor
        ///         2 = armor rating
        public StatusLinkageToStat LinkModifierToStat(StatModifierData statModifier_data)
        {
            var modifier_linkage = new StatusLinkageToStat();
            modifier_linkage.modifier = statModifier_data.modifier;

            if (statModifier_data.stat_layerMask.HasFlag(StatModified.Stat0))
            {
                Health.AddModifier(statModifier_data.modifier);

                modifier_linkage.stats_linked.Add(Health);
                CheckHealthIfZero();
            }

            modifier_linkage.removal_methods.Add(delegate
            {
                CheckHealthIfZero();
            });

            return modifier_linkage;
        }

        public StatusLinkageToIncrementalStat LinkIncrementalModifierToStat(IncrementalStatModifierData statModifier_data)
        {
            var modifier_linkage = new StatusLinkageToIncrementalStat();
            modifier_linkage.modifier = statModifier_data.modifier;
            modifier_linkage.stats_linked = new List<IncrementalStat>();

            if (statModifier_data.stat_layerMask.HasFlag(StatModified.Stat0))
            {
                Health.AddIncrementalModifier(statModifier_data.modifier);

                modifier_linkage.stats_linked.Add(Health);
            }

            return modifier_linkage;
        }

        public void ModifyHealth(HealthModificationInfo modificationInfo)
        {
            bool is_damaged = modificationInfo.HealthDeltaValue < 0;
            var delta_value = modificationInfo.HealthDeltaValue;

            if (is_damaged && ArmorRating.ActualValue > 0)
            {
                delta_value = HealthSystemCalculator.GetDamageDealtReduction(delta_value, Armor.Value);
            }

            Health.AddToTemporary(delta_value, modificationInfo.DeltaValueType);
            if (modificationInfo.StatusApplied != null)
            {
                foreach (var status_name in modificationInfo.StatusApplied)
                {
                    status_system.AddStatus(status_name);
                }
            }

            if (!CheckHealthIfZero())
            {
                if (is_damaged)
                {
                    if (suit_equipped != null)
                    {
                        var armor_loss = HealthSystemCalculator.GetArmorRatingLoss(suit_equipped.SuitData.base_armor_rating_loss, delta_value);
                        ArmorRating.AddToTemporary(-armor_loss, IncrementalStat.AdditiveTemporaryType.Flat);

                        if (ArmorRating.ActualValue == 0)
                        {
                            suit_equipped.ActualRating = 0;

                            OnZeroArmorRating?.Invoke(new ArmorEventArgs(suit_equipped));
                        }

                        OnArmorRatingModified?.Invoke(new ArmorEventArgs(Armor.Value, ArmorRating.ActualValue, ArmorRating.Value));
                    }
                }
            }
        }
        public void Dispose()
        {
            equipment_system.RemoveMethodFromEquipType(ItemType.Suit, Handler_SuitEquipped);
        }

        public void RecoverArmor(float amount) { }


        private void Handler_SuitEquipped(EquipmentSlotArgs args)
        {
            var last_suitObject = args.LastItemInSlot as SuitObject;
            if (last_suitObject != null)
            {
                last_suitObject.ActualRating = ArmorRating.NoCalculatedActualValue;
            }

            suit_equipped = args.Slot.ItemContained as SuitObject;
            InitializeArmor();
        }

        // Pre:     suit_equipped != null
        // Post :   ArmorRating.Value != 0
        //          Armor.Value != 0
        private void InitializeArmor()
        {
            float base_armor_value = 0;
            // Search for sources of base armor

            if (suit_equipped != null)
            {
                base_armor_value += suit_equipped.SuitData.base_damage_reduction;

                ArmorRating.ChangeBaseValue(suit_equipped.SuitData.base_armor_rating);
                ArmorRating.SetTemporalValue(suit_equipped.ActualRating);
            }
            else
            {
                ArmorRating.ChangeBaseValue(0f);
            }

            Armor.ChangeBaseValue(base_armor_value);

            // add the value to the stat
            OnArmorEquipped?.Invoke(new ArmorEventArgs(Armor.Value, ArmorRating.ActualValue, ArmorRating.Value));
        }

        // Post:    Returns true if the item is 
        private bool CheckHealthIfZero()
        {
            if (Health.ActualValue == 0)
            {
                OnZeroHealth?.Invoke(new HealthEventArgs(Health.ActualValue, Health.Value));
                return true;
            }
            else
            {
                OnHealthModified?.Invoke(new HealthEventArgs(Health.ActualValue, Health.Value));
                return false;
            }
        }


    }
}