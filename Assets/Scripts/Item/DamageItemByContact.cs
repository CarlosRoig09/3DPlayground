using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageItemByContact : ItemBehaivour
{
    public override void GiveToPlayer(GameObject inventory)
    {
        if (inventory.transform.parent.TryGetComponent<IDamagable>(out var player) && !_alredyGrabbed)
        {
            _alredyGrabbed = true;
            var damageItem = (DamageItem)itemData;
            player.ModifyLife(damageItem.Damage*-1);
            DestroyItem();
            GameManager.Instance.ApplyAudioClipForInteraction(itemData.Clip);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GiveToPlayer(other.gameObject);
    }
}
