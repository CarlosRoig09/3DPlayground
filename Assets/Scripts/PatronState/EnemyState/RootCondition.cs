using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaivourTree;
[CreateAssetMenu(fileName = "RootCondition", menuName = "ScriptableConditions/RootCondition")]
public class RootCondition : ScriptableCondition
{
    public override bool Check(StateTreeController sc)
    {
        return !sc.currentState.Condition(sc);
    }
}
