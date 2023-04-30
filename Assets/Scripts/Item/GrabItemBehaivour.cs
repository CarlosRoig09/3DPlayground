using UnityEngine;

public class GrabItemBehaivour : ItemBehaivour
{
    public override void GiveToPlayer(GameObject player)
    {
        if (player.TryGetComponent<ControlInventory>(out var controlInventory) && !_alredyGrabbed)
        {
            _alredyGrabbed = true;
            controlInventory.AddItem(itemData);
            DestroyItem();
            GameManager.Instance.ApplyAudioClipForInteraction(itemData.Clip);
        }
    }
}
