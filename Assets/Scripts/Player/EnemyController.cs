using BehaivourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : StateTreeController, IDamagable, ICanBeImpulse
{
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
    private float _attackWaitTime;
    [SerializeField]
    private float _hitDuration;
    [SerializeField]
    private LayerMask _ToIgnore;
    [SerializeField]
    private float _smothAnim;
    private Rigidbody _rb;
    private Animator _anim;
    private NavMeshAgent _enemyAgent;
    private int _upAttentionLayer;
    private int _fireLayer;
    private float _attentionLayerTime;
    [SerializeField]
    private GameObject _spawnBullet;
    [SerializeField]
    private GameObject _bullet;
    [SerializeField]
    private float _proyectileSpeed;
    [SerializeField]
    private GameObject _itemHolder;
    private Collider _itemCollider;
    private bool _noMove;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _currentHp = _maxHp;
        statetoPlay = currentState;
        _anim = GetComponent<Animator>();
        _enemyAgent= GetComponent<NavMeshAgent>();
        _upAttentionLayer = 3;
        _fireLayer = 4;
        SetData("Nav", _enemyAgent);
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
        SetData("AttackDuration", _attackWaitTime);
        SetData("Hit", false);
        SetData("HitDuration", _hitDuration);
        SetData("HP", _currentHp);
        SetData("Animator", _anim);
        SetData("FireLayer", _fireLayer);
        SetData("AttackWaitTime", _attackWaitTime);
        _noMove = false;
    }

    private void Start()
    {
        if (_itemHolder.transform.childCount>0)
        {
            _itemCollider = _itemHolder.transform.GetChild(0).GetComponent<Collider>();
            _itemCollider.enabled = false;
        }
        if (currentState.action is PatrolAction patrolAction)
        {
            patrolAction.CurrentWaypointIndex = 0;
        }
       transform.position = _patrolPositions[0];
        _attentionLayerTime = 0;
        transform.Find("PlayerDetector").GetComponent<PlayerDetector>().playerDetected += DetectPlayer;
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if(_playerDetected)
        {
            if (!Physics.Raycast(transform.position, target.transform.position - transform.position, 15, ~_ToIgnore))
            {
                _playerDetected = false;
                SetData("Detected", _playerDetected);
            }
        }
        if (!_noMove)
        {
            Animations();
        }
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
        _anim.SetFloat("MovementX", _enemyAgent.velocity.normalized.x);
        _anim.SetFloat("MovementZ", _enemyAgent.velocity.normalized.z);
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

    public void ModifyLife(float damage)
    {
        _currentHp += damage;
        _anim.SetFloat("Life", _currentHp);
        SetData("Hit", true);
        SetData("HP", _currentHp);
    }

    private void DetectPlayer(GameObject player)
    {
        _playerDetected = true;
        SetData("Detected", _playerDetected);
        target = player;
         SetData("TargetTransformm", target.transform);
    }

    private void OnDestroy()
    {
        SetData("Script", null);
    }

    public void Death()
    {
        SetData("Hit", false);
        _anim.SetFloat("Life", _currentHp);
        if (_itemHolder.transform.childCount>0)
        {
            var item = _itemHolder.transform.GetChild(0).gameObject;
            ParentAndChildrenMethods.UnParentAnSpecificChildren(_itemHolder, item);
            item.transform.position = transform.position;
            _itemCollider.enabled = true;
        }
        _noMove = true;
    }

    public void GetImpulse(Vector3 impulse)
    {
        _rb.AddForce(impulse*0.5f);
    }

    public void StopMomentum()
    {
        _rb.velocity = Vector3.zero;
    }

    public Vector3 TrasspassImpulse(Vector3 impulse)
    {
        return new Vector3(impulse.x * _rb.velocity.x, impulse.y * _rb.velocity.y, impulse.z * _rb.velocity.z);
    }

    public void OnHit()
    {
        throw new System.NotImplementedException();
    }
}
