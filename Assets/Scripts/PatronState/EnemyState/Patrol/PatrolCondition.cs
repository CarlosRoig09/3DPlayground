using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaivourTree;
[CreateAssetMenu(fileName = "PatrolCondition", menuName = "ScriptableConditions/PatrolCondition")]
public class PatrolCondition : ScriptableCondition
{
    public override bool Check(StateTreeController sc)
    {
        return !(bool)sc.GetData("Detected");
    }
}
