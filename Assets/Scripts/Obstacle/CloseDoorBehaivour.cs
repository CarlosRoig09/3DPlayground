using EnumLibrary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoorBehaivour : MonoBehaviour, IWaitTillUsableItem
{
    [SerializeField]
    private int _id;
    private Animator _anim;
    [SerializeField]
    private AudioClip _successClip;
    [SerializeField]
    private UsableItemType _type;
    private void Start()
    {
        _anim = GetComponent<Animator>();
    }
    public int GetId()
    {
        return _id;
    }

    public void ItemInteractionEvent()
    {
        GameManager.Instance.ApplyAudioClipForInteraction(_successClip);
        _anim.SetBool("Open", true);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    public UsableItemType UsableItemType()
    {
        return _type;
    }
}
