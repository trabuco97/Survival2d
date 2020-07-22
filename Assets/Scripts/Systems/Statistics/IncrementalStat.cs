using System;
using System.Collections.Generic;

namespace Survival2D.Systems.Statistics
{
    /// <summary>
    /// For design purposes, the incremental modifiers are only applied to flat increment/decrements
    /// All parameters uses percentages as base value 100
    /// </summary>

    public class IncrementalStat : Stat
    {
        /// <summary>
        /// Indicate if the value is added by flat value or percentual to the base value
        /// 
        /// Percentual : value% [0, -)
        /// </summary>
        public enum AdditiveTemporaryType { Flat, PercentualBase, PercentualTemporary, MAX_TYPES }
        public enum TemporaryType { Flat, Percentual }

        protected readonly List<IncrementalStatModifier> incremental_modifiers_container = null;

        protected float temporary_value;
        protected float last_temporary_calculated_value;
        protected bool is_temporary_dirty = true;

        public float NoCalculatedActualValue { get { return temporary_value; } }

        public float ActualValue
        {
            get
            {
                if (is_temporary_dirty)
                {
                    last_temporary_calculated_value = CalculateFinalValue(temporary_value, modifier_container);

                    is_temporary_dirty = false;
                }

                return last_temporary_calculated_value;
            }
        }

        public IncrementalStat(float base_value) : base(base_value)
        {
            temporary_value = base_value;
            last_temporary_calculated_value = temporary_value;

            incremental_modifiers_container = new List<IncrementalStatModifier>();
        }

        public override void ChangeBaseValue(float new_base_value)
        {
            base.ChangeBaseValue(new_base_value);
            temporary_value = new_base_value;
            is_temporary_dirty = true;
        }

        public override void AddModifier(StatModifier modifier)
        {
            base.AddModifier(modifier);
            is_temporary_dirty = true;
        }

        public override bool RemoveModifier(StatModifier modifier)
        {
            // workaround, maybe improve the remove modifer system

            //    bool is_modifier_removed = false;
            //    if (!base.RemoveModifier(modifier))
            //        is_modifier_removed = incremental_modifiers_container.Remove(modifier);

            //    is_temporal_dirty = true;
            //    return is_modifier_removed;

            // this is better because now the logic differenciates between base value modifiers and temporary value modifiers
            is_temporary_dirty = true;
            return base.RemoveModifier(modifier);
        }

        public void AddIncrementalModifier(IncrementalStatModifier modifier)
        {
            modifier.Init();
            incremental_modifiers_container.Add(modifier);
            ReorderMofifierList(incremental_modifiers_container);
        }

        public bool RemoveIncrementalModifier(IncrementalStatModifier modifier)
        {
            return incremental_modifiers_container.Remove(modifier);
        }

        // Only positive values
        public virtual void SetTemporalValue(float base_temporal, TemporaryType type = TemporaryType.Flat)
        {
            if (type == TemporaryType.Percentual)
            {
                base_temporal = base_temporal / 100 * base_value;
            }


            if (base_temporal > base_value)
            {
                temporary_value = base_value;
            }
            else if (base_temporal < 0)
            {
                temporary_value = 0;
            }
            else
            {
                temporary_value = base_temporal;
            }

            is_temporary_dirty = true;
        }

        // This accept negative values
        public virtual void AddToTemporary(float base_add_temporary, AdditiveTemporaryType type)
        {
            float final_add_temporary = base_add_temporary;

            // Get the base value to modify
            if (type != AdditiveTemporaryType.Flat)
            {
                if (type == AdditiveTemporaryType.PercentualBase)
                {
                    if (final_add_temporary > 100) final_add_temporary = 100;
                    else if (final_add_temporary < -100) final_add_temporary = -100;

                    final_add_temporary = final_add_temporary / 100 * base_value;
                }
                else if (type == AdditiveTemporaryType.PercentualTemporary)
                {
                    final_add_temporary = final_add_temporary / 100 * temporary_value;
                }
            }
            else
            {
                IncrementalStatModifier.IncrementalType incremental_type;
                if (final_add_temporary >= 0)
                {
                    incremental_type = IncrementalStatModifier.IncrementalType.Increase;
                }
                else
                {
                    incremental_type = IncrementalStatModifier.IncrementalType.Decrease;
                }

                final_add_temporary = CalculateFinalIncrementalValue(base_add_temporary, incremental_type, incremental_modifiers_container);
            }

            SetTemporalValue(temporary_value + final_add_temporary);
        }

        protected float CalculateFinalIncrementalValue(float delta_value, IncrementalStatModifier.IncrementalType type, List<IncrementalStatModifier> modifiers)
        {
            float final_delta_value = delta_value;

            foreach (var modifier in modifiers)
            {
                if (modifier.incrementalType != type) continue;

                switch (modifier.type)
                {
                    case StatModifierType.Flat:
                        if (type == IncrementalStatModifier.IncrementalType.Increase)
                        {
                            final_delta_value += modifier.value;
                        }
                        else 
                        {
                            final_delta_value -= modifier.value;
                        }
                        break;
                    case StatModifierType.PercentAdd:
                        if (type == IncrementalStatModifier.IncrementalType.Increase)
                        {
                            final_delta_value += (final_delta_value * modifier.value / 100);
                        }
                        else
                        {
                            final_delta_value -= (final_delta_value * modifier.value / 100);
                        }
                        break;
                    case StatModifierType.PercentMult:
                        if (type == IncrementalStatModifier.IncrementalType.Increase)
                        {
                            final_delta_value *= modifier.value / 100;

                        }
                        else
                        {
                            final_delta_value *= -modifier.value / 100;
                        }
                        break;
                }
            }

            return final_delta_value;
        }
    }
}