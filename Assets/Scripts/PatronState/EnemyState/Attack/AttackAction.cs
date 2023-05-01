using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaivourTree;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "AttackAction", menuName = "ScriptableTreeActions/AttackAction")]
public class AttackAction : ScriptableActionTree
{
    private Animator _anim;
    private Transform _transform;
    private Transform _playerTransform;
    private LayerMask _toIgnore;
    private int _fireLayer;
    private EnemyController _script;
    public override void OnFinishedState()
    {
       // _anim.SetBool("Attack", false);
        Debug.Log("AttackFinished");
    }

    public override void OnUpdate()
    {
        sc.SetData("Attack", Physics.Raycast(_transform.position, _playerTransform.position - _transform.position, 4, ~_toIgnore));
        if (!(bool)sc.GetData("Wait"))
        {
            _transform.LookAt(_playerTransform.position);
        }
        else
        {
            _script.WaitTime((float)sc.GetData("AttackDuration"));
            _anim.SetLayerWeight(_fireLayer, 1);
        }
    }

    public override void OnSetState(StateTreeController sc)
    {
        base.OnSetState(sc);
        _transform = (Transform)sc.GetData("Transform");
         _script = (EnemyController)sc.GetData("Script");
        _playerTransform = (Transform)sc.GetData("TargetTransformm");
        _toIgnore = (LayerMask)sc.GetData("ToIgnore");
        _fireLayer = (int)sc.GetData("FireLayer");
        _anim = (Animator)sc.GetData("Animator");
    }

}
