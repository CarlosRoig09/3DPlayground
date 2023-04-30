using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumLibrary;

[CreateAssetMenu(fileName = "UsableItemData", menuName = "UsableItemData")]
public class UsableItem : ItemData
{
    public UsableItemBehaivour Script;
    public UsableItemType Type;
    public int Id;
}
