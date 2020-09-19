using System;

namespace Survival2D.Entities.AI
{

    public delegate void AIMethods(AIEventArgs args);


    public class AIEventArgs : EventArgs
    {
        public ActionType ActionType { get; private set; }

        public float ActionValue { get; private set; }

        public AIEventArgs(ActionType type, float value)
        {
            ActionType = type;
            ActionValue = value;
        }
    }
}