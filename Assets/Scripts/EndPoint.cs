using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    public AudioClip _victory;
    private void OnTriggerExit(Collider other)
    {
        GameManager.Instance.ApplyAudioClipForInteraction(_victory);
        other.GetComponent<PlayerMovement>().CelebrateDance();
    }
}
