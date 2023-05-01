using BehaivourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : StateTreeController
{
    private List<Collider> _colliders = new();
    [SerializeField] 
    private float _maxHp;
    [SerializeField]
    private float _currentHp;
    [SerializeField]
    private bool _playerDetected;
    [SerializeField] 
    private List<Vector3> _patrolPositions;
    [SerializeField]
    private float _patrolSpeed;
    [SerializeField] 
    private float _runSpeed;
    [SerializeField]
    private float _waitPatrolTime;
    [SerializeField]
    private float _attackDuration;
    [SerializeField]
    private float _hitDuration;
    [SerializeField]
    private LayerMask _ToIgnore;
    [SerializeField]
    private float _smothAnim;
    private Rigidbody _rb;
    private Animator _anim;
    private int _upIdleLayer;
    private int _searchinLayer;
    private int _upAttentionLayer;
    private int _fireLayer;
    private float _attentionLayerTime;
    [SerializeField]
    private GameObject _spawnBullet;
    [SerializeField]
    private GameObject _bullet;
    [SerializeField]
    private float _proyectileSpeed;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _currentHp = _maxHp;
        statetoPlay = currentState;
        SetData("Wait", false);
        SetData("Detected", false);
        SetData("PatrolPositions", _patrolPositions);
        SetData("Transform", gameObject.transform);
        SetData("PatrolSpeed", _patrolSpeed);
        SetData("Script", this);
        SetData("WaitPatrolTime", _waitPatrolTime);
        SetData("Rigydbody", _rb);
        SetData("Attack", false);
        SetData("RunSpeed", _runSpeed);
        SetData("ToIgnore", _ToIgnore);
        SetData("AttackDuration", _attackDuration);
        SetData("Hit", false);
        SetData("HitDuration", _hitDuration);
        SetData("HP", _currentHp);
        SetData("Animator", _anim);
        SetData("FireLayer", _fireLayer);
    }

    private void Start()
    {
        if (currentState.action is PatrolAction patrolAction)
        {
            patrolAction.CurrentWaypointIndex = 0;
        }
        transform.position = _patrolPositions[0];
        _anim= GetComponent<Animator>();
        _upIdleLayer = 1;
        _searchinLayer = 2;
        _upAttentionLayer= 3;
        _fireLayer= 4;
        _attentionLayerTime = 0;
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if(_playerDetected)
        {
            if (!Physics.Raycast(transform.position, target.transform.position - transform.position, 10, ~_ToIgnore))
            {
                _playerDetected = false;
                SetData("Detected", _playerDetected);
            }
        }
        Animations();
    }

    public void StartAttack()
    {
        GameObject newbullet;
        newbullet = Instantiate(_bullet, _spawnBullet.transform.position + _spawnBullet.transform.forward * 0.5f, _spawnBullet.transform.rotation);
        newbullet.GetComponent<Rigidbody>().velocity = new Vector3(_spawnBullet.transform.forward.x, _spawnBullet.transform.forward.y, _spawnBullet.transform.forward.z) * _proyectileSpeed;
    }

    public void FinishAttack()
    {
        _anim.SetLayerWeight(_fireLayer, 0);
    }

    private void Animations()
    {
        _anim.SetFloat("MovementX", _rb.velocity.normalized.x);
        _anim.SetFloat("MovementZ", _rb.velocity.normalized.z);
        SetWeightLayer(_upAttentionLayer,_playerDetected,ref _attentionLayerTime);
    }

    void SetWeightLayer(int layer, bool condition, ref float currentAnimationTime)
    {
        if (condition)
        {
            if (currentAnimationTime < 1)
                currentAnimationTime += _smothAnim * Time.deltaTime;
            else
                currentAnimationTime = 1;
            _anim.SetLayerWeight(layer, currentAnimationTime);
        }
        else
        {
            if (currentAnimationTime > 0)
                currentAnimationTime -= _smothAnim * Time.deltaTime;
            else
                currentAnimationTime = 0;
            _anim.SetLayerWeight(layer, currentAnimationTime);
        }
    }

    public void WaitTime(float time)
    {
        StartCoroutine(Countdown(time));
    }
    private IEnumerator Countdown(float time)
    {
        SetData("Wait", true);
        yield return new WaitForSeconds(time);
        SetData("Wait", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        _playerDetected = true;
        SetData("Detected", _playerDetected);
        target = other.gameObject;
        SetData("TargetTransformm",target.transform);
    }

    private void OnDestroy()
    {
        SetData("Script", null);
    }
}
