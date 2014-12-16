using UnityEngine;
using System.Collections;

public class Food : Item 
{
    UseableItemInventory m_Inventory;
    public ItemEffect m_Effect;
   
    public bool m_IsPerishable = false;

    public float m_Timer = 0.0f;
    public float m_HungerAmount = 0.0f;     //how much you want to subtract from player hunger
    public float m_SpeedModifier = 1.0f;    //scalar to change player speed
    public float m_Happiness = 0.0f;        //used to increase player's happiness

    public string m_FoodName = "";

    void Start()
    {
        m_Effect.m_Type = ItemEffect.EffectType.SPEED;      //sets the type to be speed so when the players uses a food item speed is changed
        m_Effect.m_Timer = m_Timer;
        m_Effect.m_Value = m_SpeedModifier;
    }

    //when the function is called subtract a value from the hunger meter and increase or decrease player speed
    public override void UseItem(PlayerData playerData)
    {
        if (m_UseCount != 0)
        {
            m_UseCount--;  //subtract 1 from the count of uses

            //uses the AddEffect function which sets all values based on the ItemEffect specified 
            playerData.AddEffect(m_Effect);
            playerData.m_HungerMeter -= m_HungerAmount;     //subtract from player's hunger
            playerData.m_Happiness += m_Happiness;          //add to player's happiness
        }       
    }

    public void RemoveFood()
    {
        if(m_IsPerishable)
        {
            //remove the food item after the player turn when the item is perishable
            m_Inventory.RemoveFromInventory(m_FoodName);
        }
    }
}
