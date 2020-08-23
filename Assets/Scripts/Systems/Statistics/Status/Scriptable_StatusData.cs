using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survival2D.Systems.Statistics.Status
{
    [CreateAssetMenu(fileName = "New StatusData", menuName = "Custom/Statistics/Status")]
    public class Scriptable_StatusData : ScriptableObject
    {
        public string status_name;
        public List<StatModifierData> modifiers_data;
        public List<IncrementalStatModifierData> incremental_modifiers_data;
        public bool has_duration;
        public float status_duration;

        public Sprite ui_icon;
    }
}