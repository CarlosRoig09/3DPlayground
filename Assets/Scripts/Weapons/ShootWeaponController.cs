using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootWeaponController : WeaponController
{
    private ShootWeaponData _shootWeaponData;
    private GameObject _spawnBullet;

    protected override void Start()
    {
        base.Start();
        _shootWeaponData = (ShootWeaponData)WeaponData;
        _spawnBullet = transform.GetChild(0).gameObject;
    }
    public override void Attack()
    {
        GameObject newbullet;
        newbullet = Instantiate(_shootWeaponData.ProyectilePrefab, _spawnBullet.transform.position + _spawnBullet.transform.forward * 0.5f, _spawnBullet.transform.rotation);
        newbullet.GetComponent<Rigidbody>().velocity = new Vector3(_spawnBullet.transform.forward.x, _spawnBullet.transform.forward.y, _spawnBullet.transform.forward.z) * _shootWeaponData.ProyectileSpeed;
    }
}
