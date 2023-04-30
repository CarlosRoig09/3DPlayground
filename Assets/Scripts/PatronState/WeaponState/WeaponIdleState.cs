using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "WeaponIdleState", menuName = "WeapondIdleState")]
public class WeaponIdleState : ScriptableAction
{
    public override void OnFinishedState()
    {
        weaponData.Script._weaponAttack.started -= weaponData.Script.WeaponAttack;
        Debug.Log("Finish Idle");
       // weaponData.Anim.SetBool("Idle", false);
    }

    public override void OnUpdate()
    {
        if (weaponData.EventSubscriber)
        {
            weaponData.Script._weaponAttack.started += weaponData.Script.WeaponAttack;
            weaponData.EventSubscriber = false;
        }

    }
    public override void OnSetState()
    {
        Debug.Log("Start Idle");
        weaponData.EventSubscriber = true;
        //weaponData.Anim.SetBool("Idle", true);
    }
}
