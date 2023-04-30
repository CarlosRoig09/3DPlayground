using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using EnumLibrary;
public enum Events
{
    Movement,
    Jump,
    Sprint,
    Crouch,
    AttackButton1,
    AttackButton2,
    GrabItem,
    Celebration,
    ChangeWeapon,
    None
}

public class PlayerMovement : MonoBehaviour, IDamagable
{
    [SerializeField]
    private GameObject weapon;
    private CharacterController _controller;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _rotationSpeed;
    [SerializeField]
    private float _sprintSpeed;
    [SerializeField]
    private float _rotationSmoothTime;
    private CheckIfGround _checkIfGround;
    private bool _noMove;
    public bool GrabingCurrentItem
    {
        get { return _noMove; }
    }
    private float _realSpeed;
    [SerializeField]
    private float _gravity;
    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _sprintAction;
    private InputAction _crouchAction;
    private InputAction _apuntAction;
    private InputAction _shootAction;
    private InputAction _grabItem;
    private InputAction _changeWeaponAction;
    private Animator _anim;
    private bool _crouching;
    private float _animCrouchTime;
    private bool _apunting;
    private float _animApuntTime;
    public bool Fire;
    private bool _injured;
    private float _animInjurUpTime;
    private float _animInjurDownTime;
    [SerializeField]
    private float _smothAnim;
    [SerializeField]
    private float _jumpForce;
    public float Life;
    private Vector3 _jump;
    private bool _jumping;
    private int _baseLayerDown;
    private int _crouchLayer;
    private int _weaponApuntLayer;
    private int _weaponFireLayer;
    public int WeaponApuntLayer
    {
        get { return _weaponApuntLayer; }
        set { _weaponApuntLayer = value;}
    }
    public int WeaponFireLayer
    {
        get { return _weaponFireLayer; }
        set {_weaponFireLayer = value;}
    }
    private int _injuredDownLayer;
    private int _injuredUpLayer;
    [SerializeField]
    private GameObject _playerCamera;
    [SerializeField]
    private GameObject _celebrationCamera;
    private float _idleTime;
    [SerializeField]
    private float _idleMaxTime;
    private bool _animationJump;
    private ControlInventory _inventory;
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions["Movement"];
        _jumpAction = _playerInput.actions["Jump"];
        _sprintAction = _playerInput.actions["Sprint"];
        _crouchAction = _playerInput.actions["Crouch"];
        _shootAction = _playerInput.actions["WeaponFirstButton"];
        _apuntAction = _playerInput.actions["WeaponSecondButton"];
        _grabItem = _playerInput.actions["GrabItem"];
        _changeWeaponAction = _playerInput.actions["ChangeWeapon"];
    }

    private void OnEnable()
    {
        SubrcibeEvents(new Events[] {Events.Jump,Events.Celebration,Events.Crouch,Events.Sprint, Events.ChangeWeapon });
        DesubrcribeEvents(new Events[] { Events.GrabItem });
    }
    private void OnDisable()
    {
        DesubrcribeEvents(new Events[] { Events.Jump, Events.Celebration, Events.Crouch, Events.Sprint, Events.AttackButton1, Events.AttackButton2, Events.ChangeWeapon });
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _controller = gameObject.GetComponent<CharacterController>();
        _realSpeed = _speed;
        _anim = GetComponent<Animator>();
        _crouching = false;
        _apunting = false;
        Fire = false;
        _animCrouchTime = 0;
        _baseLayerDown = 0;
        _crouchLayer = 2;
        _weaponApuntLayer= 4;
        _weaponFireLayer= 5;
        _injuredDownLayer= 6;
        _injuredUpLayer= 7;
        _animCrouchTime= 0;
        _animInjurDownTime = 0;
        _animInjurUpTime= 0;
        _noMove = false;
        _injured = false;
        Life = 100;
        _checkIfGround = transform.GetChild(0).GetComponent<CheckIfGround>();
        _jumping = false;
        _animationJump = false;
        _inventory = transform.Find("Inventory").GetComponent<ControlInventory>();
        weapon = GameObject.Find("Weapon");
        _playerCamera = GameObject.Find("PlayerCamera");
        _celebrationCamera = GameObject.Find("CelebrationCamera");
        _celebrationCamera.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        GroundedCheck();
        if (!_noMove)
        {
            JumpAction();
            TotalMovement(MovementAction());
        }
        Animations();
    }
    void GroundedCheck()
    {
        if (_checkIfGround.IsGround||_controller.isGrounded)
        {
            if(!_jumping)
            _jump = Vector3.zero;
            _animationJump = false;
        }
        else
        {
            _jumping = false;
            _jump -= new Vector3(0, _gravity * Time.deltaTime * 2, 0);
        }
    }

    void SprintAction(InputAction.CallbackContext context)
    {
        _realSpeed = _speed * _sprintSpeed;
    }

    void OnSprintCanceled(InputAction.CallbackContext context)
    {
        _realSpeed = _speed;
    }

    void Animations()
    {
        IdleAnimation();
        _anim.SetFloat("MovementX", Mathf.Abs(_moveAction.ReadValue<Vector2>()[0]) * _realSpeed);
        _anim.SetFloat("MovementZ", Mathf.Abs(_moveAction.ReadValue<Vector2>()[1]) * _realSpeed);
        _anim.SetBool("Jump", _animationJump);
        if(Life<=50)
            _injured= true;
        else
            _injured= false;
        if (_crouching)
            _injured = false;
        
        SetWeightLayer(_injuredUpLayer, _injured, ref _animInjurUpTime);
        SetWeightLayer(_injuredDownLayer, _injured, ref _animInjurDownTime);
        SetWeightLayer(_crouchLayer, _crouching, ref _animCrouchTime);
        SetWeightLayer(_weaponApuntLayer, _apunting, ref _animApuntTime);
        if(Fire)
        _anim.SetLayerWeight(_weaponFireLayer, 1);
        else
            _anim.SetLayerWeight(_weaponFireLayer, 0);
        _anim.SetFloat("Life", Life);
    }

    void IdleAnimation()
    {
        if (_idleTime <= _idleMaxTime&&_moveAction.ReadValue<Vector2>().magnitude<=0&&!_noMove&&!_jumping)
        {
          _anim.SetFloat("IdleTime", _idleTime);
          _idleTime += Time.deltaTime;
        }
        else
            _idleTime = 0;
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

   public void SubrcibeEvents(Events[] events)
    {
        
        foreach (var evento in events)
        {
            switch (evento)
            {
                case Events.Movement:
                    break;
                case Events.Jump:
                    _jumpAction.started += CallJump;
                    break;
                case Events.Sprint:
                    _sprintAction.started += SprintAction;
                    _sprintAction.canceled += OnSprintCanceled;
                    break;
                case Events.Crouch:
                    _crouchAction.started += CrouchAction;
                    _crouchAction.canceled += CrouchCancel;
                    break;
                case Events.AttackButton1:
                    _shootAction.performed += FireAction;
                    break;
                case Events.AttackButton2:
                    _apuntAction.started+= ApuntAction;
                    _apuntAction.canceled += ApuntActionCanceled;
                    break;
                case Events.ChangeWeapon:
                    _changeWeaponAction.started += ChangeWeapon;
                    break;
                case Events.GrabItem:
                    _grabItem.started+= GrabItem;
                    break;
                default:
                    break;
            }
        }
      
    }
   public void DesubrcribeEvents(Events[] events)
    {
        foreach (var evento in events)
        {
            switch (evento)
            {
                case Events.Movement:
                    break;
                case Events.Jump:
                    break;
                case Events.Sprint:
                    _sprintAction.started -= SprintAction;
                    _sprintAction.canceled -= OnSprintCanceled;
                    break;
                case Events.Crouch:
                    _crouchAction.started -= CrouchAction;
                    _crouchAction.canceled -= CrouchCancel;
                    break;
                case Events.AttackButton1:
                    _shootAction.performed -= FireAction;
                    break;
                case Events.AttackButton2:
                    _apuntAction.started -= ApuntAction;
                    _apuntAction.canceled -= ApuntActionCanceled;
                    break;
                case Events.ChangeWeapon:
                    _changeWeaponAction.started -= ChangeWeapon;
                    break;
                case Events.GrabItem:
                    _grabItem.started -= GrabItem;
                    break;
                default:
                    break;
            }
        }
    }

    public void ChangeWeapon(InputAction.CallbackContext context)
    {
        _inventory.ChangeWeapon();
    }

    IEnumerator AnimationEndByTime(int layer, Events[] events, string animation)
    {
        DesubrcribeEvents(events); 
        yield return new WaitForSeconds(_anim.GetCurrentAnimatorClipInfo(layer)[0].clip.averageDuration);
        _anim.SetBool(animation, false);
        SubrcibeEvents(events);
        _noMove = false;
    }
    IEnumerator AnimationTransition(float duration,int layer, Events[] events, string animation)
    {
        _anim.SetFloat("MovementX", 0);
        _anim.SetFloat("MovementZ", 0);
        _noMove = true;
        _anim.SetBool(animation, true);
        yield return new WaitForSeconds(duration);
        StartCoroutine(AnimationEndByTime(layer,events,animation));
    }

    Vector3 MovementAction()
    {
        transform.GetChild(1).rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
        if (_checkIfGround.IsGround||_controller.isGrounded) {
        Vector2 horizontal = _moveAction.ReadValue<Vector2>();
        if (horizontal.magnitude > 0)
        {
            Vector3 direction = new Vector3(horizontal.x, 0f, horizontal.y).normalized;
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _rotationSpeed, _rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0, rotation, 0);
            return Quaternion.Euler(0, rotation, 0) * Vector3.forward;
           }
        }
        return Vector3.zero;
    }
    void JumpAction()
    {
        if (_jumping)
        {
            _jump = new Vector3(0, Mathf.Sqrt(2*_gravity * _jumpForce), 0);
            _animationJump = true;
        }
    }

    void CallJump(InputAction.CallbackContext context)
    {
        if (_checkIfGround.IsGround || _controller.isGrounded)
        { 
        _jumping = true;
        }
    }


    void TotalMovement(Vector3 groundMovement)
    {
        _controller.Move(_realSpeed * Time.deltaTime * groundMovement.normalized + _jump*Time.deltaTime);
    }

    void CrouchAction(InputAction.CallbackContext context)
    {
        _crouching = true;
        FireActionCanceled();
        ApuntActionCanceled(context);
        DesubrcribeEvents(new Events[] { Events.AttackButton2});
    }

    void CrouchCancel(InputAction.CallbackContext context)
    {
        _crouching = false;
        SubrcibeEvents(new Events[] { Events.AttackButton2 });
    }
    void ApuntAction(InputAction.CallbackContext context)
    {
        _playerCamera.GetComponent<CinemachineFreeLook>().m_Lens.FieldOfView = 20;
        _apunting = true;
        SubrcibeEvents(new Events[] { Events.AttackButton1 });
        DesubrcribeEvents(new Events[] { Events.ChangeWeapon });
    }
    void ApuntActionCanceled(InputAction.CallbackContext context)
    {
        _playerCamera.GetComponent<CinemachineFreeLook>().m_Lens.FieldOfView = 50;
        _apunting = false;
        DesubrcribeEvents(new Events[] { Events.AttackButton1 });
        SubrcibeEvents(new Events[] { Events.ChangeWeapon });
        FireActionCanceled();
    }

    void FireAction(InputAction.CallbackContext context)
    {
        Fire = true;
        if(weapon.transform.GetChild(0).TryGetComponent<WeaponController>(out var weaponController))
        {
            weaponController.Attack();
        }
    }

   public void FireActionCanceled()
    {
        Fire = false;
    }

    void GrabItem(InputAction.CallbackContext context)
    {
        OnSprintCanceled(context);
        StartCoroutine(AnimationTransition(0.2f,_baseLayerDown,new Events[] { Events.Jump, Events.Celebration, Events.Crouch, Events.Sprint, Events.AttackButton2, Events.ChangeWeapon },"GrabItem"));
        StartCoroutine(DelayAudio(0.5f));
    }

   public void CelebrateDance()
    {
        _playerCamera.SetActive(false);
        _celebrationCamera.SetActive(true);
        _realSpeed = 0;
        DesubrcribeEvents(new Events[] { Events.Jump, Events.Celebration, Events.Crouch, Events.Sprint, Events.AttackButton2, Events.AttackButton1, Events.Movement, Events.GrabItem, Events.ChangeWeapon });
        _noMove = true;
        gameObject.GetComponent<AudioSource>().Play();
        _anim.SetBool("Celebrate",true);
    }

    private IEnumerator DelayAudio(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.GetComponent<AudioSource>().Play();
    }

    public void ModifyLife(float damage)
    {
        Life += damage;
        if(Life<=0) { Death(); }
    }

    public void Death()
    {
        _realSpeed = 0;
        DesubrcribeEvents(new Events[] { Events.Jump, Events.Celebration, Events.Crouch, Events.Sprint, Events.AttackButton2, Events.AttackButton1, Events.Movement, Events.GrabItem, Events.ChangeWeapon});
        _noMove = true;
    }

    public void CallGameOver(string death)
    {
        switch (death)
        {
            case "Yes":
                GameManager.Instance.GameOver(true);
                break;
            case "No":
                GameManager.Instance.GameOver(false);
                break;
        }
    }

    public void DesactiveWeaponCollider()
    {
        if (weapon.transform.GetChild(0).TryGetComponent<MeleeWeaponController>(out var weaponController))
        {
            weaponController.DisableCollider();
        }
    }

    public void ActiveWeaponCollider()
    {
        if (weapon.transform.GetChild(0).TryGetComponent<MeleeWeaponController>(out var weaponController))
        {
            weaponController.EnableCollider();
        }
    }

}
