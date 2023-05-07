using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICanBeImpulse 
{
    public abstract void GetImpulse(Vector3 impulse);
    public abstract void StopMomentum();
    public abstract Vector3 TrasspassImpulse(Vector3 impulse);

    public abstract void OnHit();
}
