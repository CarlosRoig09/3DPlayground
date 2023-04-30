using UnityEngine;
using EnumLibrary;

[CreateAssetMenu(fileName = "WeaponAttackAction", menuName = "WeaponAttackAction")]
public class WeaponAttackAction : ScriptableAction
{
    public override void OnFinishedState()
    {
        Debug.Log("Attack Finished");
    }

    public override void OnSetState()
    {
        Debug.Log("Attack Start");
        base.OnSetState();
        weaponData.Script.Attack();
        weaponData.Script.WaitTime(weaponData.AttackDuration);
    }

    public override void OnUpdate()
    {
        if (!weaponData.StateIsActive)
            weaponData.Script.CountDown();
    }
}
