using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

public class UseableItemInventory : MonoBehaviour
{
    [System.Serializable]
    public struct ItemInventoryEntry
    {
        public Item item;
        public int count;
    };

    public string[] m_Names;
    public ItemInventoryEntry[] m_InspectorInventory;
    public Dictionary<string, UseableItemInventory.ItemInventoryEntry> m_UseableItemInventory;

    void Start()
    {
        m_UseableItemInventory = new Dictionary<string, ItemInventoryEntry>();

        //loops through the array of names and InspectorInventory, then adds them to the m_UseableItemInventory list
        for (int i = 0; i < m_InspectorInventory.Length; ++i)
        {
            for(int j = 0; j < m_Names.Length; ++j)
            {
                AddToInventory(m_Names[j], m_InspectorInventory[i]);
            }    
        }     
    }

    public void AddToInventory(string name, ItemInventoryEntry item)
    {
        //adds an item to the list based on a name given and takes in a list of type ItemInventoryEntry
        m_UseableItemInventory.Add(name, item);
    }

    public void RemoveFromInventory(string name)
    {
        //removes an item from the ItemInventoryEntry based on the name given
        m_UseableItemInventory.Remove(name);
    }
}
