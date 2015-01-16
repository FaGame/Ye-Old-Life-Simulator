using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

public class UseableItemInventory : MonoBehaviour
{

    public DataCollection m_DataCollection;
    //used for inspector use, place the names of the items in here
    public string[] m_Names;
    //used for inspector use, place your items in here
    //public ItemInventoryEntry[] m_InspectorInventory;
    public string m_ItemBought;
    public string m_ItemUsed;

    public Dictionary<string, Item.ItemInventoryEntry> m_UseableItemInventory;

    void Awake()
    {
        m_UseableItemInventory = new Dictionary<string, Item.ItemInventoryEntry>();

        //loops through the array of names and InspectorInventory, then adds them to the m_UseableItemInventory list
        /*if (m_Names.Length != Item.m_InspectorInventory.Length)
        {
            Debug.Log("Names and InspectorInventory lenghts do not match. Shit.");
        }
        else
        {
            for (int i = 0; i < m_Names.Length; ++i)
            {
                AddToInventory(m_Names[i], m_InspectorInventory[i]);
            }
        }*/
    }

    public void AddToInventory(string name, Item.ItemInventoryEntry item)
    {
        //adds an item to the list based on a name given and takes in a list of type ItemInventoryEntry
        if (!m_UseableItemInventory.ContainsKey(name))
        {
            m_UseableItemInventory.Add(name, item);
        }
        else
        {
            //ItemInventoryEntry entry = item;
            item.count += m_UseableItemInventory[name].count;
            m_UseableItemInventory[name] = item;
        }
    }

    public void RemoveFromInventory(string name)
    {
        //removes an item from the ItemInventoryEntry based on the name given
        if (m_UseableItemInventory.ContainsKey(name))
        {
            Item.ItemInventoryEntry item = m_UseableItemInventory[name];
            if(item.item.m_UseCount <= 0)
            {
                item.item.m_UseCount = item.item.m_DefaultUse;
                item.count--;
            }
            
            m_UseableItemInventory[name] = item;

            if(item.count <= 0)
            {
                m_UseableItemInventory.Remove(name);
            }
        }
    }

    public void UseItem(PlayerData playerData, string usedItem)
    {
        foreach (KeyValuePair<string, Item.ItemInventoryEntry> currentItem in m_UseableItemInventory)
        {
            if (currentItem.Key == usedItem)
            {
                currentItem.Value.item.UseItem(playerData);
                RemoveFromInventory(currentItem.Key);
                break;
            }
        }
    }
}
