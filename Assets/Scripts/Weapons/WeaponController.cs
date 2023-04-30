using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class WeaponController : StateController
{
    public WeaponData WeaponData;
    private ScriptableState _attack;
    private ScriptableState _countDown;
    private ScriptableState _idle;
    public InputAction _weaponAttack;
    public InputAction _changeWeapon;
    public InputAction _weaponApunt;
    public ControlInventory _controlInventory;
    public PlayerMovement _playerMovement;
    // protected AnimationController _playerAnimator;
    protected virtual void Start()
    {
        Debug.Log("Start name: " + gameObject.name);
        WeaponData.Script = this;
        _idle = WeaponData.States[0];
        _countDown = WeaponData.States[1];
        _attack = WeaponData.States[2];
        currentState.Action.weaponData = WeaponData;
        _playerMovement.SubrcibeEvents(new Events[] {Events.AttackButton2});
    }
    public void Idle()
    {
        ChangeStateWeapon(_idle);
    }
    public void WeaponAttack(InputAction.CallbackContext context)
    {
        ChangeStateWeapon(_attack);
    }
    public void CountDown()
    {
        ChangeStateWeapon(_countDown);
    }

    public abstract void Attack();

    public void Apunt()
    {
       if (!WeaponData.StateIsActive) 
            { 
              _playerMovement.SubrcibeEvents(new Events[] {Events.AttackButton1});
            }
    }

    private void ChangeStateWeapon(ScriptableState state)
    {
        state.Action.weaponData = WeaponData;
        StateTransitor(state);
    }

    public void WaitTime(float time)
    {
        StartCoroutine(Countdown(time));
    }
    private IEnumerator Countdown(float time)
    {
        yield return new WaitForSeconds(time);
        WeaponData.StateIsActive = false;
    }
    private void OnDestroy()
    {
        _playerMovement.DesubrcribeEvents(new Events[] { Events.AttackButton2, Events.AttackButton1});
    }
}
