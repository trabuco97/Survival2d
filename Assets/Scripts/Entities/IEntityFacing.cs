using System;
using UnityEngine;

namespace Survival2D.Entities
{
    [Serializable]
    public class IEntityFacingWrapper : IUnifiedContainer<IEntityFacing> { }


    public interface IEntityFacing
    {
        bool IsFacing { get; }
        Vector2 FacingVector { get; }
    }
}