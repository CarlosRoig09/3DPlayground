using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaivourTree
{
    public abstract class ScriptableCondition : ScriptableObject
    {
        public abstract bool Check(StateTreeController sc);
    }
}
