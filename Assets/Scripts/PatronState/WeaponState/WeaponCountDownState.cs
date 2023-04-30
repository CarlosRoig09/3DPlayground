using UnityEngine;

[CreateAssetMenu(fileName = "WeaponCountDownState", menuName = "WeaponCountDownState")]
public class WeaponCountDownState : ScriptableAction
{
    public override void OnFinishedState()
    {
        Debug.Log("Finisg countDown");
    }

    public override void OnSetState()
    {
        Debug.Log("I started the countDown");
        base.OnSetState();
        weaponData.Script.WaitTime(weaponData.Countdown);
    }

    public override void OnUpdate()
    {
        if (!weaponData.StateIsActive)
            weaponData.Script.Idle();
    }
}
