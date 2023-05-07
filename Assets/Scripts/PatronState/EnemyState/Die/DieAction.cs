using BehaivourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DieAction", menuName = "ScriptableTreeActions/DieAction")]
public class DieAction : ScriptableActionTree
{
    private IDamagable damagable;
    public override void OnFinishedState()
    {
    }

    public override void OnUpdate()
    {
    }

    public override void OnSetState(StateTreeController sc)
    {
        base.OnSetState(sc);
       damagable = (IDamagable)sc.GetData("Script");
        damagable.Death();
    }
}
