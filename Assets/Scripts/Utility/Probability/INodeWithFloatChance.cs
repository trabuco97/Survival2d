using UnityEngine;
using System.Collections;

namespace Survival2D
{
    public interface INodeWithFloatChance
    {
        float Chance { get; }
    }

    public interface INodeWithWeight
    {
        int Weight { get; }
    }
}