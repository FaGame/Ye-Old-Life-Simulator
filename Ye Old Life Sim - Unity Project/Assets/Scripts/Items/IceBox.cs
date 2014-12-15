using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IceBox : MonoBehaviour 
{
    List<Food> playerFood_ = new List<Food>();                      //list of the food the player has
    public UseableItemInventory m_PlayerUseableItemInventory;       //place the player's inventory in here  

    void Start()
    {     
        //get all the items with the Food script in them
        Food[] playerInventoryFood = m_PlayerUseableItemInventory.GetComponentsInChildren<Food>();
        for (int i = 0; i < playerInventoryFood.Length; ++i)
        {
            //add the array of inventoryFood to the list of playerFood_
            playerFood_.Add(playerInventoryFood[i]);
        }       
        KeepFoodFresh();
    }

    public void KeepFoodFresh()
    {
        //set the food to be not perishable
        for (int i = 0; i < playerFood_.Count; ++i)
        {
            playerFood_[i].m_IsPerishable = false;
        }
    }
}
