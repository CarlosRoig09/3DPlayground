using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShootWeaponData", menuName = "ShootWeaponData")]
public class ShootWeaponData : WeaponData
{
    public GameObject ProyectilePrefab;
    public float ProyectileSpeed;
    public float Range;
    public float ProyectileDamage;
    public float HeadDamageMultiplicator;
    public float TimeBeforeProyectileSpawn;
}
