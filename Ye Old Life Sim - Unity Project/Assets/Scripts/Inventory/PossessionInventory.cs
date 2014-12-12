using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

public class PossessionInventory : MonoBehaviour 
{
    [System.Serializable]
    public struct ItemInventoryEntry
    {
        public Item item;
        public int count;
    };

    //used for inspector use, place the names of the items in here
    public string[] m_Names;
    //used for inspector use, place your items in here
    public ItemInventoryEntry[] m_InspectorInventory;
    //this is what you need to use when accessing the inventory
    public Dictionary<string, PossessionInventory.ItemInventoryEntry> m_PossessionItemInventory;

    void Start()
    {
        m_PossessionItemInventory = new Dictionary<string, ItemInventoryEntry>();

        //loops through the array of names and InspectorInventory, then adds them to the m_UseableItemInventory list
        for (int i = 0; i < m_InspectorInventory.Length; ++i)
        {
            for (int j = 0; j < m_Names.Length; ++j)
            {
                AddToInventory(m_Names[j], m_InspectorInventory[i]);
            }
        }
    }

    public void AddToInventory(string name, ItemInventoryEntry item)
    {
        //adds an item to the list based on a name given and takes in a list of type ItemInventoryEntry
        if (!m_PossessionItemInventory.ContainsKey(name))
        {
            m_PossessionItemInventory.Add(name, item);
        }
        else
        {
            ItemInventoryEntry entry = item;
            item.count += m_PossessionItemInventory[name].count;
            m_PossessionItemInventory[name] = item;
        }
    }
}
