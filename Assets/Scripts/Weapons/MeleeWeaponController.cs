using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumLibrary;

public class MeleeWeaponController : WeaponController
{
    private MeleeWeaponData _meleeWeapon;
    private BoxCollider _boxCollider;
    protected override void Start()
    {
        base.Start();
        _meleeWeapon = (MeleeWeaponData)WeaponData;
        _boxCollider = GetComponent<BoxCollider>();
        DisableCollider();
    }
    public override void Attack()
    {
        _playerMovement.Fire = true;
    }

    public void EnableCollider()
    {
        _boxCollider.enabled = true;
        Debug.Log("Active");
    }

    public void DisableCollider()
    {
        _boxCollider.enabled = false;
        Debug.Log("Desactive");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ICanBeImpulse>(out var canBeImpulse))
        {
            Debug.Log("MueveteeeeeAAAAA");
            Vector3 enemyDirection = other.gameObject.transform.position - transform.position;
            enemyDirection.y *= 0.5f;
            canBeImpulse.GetImpulse(enemyDirection.normalized * _meleeWeapon.Impulse);
        }
    }
}
