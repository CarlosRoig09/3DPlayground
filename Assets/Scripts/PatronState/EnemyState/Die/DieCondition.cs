using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaivourTree;
[CreateAssetMenu(fileName = "DieCondition", menuName = "ScriptableConditions/DieCondition")]
public class DieCondition : ScriptableCondition
{
    public override bool Check(StateTreeController sc)
    {
        return (float)sc.GetData("HP") <= 0;
    }
}
