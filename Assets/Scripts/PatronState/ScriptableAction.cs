using UnityEngine;

public abstract class ScriptableAction : ScriptableObject
{
    public WeaponData weaponData;
    public abstract void OnFinishedState();

    public virtual void OnSetState()
    {
        weaponData.StateIsActive = true;
    }

    public abstract void OnUpdate();

}