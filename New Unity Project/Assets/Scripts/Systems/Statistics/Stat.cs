using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Survival2D.Systems.Statistics
{

    public class Stat
    {
        protected readonly List<StatModifier> modifier_container = null;

        protected float last_calculated_value;
        protected bool is_dirty = false;
        protected float base_value;


        public float Value 
        {
            get 
            {
                if (is_dirty)
                {
                    last_calculated_value = CalculateFinalValue(base_value, modifier_container);
                    is_dirty = false;
                }

                return last_calculated_value;
            } 
        }

        public Stat(float base_value)
        {
            this.base_value = base_value;
            last_calculated_value = base_value;

            modifier_container = new List<StatModifier>();
        }

        public virtual void ChangeBaseValue(float new_base_value)
        {
            base_value = new_base_value;
            is_dirty = true;
        }


        public virtual void AddModifier(StatModifier modifier)
        {
            modifier.Init();
            modifier_container.Add(modifier);
            ReorderMofifierList(modifier_container);
            is_dirty = true;
        }

        public virtual bool RemoveModifier(StatModifier modifier)
        {
            is_dirty = true;

            return modifier_container.Remove(modifier);
        }


        protected static float CalculateFinalValue(float base_value, List<StatModifier> modifier_container)
        {
            float final_value = base_value;

            foreach (var modifier in modifier_container)
            {
                switch (modifier.type)
                {
                    case StatModifierType.Flat:
                        final_value += modifier.value;
                        break;
                    case StatModifierType.PercentAdd:
                        final_value += (final_value * modifier.value/100);
                        break;
                    case StatModifierType.PercentMult:
                        final_value *= modifier.value;
                        break;
                }
            }

            return final_value;
        }

        // Reorder the list to set modifier in this order : Flat, PercentAdd, PercentMult
        protected static void ReorderMofifierList(List<StatModifier> modifier_container)
        {
            modifier_container.Sort((StatModifier a, StatModifier b) =>
            {
                if (a.order < b.order)
                    return -1;
                else if (a.order > b.order)
                    return 1;
                else
                    return 0;
            });
        }
    }
}