using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UseItemButton : MonoBehaviour 
{
    public UseableItemInventory m_InventoryScript;
    public DataCollection m_DataCollection;
    public string m_ItemUsed;

    public void UseItem(PlayerData playerData)
    {
        string nameOfObject = gameObject.name;
        if (nameOfObject == "InventoryItemButton")
        {
            //close inventory
        }
        else if(nameOfObject == null)
        {
            return;
        }
        else
        {
            foreach (KeyValuePair<string, Item.ItemInventoryEntry> currentItem in m_InventoryScript.m_UseableItemInventory)
            {
                if(currentItem.Key == nameOfObject)
                {
                    currentItem.Value.item.UseItem(playerData);
                    //////////////////////////////////////////////////////////
                    m_ItemUsed = nameOfObject;
                    m_DataCollection.AddItemUsed();
                    //////////////////////////////////////////////////////////
                    m_InventoryScript.RemoveFromInventory(currentItem.Key);
                }
            }
            return;
        }
    }
}
