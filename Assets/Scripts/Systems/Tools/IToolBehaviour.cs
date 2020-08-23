using UnityEngine;
using System.Collections;

using Survival2D.Systems.Statistics.Status;


namespace Survival2D.Systems.Tools
{
    public interface IToolBehaviour : ISystemWithStatus
    {

        bool IsInicialized { get; }

        void OnToolDeactivated();
        void OnToolActivated(IToolItemObject tool_object);
        void ExecuteAction(int action_index);
        void InicializeBehaviour();
    }
}