using BehaivourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChasingCondition", menuName = "ScriptableConditions/ChasingCondition")]
public class ChasingCondition : ScriptableCondition
{
    public override bool Check(StateTreeController sc)
    {
        return (bool)sc.GetData("Detected")&&!(bool)sc.GetData("Attack");
    }
}
