using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaivourTree;

[CreateAssetMenu(fileName = "HitAction", menuName = "ScriptableTreeActions/HitAction")]
public class HitAction : ScriptableActionTree
{
    private Animator _anim;
    public override void OnFinishedState()
    {
        _anim.SetBool("Hit", false);
        Debug.Log("HitFinished");
    }

    public override void OnUpdate()
    {
        if (!(bool)sc.GetData("Wait"))
        {
            sc.SetData("Hit", false);
        }
    }

    public override void OnSetState(StateTreeController sc)
    {
        base.OnSetState(sc);
        _anim = (Animator)sc.GetData("Animator");
        sc.SetData("Wait", true);
        _anim.SetBool("Hit", true);
        var script = (EnemyController)sc.GetData("Script");
        script.WaitTime((float)sc.GetData("HitDuration"));
        Debug.Log("StartHit");
    }
}
