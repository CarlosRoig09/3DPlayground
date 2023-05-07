using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaivourTree;
using UnityEngine.AI;

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
    private NavMeshAgent _nav;
    public override void OnFinishedState()
    {
        _nav.isStopped= true;
    }

    public override void OnUpdate()
    {
        if (!(bool)sc.GetData("Wait"))
        {

            float dist =_nav.remainingDistance; 
            if (dist != Mathf.Infinity && _nav.pathStatus == NavMeshPathStatus.PathComplete && _nav.remainingDistance == 0)
            {
                _transform.position = _positions[CurrentWaypointIndex];
                _script.WaitTime(_waitTime);
                CurrentWaypointIndex += 1;
                if (CurrentWaypointIndex >= _positions.Count)
                    CurrentWaypointIndex = 0;
                _transform.LookAt(_positions[CurrentWaypointIndex]);
                _nav.destination = _positions[CurrentWaypointIndex];
            }
            _nav.isStopped = false;
        }
        else
        {
            _nav.isStopped = true;
        }
    }

    public override void OnSetState(StateTreeController sc)
    {
        base.OnSetState(sc);
        CurrentWaypointIndex = 0;
       _positions = (List<Vector3>)sc.GetData("PatrolPositions");
        _transform = (Transform)sc.GetData("Transform");
        _nav = (NavMeshAgent)sc.GetData("Nav");
       _nav.speed = (float)sc.GetData("PatrolSpeed");
        _waitTime = (float)sc.GetData("WaitPatrolTime");
        _script = (EnemyController)sc.GetData("Script");
        _rb = (Rigidbody)sc.GetData("Rigydbody");
    }
}
