using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaivourTree;

[CreateAssetMenu(fileName = "PatrolAction", menuName = "ScriptableTreeActions/PatrolAction")]
public class PatrolAction : ScriptableActionTree
{
    private List<Vector3> _positions;
    private Transform _transform;
    private float _speed;
    private float _waitTime;
    private bool _wait;
    private EnemyController _script;
    private Rigidbody _rb;
    public int CurrentWaypointIndex;

    public override void OnFinishedState()
    {
        _rb.velocity= Vector3.zero;
    }

    public override void OnUpdate()
    {
        if (!_wait)
        {

            if (Vector3.Distance(_transform.position, _positions[CurrentWaypointIndex]) < 0.01f)
            {
                _transform.position = _positions[CurrentWaypointIndex];
                _script.WaitTime(_waitTime);
                CurrentWaypointIndex += 1;
                if (CurrentWaypointIndex >= _positions.Count)
                    CurrentWaypointIndex = 0;
            }
            else
            {
                _transform.LookAt(_positions[CurrentWaypointIndex]);
                
                _rb.velocity = _speed * Time.deltaTime * (_positions[CurrentWaypointIndex] - _transform.position).normalized;
            }

        }
    }

    public override void OnSetState(StateTreeController sc)
    {
        base.OnSetState(sc);
       _positions = (List<Vector3>)sc.GetData("PatrolPositions");
        _transform = (Transform)sc.GetData("Transform");
        _speed = (float)sc.GetData("PatrolSpeed");
        _wait = (bool)sc.GetData("Wait");
        _waitTime = (float)sc.GetData("WaitPatrolTime");
        _script = (EnemyController)sc.GetData("Script");
        _rb = (Rigidbody)sc.GetData("Rigydbody");
    }
}
