using UnityEngine;
using NUnit.Framework;

using Survival2D.Systems.Statistics;

namespace Test.Statistics
{
    public class StatsTests : MonoBehaviour
    {

        // Calculate 100 * 1.5 + 10 = 160
        [Test]
        public void Statistics_StatCalculation()
        {
            var stat = new Stat(100f);
            var modifier1 = new StatModifier { value = 10, type = StatModifierType.Flat };
            var modifier2 = new StatModifier { value = 150, type = StatModifierType.PercentMult };

            stat.AddModifier(modifier1);
            stat.AddModifier(modifier2);

            Assert.AreEqual(160f, stat.Value);
        }

        // Calculate 100 * 1.5 + 10 = 160
        // Calculate 100 * 1.5 = 150
        [Test]
        public void Statistics_StatRemoval()
        {
            var stat = new Stat(100f);
            var modifier1 = new StatModifier { value = 10, type = StatModifierType.Flat };
            var modifier2 = new StatModifier { value = 150, type = StatModifierType.PercentMult };

            stat.AddModifier(modifier1);
            stat.AddModifier(modifier2);

            stat.RemoveModifier(modifier1);

            Assert.AreEqual(150f, stat.Value);
        }


        // Calculate (100 + 100) * 2.0 = 400
        [Test]
        public void Statistics_StatModifierOrderCalculation()
        {
            var stat = new IncrementalStat(100f);
            var modifier1 = new StatModifier { value = 100, type = StatModifierType.Flat, order = 10};
            var modifier2 = new StatModifier { value = 200, type = StatModifierType.PercentMult };

            stat.AddModifier(modifier1);
            stat.AddModifier(modifier2);

            Assert.AreEqual(400f, stat.Value);
        }

        // Check the values are changed properly
        [Test]
        public void Statistics_IncrementalStat_ValueAndActualValue()
        {
            var stat = new IncrementalStat(100f);
            stat.SetTemporalValue(50f);

            Assert.AreEqual(100f, stat.Value);
            Assert.AreEqual(50f, stat.ActualValue);
        }

        // 50 - 10 = 40
        [Test]
        public void Statistics_IncrementalStat_ActualValueModification()
        {
            var stat = new IncrementalStat(100f);
            stat.SetTemporalValue(50f);

            stat.AddToTemporary(-10, IncrementalStat.AdditiveTemporaryType.Flat);

            Assert.AreEqual(40f, stat.ActualValue);
            Assert.AreEqual(100f, stat.Value);
        }

        // 60 - 10 = 50
        // 50 * 2 = 100
        // 100 * 2 = 200
        [Test]
        public void Statistics_IncrementalStat_ActualValueModification_ValueModifier()
        {
            var stat = new IncrementalStat(100f);
            stat.SetTemporalValue(60f);

            stat.AddToTemporary(-10, IncrementalStat.AdditiveTemporaryType.Flat);

            var modifier1 = new StatModifier { value = 200, type = StatModifierType.PercentMult };
            stat.AddModifier(modifier1);

            Assert.AreEqual(100f, stat.ActualValue);
            Assert.AreEqual(200f, stat.Value);
        }

        // 50 + (200 * 0.5) = 150
        // 200 * 0.75 = 150
        // 150 - (150 * 0.5) = 75
        [Test]
        public void Statistics_IncrementalStat_ActualValueModification_SetAddTemporary()
        {
            var stat = new IncrementalStat(200f);
            stat.SetTemporalValue(50f);

            stat.AddToTemporary(50, IncrementalStat.AdditiveTemporaryType.PercentualBase);
            Assert.AreEqual(150, stat.ActualValue);

            stat.SetTemporalValue(75, IncrementalStat.TemporaryType.Percentual);
            Assert.AreEqual(150, stat.ActualValue);

            stat.AddToTemporary(-50, IncrementalStat.AdditiveTemporaryType.PercentualTemporary);
            Assert.AreEqual(75, stat.ActualValue);
        }

        // increment(i) : i + 10
        // 60 + increment(10) = 80
        // increment(i) : i * 2.0 + 10
        // 80 + increment(10) = 110

        [Test]
        public void Statistics_IncrementalStat_ActualValueModification_IncrementalModifier()
        {
            var stat = new IncrementalStat(1000f);
            stat.SetTemporalValue(60f);

            var modifier1 = new IncrementalStatModifier { incrementalType = IncrementalStatModifier.IncrementalType.Increase, value = 10, type = StatModifierType.Flat };
            var modifier2 = new IncrementalStatModifier { incrementalType = IncrementalStatModifier.IncrementalType.Increase, value = 200, type = StatModifierType.PercentMult };

            stat.AddIncrementalModifier(modifier1);
            stat.AddToTemporary(10, IncrementalStat.AdditiveTemporaryType.Flat);

            Assert.AreEqual(80f, stat.ActualValue);

            stat.AddIncrementalModifier(modifier2);
            stat.AddToTemporary(10, IncrementalStat.AdditiveTemporaryType.Flat);

            Assert.AreEqual(110f, stat.ActualValue);

        }

        // decrease(i) : i - 20
        // increase(i) : i + 10
        // 60 + increase(10) = 80
        // 80 - decrease(10) = 50
        [Test]
        public void Statistics_IncrementalStat_ActualValueModification_IncrementalModifier_IncreaseDecrease()
        {
            var stat = new IncrementalStat(1000f);
            stat.SetTemporalValue(60f);

            var modifier1 = new IncrementalStatModifier { incrementalType = IncrementalStatModifier.IncrementalType.Increase, value = 10, type = StatModifierType.Flat };
            var modifier2 = new IncrementalStatModifier { incrementalType = IncrementalStatModifier.IncrementalType.Decrease, value = 20, type = StatModifierType.Flat };

            stat.AddIncrementalModifier(modifier1);
            stat.AddToTemporary(10, IncrementalStat.AdditiveTemporaryType.Flat);

            Assert.AreEqual(80f, stat.ActualValue);

            stat.AddIncrementalModifier(modifier2);
            stat.AddToTemporary(-10, IncrementalStat.AdditiveTemporaryType.Flat);

            Assert.AreEqual(50f, stat.ActualValue);
        }


        // increase(i) : i + 10
        // 60 + increase(10) = 80
        // increase(i) : i
        // 80 + increase(10) = 90
        [Test]
        public void Statistics_IncrementalStat_ActualValueModification_IncrementalModifierRemoval()
        {
            var stat = new IncrementalStat(1000f);
            stat.SetTemporalValue(60f);

            var modifier1 = new IncrementalStatModifier { incrementalType = IncrementalStatModifier.IncrementalType.Increase, value = 10, type = StatModifierType.Flat };

            stat.AddIncrementalModifier(modifier1);
            stat.AddToTemporary(10, IncrementalStat.AdditiveTemporaryType.Flat);

            Assert.AreEqual(80f, stat.ActualValue);

            stat.RemoveIncrementalModifier(modifier1);
            stat.AddToTemporary(10, IncrementalStat.AdditiveTemporaryType.Flat);

            Assert.AreEqual(90f, stat.ActualValue);
        }
    }
}