using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaivourTree
{
    public abstract class ScriptableActionTree : ScriptableObject
    {
        protected StateTreeController sc;
        public abstract void OnFinishedState();

        public virtual void OnSetState(StateTreeController sc)
        {
            this.sc = sc;
        }

        public abstract void OnUpdate();
    }
}
