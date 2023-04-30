using UnityEngine;
using EnumLibrary;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;

public class ChangeItemsController : MonoBehaviour
{
    public GameObject Player;
    public GameObject Weapon;
    public GameObject WeaponHolder2;
    public List<GameObject> ItemHolders;
    private int itemHolderCount;
  // private AnimationController _playerAnimController;
    private void Awake()
    {
       //Player.TryGetComponent(out _playerAnimController);

    }
    private void Start()
    {
        itemHolderCount= 0;
        Player = GameObject.Find("Player");
        Weapon = GameObject.Find("Weapon").gameObject;
        WeaponHolder2 = GameObject.Find("Weapon2Holder").gameObject;
        ItemHolders = new List<GameObject>() { GameObject.Find("ItemHolder").gameObject};
    }
    public void AddNewWeapon(WeaponData weaponData)
    {
        GameObject newWeapon = Instantiate(weaponData.prefab);
        newWeapon.TryGetComponent<WeaponController>(out var weaponController);
        weaponController.WeaponData = weaponData;
        weaponController._playerMovement = Player.GetComponent<PlayerMovement>();
        weaponController._controlInventory = GetComponent<ControlInventory>();
        weaponData.EventSubscriber = true;
        SetAItemInAHolder(newWeapon, Weapon.transform.childCount>0 ? WeaponHolder2 : Weapon, Weapon.transform.childCount > 0 ? false : true, weaponData);
    }

    public void ChangeWeapons()
    {
        GameObject HandWeapon = Weapon.transform.GetChild(0).gameObject;
        GameObject GuardWeapon = WeaponHolder2.transform.GetChild(0).gameObject;
        ParentAndChildrenMethods.UnParentAnSpecificChildren(Weapon, HandWeapon);
        ParentAndChildrenMethods.UnParentAnSpecificChildren(WeaponHolder2, GuardWeapon);
        SetAItemInAHolder(HandWeapon, WeaponHolder2,false, HandWeapon.GetComponent<WeaponController>().WeaponData);
        SetAItemInAHolder(GuardWeapon, Weapon, true, GuardWeapon.GetComponent<WeaponController>().WeaponData);
    }

    public void ItemToPlayerModel(UsableItem usableItem)
    {
        GameObject newItem = Instantiate(usableItem.prefab);
        newItem.TryGetComponent<UsableItemBehaivour>(out var uIB);
        uIB.UsableItem= usableItem;
        uIB.ControlInventory = gameObject.GetComponent<ControlInventory>();
        SetAItemInAHolder(newItem, ItemHolders[itemHolderCount], false, usableItem);
        itemHolderCount += 1;
    }

    private void SetAItemInAHolder(GameObject item, GameObject itemHolder, bool primaryWeapon, ItemData itemData)
    {
       item.transform.SetPositionAndRotation(itemHolder.transform.position,itemHolder.transform.rotation);
            ParentAndChildrenMethods.ParentAChildren(itemHolder, item);
        if (primaryWeapon&& itemData is WeaponData weaponData)
        {
             item.transform.localPosition = weaponData.WeaponPosition;
             item.transform.localRotation = weaponData.WeaponRotation;
            weaponData.EventSubscriber = true;
            AnimationWeights(weaponData.AnimType);
        }
        else
        {
            item.transform.localPosition = itemData.Position;
            item.transform.localRotation = itemData.Rotation;
            if (itemData is WeaponData weaponData2)
            {
                weaponData2.EventSubscriber = false;
            }
        }
    }

    private void AnimationWeights(WeaponAnimType AnimType)
    {
        var pM = Player.GetComponent<PlayerMovement>();
        switch (AnimType)
        {
            case WeaponAnimType.PunchWeapon:
                pM.WeaponApuntLayer = 10;
                pM.WeaponFireLayer = 11;
                break;
            case WeaponAnimType.TwoHandedFireWeapon:
                pM.WeaponApuntLayer = 4;
                pM.WeaponFireLayer = 5;
                break;
        }
    }

    private IEnumerator WaitTillAnimationEnd(int layer, float weight,float speed)
    {
       // while(_playerAnimController.AnimTransition)
        yield return null;
        //_playerAnimController.SetAnimationLayerWeight(layer, weight, speed);
    }
}
