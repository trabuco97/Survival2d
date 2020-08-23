using UnityEngine;
using System.Collections;

using Survival2D.Systems.HealthArmor;

namespace Survival2D.UI.HealthArmor
{
    public interface UI_IHealthArmorStat
    {
        void InitializeDisplay(HealthArmorSystem health_system);
    }
}