using BehaivourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DieAction", menuName = "ScriptableTreeActions/DieAction")]
public class DieAction : ScriptableActionTree
{
    private Animator _anim;
    public override void OnFinishedState()
    {
    }

    public override void OnUpdate()
    {
    }

    public override void OnSetState(StateTreeController sc)
    {
        base.OnSetState(sc);
        _anim = (Animator)sc.GetData("Animator");
        _anim.SetFloat("HP", (float)sc.GetData("HP"));
    }
}
