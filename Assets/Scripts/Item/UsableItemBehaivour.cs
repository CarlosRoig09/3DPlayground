using UnityEngine;

public class UsableItemBehaivour : MonoBehaviour
{
    public delegate void UsableItemEvent();
    public event UsableItemEvent usableItemEvent;
    public ControlInventory ControlInventory;
    private UsableItem _usableItem;
    public UsableItem UsableItem { get { return _usableItem; } set { _usableItem = value; } }

    private void Start()
    {
        GameManager.Instance.EventSubscriberForUsableItem(this);
        _usableItem.Script = this;
    }
    public bool DoEvent(IWaitTillUsableItem eventor)
    {

      if (eventor != null && eventor.GetId() == _usableItem.Id)
        {
           usableItemEvent();
           Destroy(gameObject);
            return true;
        }

      return false;
    }
}
