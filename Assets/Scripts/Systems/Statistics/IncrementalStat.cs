using UnityEngine.Events;
using System.Collections.Generic;

namespace Survival2D.Systems.Statistics
{
    public class IncrementalStat : Stat
    {
        /// <summary>
        /// Indicate if the value is added by flat value or percentual to the base value
        /// 
        /// Percentual : value% [0, -)
        /// </summary>
        public enum TemporalType { Flat, Percentual, MAX_TYPES }

        protected readonly List<StatModifier> incremental_modifiers_container = null;

        protected float temporal_value;
        protected float last_temporal_calculated_value;
        protected bool is_temporal_dirty = true;

        public float NoCalculatedActualValue { get { return temporal_value; } }

        public float ActualValue
        {
            get
            {
                if (is_temporal_dirty)
                {
                    last_temporal_calculated_value = CalculateFinalValue(temporal_value, modifier_container);

                    is_temporal_dirty = false;
                }

                return last_temporal_calculated_value;
            }
        }

        public IncrementalStat(float base_value) : base(base_value)
        {
            temporal_value = base_value;

            incremental_modifiers_container = new List<StatModifier>();
        }

        public override void ChangeBaseValue(float new_base_value)
        {
            base.ChangeBaseValue(new_base_value);
            temporal_value = new_base_value;
            is_temporal_dirty = true;
        }

        public override void AddModifier(StatModifier modifier)
        {
            base.AddModifier(modifier);
            is_temporal_dirty = true;
        }

        public override bool RemoveModifier(StatModifier modifier)
        {
            // workaround, maybe improve the remove modifer system

            bool is_modifier_removed = false;
            if (!base.RemoveModifier(modifier))
                is_modifier_removed = incremental_modifiers_container.Remove(modifier);

            is_temporal_dirty = true;
            return is_modifier_removed;
        }

        public void AddIncrementalModifier(StatModifier modifier)
        {
            modifier.Init();
            incremental_modifiers_container.Add(modifier);
            ReorderMofifierList(incremental_modifiers_container);
        }

        public virtual void SetTemporalValue(float base_temporal, TemporalType type)
        {
            float final_base_temporal = base_temporal;
            if (type == TemporalType.Percentual)
            {
                final_base_temporal = final_base_temporal / 100 * base_value;
            }


            if (IsInStatBoundaries(base_temporal))
            {
                final_base_temporal = CalculateFinalValue(final_base_temporal, incremental_modifiers_container);
            }


            if (final_base_temporal > base_value)
            {
                temporal_value = base_value;
            }
            else if (final_base_temporal < 0)
            {
                temporal_value = 0;
            }
            else
            {
                temporal_value = final_base_temporal;
            }

            is_temporal_dirty = true;

        }

        public virtual void AddToTemporalValue(float base_add_temporal, TemporalType type)
        {
            float temporal_added = temporal_value;
            if (type == TemporalType.Percentual)
            {
                temporal_added = temporal_added / base_value * 100;
            }

            SetTemporalValue(temporal_added + base_add_temporal, type);
        }

        private bool IsInStatBoundaries(float value)
        {
            return value <= base_value && value >= 0;
        }
    }
}