using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyLifeItemByContact : ItemBehaivour
{
    public override void GiveToPlayer(GameObject inventory)
    {
        if (inventory.transform.parent.TryGetComponent<IDamagable>(out var player) && !_alredyGrabbed)
        {
            _alredyGrabbed = true;
            var damageItem = (ModifyLifeItem)itemData;
            player.ModifyLife(damageItem.LifeModificator);
            DestroyItem();
            GameManager.Instance.ApplyAudioClipForInteraction(itemData.Clip);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GiveToPlayer(other.gameObject);
    }
}
