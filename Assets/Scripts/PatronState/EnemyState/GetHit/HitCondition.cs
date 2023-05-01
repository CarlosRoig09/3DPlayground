using BehaivourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HitCondition", menuName = "ScriptableConditions/HitCondition")]
public class HitCondition : ScriptableCondition
{
    public override bool Check(StateTreeController sc)
    {
        return (bool)sc.GetData("Hit");
    }
}
