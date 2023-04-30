using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlInventory : MonoBehaviour
{
    [SerializeField]
    private Inventory inventory;
    private int _weaponCounter;
    private int _itemCounter;
    private ChangeItemsController _changeWeapon;
    private PlayerMovement _playerMovement;
    public delegate void UseKey();
    public event UseKey OnUseKey;
    [SerializeField]
    private AudioClip _failClip;
    // Start is called before the first frame update
    void Start()
    {
        inventory.Weapons = new List<WeaponData>();
        inventory.Items = new List<UsableItem>();
        _weaponCounter = 0;
        _itemCounter = 0;
        TryGetComponent(out _changeWeapon);
        transform.parent.TryGetComponent(out _playerMovement);
    }

    private bool AddWeapon(WeaponData weaponData)
    {
        var cloneWeapon = Instantiate(weaponData);
        if (inventory.LimitWeapons > inventory.Weapons.Count)
        {
                inventory.Weapons.Add(cloneWeapon);
                SetNewWeapon(inventory.Weapons[_weaponCounter]);
            _weaponCounter += 1;
                return true;
        }
        else
        {
            Debug.Log("No more space");
            return true;
        }
    }

    private void AddEquipableItem(UsableItem usableItem)
    {
        var cloneItem = Instantiate(usableItem);
        if(inventory.LimitItems > inventory.Items.Count)
        {
            inventory.Items.Add(usableItem);
            _changeWeapon.ItemToPlayerModel(inventory.Items[_itemCounter]);
             _itemCounter += 1;
        }
    }

    public void AddItem(ItemData itemData)
    {
        UIManager.Instance.ShowColectedItem(itemData);
        if (itemData is WeaponData weaponData)
            AddWeapon(weaponData);
        else
            AddEquipableItem((UsableItem)itemData);

    }
    public void ChangeWeapon()
    {
        if (inventory.Weapons.Count > 1)
        {
            _changeWeapon.ChangeWeapons();
        }
    }
    public void SetNewWeapon(WeaponData newweapon)
    {
        _changeWeapon.AddNewWeapon(newweapon);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Event"))
        {
            UIManager.Instance.ShowEmergencyText("Press [E] to interact");
        }
        else if (other.CompareTag("Item") && other.GetComponent<ItemBehaivour>().itemData.Interactable)
        {
            UIManager.Instance.ShowEmergencyText("Press [E] to grab the item");
        }
        _playerMovement.SubrcibeEvents(new Events[] { Events.GrabItem });
        if (_playerMovement.GrabingCurrentItem)
        {
            if (other.CompareTag("Item"))
            {
                other.TryGetComponent<ItemBehaivour>(out var item);
                item.GiveToPlayer(gameObject);
            }
            else
            {
                UsableItemAction(other.gameObject.GetComponent<IWaitTillUsableItem>());
            }
            UIManager.Instance.HideEmergencyText();
        }
    }

    public bool UsableItemAction(IWaitTillUsableItem eventor)
    {
        bool grabItem = false;
        if (_playerMovement.GrabingCurrentItem)
        {
            foreach (var usableItem in inventory.Items)
            {
                if (usableItem.Type == eventor.UsableItemType())
                {
                    if (usableItem.Script.DoEvent(eventor))
                    {
                        inventory.Items.Remove(usableItem);
                        _itemCounter -= 1;
                        grabItem = true;
                    }
                    return true;
                }
            }
            if (!grabItem)
            {
                GameManager.Instance.ApplyAudioClipForInteraction(_failClip);
                UIManager.Instance.ShowEmergencyText("You need the correct key to open de door");
            }
                
        }
        return false;
    }

    private void OnTriggerExit(Collider other)
    {
        _playerMovement.DesubrcribeEvents(new Events[] { Events.GrabItem });
        UIManager.Instance.HideEmergencyText();
    }
}
