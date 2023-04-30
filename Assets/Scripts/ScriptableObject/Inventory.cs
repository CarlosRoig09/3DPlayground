using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory")]
public class Inventory : ScriptableObject
{
    public List<WeaponData> Weapons;
    public float LimitWeapons;
    public List<UsableItem> Items;
    public float LimitItems;
}
