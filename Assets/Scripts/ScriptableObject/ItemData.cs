using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemData : ScriptableObject
{
    public string Name;
    public string Description;
    public GameObject prefab;
    public int IconId;
    public Vector3 Position;
    public Quaternion Rotation;
    public AudioClip Clip;
    public bool Interactable;
}
