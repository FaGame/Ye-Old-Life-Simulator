using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

public class UseableItemInventory : MonoBehaviour
{
    public struct ItemInventoryEntry
    {
        public Item item;
        public int count;
    };

    public Dictionary<string, UseableItemInventory.ItemInventoryEntry> m_UseableItemInventory;
    void Start()
    {
        m_UseableItemInventory = new Dictionary<string, ItemInventoryEntry>();
    }

}
