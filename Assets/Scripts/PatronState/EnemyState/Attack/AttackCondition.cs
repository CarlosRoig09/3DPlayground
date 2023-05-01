using BehaivourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AttackCondition", menuName = "ScriptableConditions/AttackCondition")]
public class AttackCondition : ScriptableCondition
{
    public override bool Check(StateTreeController sc)
    {
        return (bool)sc.GetData("Attack");
    }
}
