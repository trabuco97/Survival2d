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
        public ZeroArmorRatingEvent onZeroArmorRating { get; } = new ZeroArmorRatingEvent();
        public ArmorRatingModificiationEvent onLossArmor { get; } = new ArmorRatingModificiationEvent();
        public HealthModificationEvent onHealthModified { get; } = new HealthModificationEvent();
        public ZeroHealthEvent onZeroHealth { get; } = new ZeroHealthEvent();
        public ArmorAdquiredEvent onArmorAdquired { get; } = new ArmorAdquiredEvent();


        // stats that the system manages
        public Stat Armor { get; private set; } = new Stat(0f);
        public IncrementalStat ArmorRating { get; private set; } = new IncrementalStat(0f);
        public IncrementalStat Health { get; private set; } = new IncrementalStat(0f);

        public HealthArmorSystem(StatusSystem status_system, EquipmentSystem equipment_system, float base_health)
        {
            this.status_system = status_system;
            Health = new IncrementalStat(base_health);

            // Link the armor equip and unequip, save the current data and inicialize new armor data
            if (equipment_system.onEquipableReplacedEvents.TryGetValue(ItemType.Suit, out var onEvent))
            {
                onEvent.AddListener(delegate (EquipmentSlot slot, ItemObject last_equipable)
                {
                    var suitObject = last_equipable as SuitObject;
                    if (suitObject != null)
                    {
                        suitObject.actual_rating = ArmorRating.NoCalculatedActualValue;
                    }

                    suit_equipped = slot.ItemContained as SuitObject;
                    InicializeArmor();
                });
            }
        }


        /// Mask:   0 = health
        ///         1 = armor
        ///         2 = armor rating
        public StatusLinkageToStat LinkModifierToStat(StatModifierData statModifier_data)
        {
            var modifier_linkage = new StatusLinkageToStat();
            modifier_linkage.modifier = statModifier_data.modifier;
            modifier_linkage.stats_linked = new List<Stat>();
            modifier_linkage.onModifierRemoval = new UnityEngine.Events.UnityEvent();

            if (statModifier_data.stat_layerMask.HasFlag(StatModified.Stat0))
            {
                Health.AddModifier(statModifier_data.modifier);

                modifier_linkage.stats_linked.Add(Health);
                CheckHealthIfZero();
            }

            modifier_linkage.onModifierRemoval.AddListener(delegate
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
            modifier_linkage.onModifierRemoval = new UnityEngine.Events.UnityEvent();

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
                            suit_equipped.actual_rating = 0;
                            onZeroArmorRating.Invoke(suit_equipped);
                        }

                        onLossArmor.Invoke(ArmorRating.ActualValue, ArmorRating.Value);

                    }
                }
            }
        }

        public void RecoverArmor(float amount) { }




        // Pre:     suit_equipped != null
        // Post :   ArmorRating.Value != 0
        //          Armor.Value != 0
        private void InicializeArmor()
        {
            float base_armor_value = 0;
            // Search for sources of base armor

            if (suit_equipped != null)
            {
                base_armor_value += suit_equipped.SuitData.base_damage_reduction;

                ArmorRating.ChangeBaseValue(suit_equipped.SuitData.base_armor_rating);
                ArmorRating.SetTemporalValue(suit_equipped.actual_rating);
            }
            else
            {
                ArmorRating.ChangeBaseValue(0f);
            }

            Armor.ChangeBaseValue(base_armor_value);

            // add the value to the stat
            onArmorAdquired.Invoke(new ArmorAdquiredEventInfo
            {
                armor_value = Armor.Value,
                armor_rating_temp_value = ArmorRating.ActualValue,
                armot_rating_total_value = ArmorRating.Value,
            });
        }

        // Post:    Returns true if the item is 
        private bool CheckHealthIfZero()
        {
            if (Health.ActualValue == 0)
            {
                onZeroHealth.Invoke(Health.Value);
                return true;
            }
            else
            {
                onHealthModified.Invoke(Health.ActualValue, Health.Value);
                return false;
            }
        }


    }
}