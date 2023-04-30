using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ItemBehaivour : MonoBehaviour
{
    public ItemData itemData;
    protected bool _alredyGrabbed;
    
    private void Start()
    {
        _alredyGrabbed = false;
    }
    public abstract void GiveToPlayer(GameObject inventory);
    protected  void DestroyItem()
    {
        Destroy(gameObject);
    }
}
