using UnityEngine;
using System.Collections;

using Survival2D.Systems.Item;

namespace Survival2D.Systems.Tools
{
    public interface IToolItemObject
    {
        ItemType ToolType { get; }
    }
}