using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumLibrary;
public interface IWaitTillUsableItem 
{
    public abstract UsableItemType UsableItemType();
    public abstract int GetId();
    public abstract void ItemInteractionEvent();
}
