using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageItemBehaivour : ItemBehaivour
{
    public override void GiveToPlayer(GameObject player)
    {
        throw new System.NotImplementedException();
    }

    private void OnTriggerEnter(Collider other)
    {
        GiveToPlayer(other.gameObject);
    }
}
