using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaivourTree;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "ChasingAction", menuName = "ScriptableTreeActions/ChasingAction")]
public class ChasingAction : ScriptableActionTree
{
    private Transform _transform;
    private Transform _playerTransform;
    private float _speed;
    private Rigidbody _rb;
    private LayerMask _toIgnore;
    public override void OnFinishedState()
    {
        _rb.velocity= Vector3.zero;
    }

    public override void OnUpdate()
    {
        sc.SetData("Attack", Physics.Raycast(_transform.position, _playerTransform.position - _transform.position, 7, ~_toIgnore));
        Vector3 directonToLook = _playerTransform.position;
        directonToLook.y =0f;
        _transform.LookAt(directonToLook);
        _rb.velocity = _speed * Time.deltaTime * (_playerTransform.position - _transform.position).normalized;
    }

    public override void OnSetState(StateTreeController sc)
    {
        base.OnSetState(sc);
        _transform = (Transform)sc.GetData("Transform");
        _speed = (float)sc.GetData("RunSpeed");
        _rb = (Rigidbody)sc.GetData("Rigydbody");
        _playerTransform = (Transform)sc.GetData("TargetTransformm");
        _toIgnore = (LayerMask)sc.GetData("ToIgnore");

    }
}
