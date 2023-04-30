using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponList", menuName = "WeaponList")]
public class WeaponList : ScriptableObject
{
    //Lista de todas las weapons disponibles
    public List<WeaponData> ImaginaryWeapons;
    //Weapon que el jugador puede obtener ahora mismo
    public WeaponData CurrentImaginaryWeapon;
}
