using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    public delegate void PlayerDetected(GameObject player);
    public event PlayerDetected playerDetected;
    private void OnTriggerEnter(Collider other)
    {
        playerDetected(other.gameObject);
    }
}
