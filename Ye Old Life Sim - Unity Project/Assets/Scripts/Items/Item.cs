using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Item : MonoBehaviour 
{
    [System.Serializable]
    public struct ItemInventoryEntry
    {
        public Item item;
        public int count;
    };

    public abstract void UseItem(PlayerData playerData);
    public virtual string GetDescription()
    {
        return m_Description;
    }

    public int m_Cost = 0;

    public int m_UseCount = 1; //this is how many times an item can be used
    public int m_DefaultUse = 1;    //reset value for uses

    public string m_Description = "Not Overridden";

    public ItemInventoryEntry m_ItemEntryData;

}
