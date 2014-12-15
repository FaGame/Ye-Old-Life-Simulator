using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IceBox : MonoBehaviour 
{
    List<Food> food_ = new List<Food>();                            //list of the food the player has
    public UseableItemInventory m_PlayerUseableItemInventory;       //place the player's inventory in here  

    void Start()
    {
        SetFoodList();  
        KeepFoodFresh();
    }

    public void KeepFoodFresh()
    {
        //set the food to be not perishable
        for (int i = 0; i < food_.Count; ++i)
        {
            food_[i].m_IsPerishable = false;
        }
    }

    public void SetFoodList()
    {
        //loop through the inventory and searches for any items with the Food script in them and adds it to the list of food_
        foreach (KeyValuePair<string, UseableItemInventory.ItemInventoryEntry> entry in m_PlayerUseableItemInventory.m_UseableItemInventory)
        {
            if (entry.Value.item is Food)
            {
                 food_.Add((Food)entry.Value.item);
            }
        }    
    }
}
