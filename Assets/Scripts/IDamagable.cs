using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable 
{
    public abstract void ModifyLife(float damage);
    public abstract void Death();
}
