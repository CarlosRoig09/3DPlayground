using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private int _damage;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<IDamagable>(out var enemy))
        {
            enemy.ModifyLife(_damage*-1);
        }
        Destroy(gameObject);
    }
}
