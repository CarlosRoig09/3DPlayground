using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaivourTree;
using UnityEngine.UIElements;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "ChasingAction", menuName = "ScriptableTreeActions/ChasingAction")]
public class ChasingAction : ScriptableActionTree
{
    private Transform _transform;
    private Transform _playerTransform;
    private float _speed;
    private Rigidbody _rb;
    private LayerMask _toIgnore;
    private NavMeshAgent _nav;
    public override void OnFinishedState()
    {
        _nav.isStopped = true;
    }

    public override void OnUpdate()
    {
        sc.SetData("Attack", Physics.Raycast(_transform.position, _playerTransform.position - _transform.position, 5.5f, ~_toIgnore));
        Vector3 directonToLook = _playerTransform.position;
        directonToLook.y =0f;
        _transform.LookAt(directonToLook);
        _nav.destination = _playerTransform.position;
    }

    public override void OnSetState(StateTreeController sc)
    {
        base.OnSetState(sc);
        _transform = (Transform)sc.GetData("Transform");
        _nav = (NavMeshAgent)sc.GetData("Nav");
        _nav.speed = (float)sc.GetData("RunSpeed");
        _rb = (Rigidbody)sc.GetData("Rigydbody");
        _playerTransform = (Transform)sc.GetData("TargetTransformm");
        _toIgnore = (LayerMask)sc.GetData("ToIgnore");
        _nav.isStopped = false;

    }
}
