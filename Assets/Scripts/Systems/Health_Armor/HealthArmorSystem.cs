using UnityEngine;
using System.Collections.Generic;

using Survival2D.Systems.Item;
using Survival2D.Systems.Item.Suit;
using Survival2D.Systems.Item.Equipment;

using Survival2D.Systems.Statistics;
using Survival2D.Systems.Statistics.Status;

namespace Survival2D.Systems.HealthArmor
{
    public class HealthArmorSystem : ISystemWithStatus
    {
        // fields
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

        public HealthArmorSystem(StatusSystem status_system, EquipmentSystem equipment_system, float base_health)
        {
            this.status_system = status_system;
            Health = new IncrementalStat(base_health);

            // Link the armor equip and unequip, save the current data and inicialize new armor data
            EquipmentMethods handler = null;
            handler = (args) =>
            {
                var last_suitObject = args.LastItemInSlot as SuitObject;
                if (last_suitObject != null)
                {
                    last_suitObject.ActualRating = ArmorRating.NoCalculatedActualValue;
                }

                suit_equipped = args.Slot.ItemContained as SuitObject;
                InitializeArmor();
            };


            if (!equipment_system.TryAddMethodToEquipType(ItemType.Suit, handler))
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
            bool is_damaged = modificationInfo.health_delta_value < 0;
            var delta_value = modificationInfo.health_delta_value;

            if (is_damaged && ArmorRating.ActualValue > 0)
            {
                delta_value = HealthSystemCalculator.GetDamageDealtReduction(delta_value, Armor.Value);
            }

            Health.AddToTemporary(delta_value, modificationInfo.temporal_delta_type);
            if (modificationInfo.status_applied != null)
            {
                foreach (var status_name in modificationInfo.status_applied)
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

        public void RecoverArmor(float amount) { }


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