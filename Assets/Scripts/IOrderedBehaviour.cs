using System;

namespace Survival2D
{
    [Serializable]
    public class OrderedBehaviourWrapper : IUnifiedContainer<IOrderedBehaviour> { }

    // each componenent that depends or is depended on derives from this class
    public interface IOrderedBehaviour
    {
        int Order { get; }
        void Initialize();
    }
}