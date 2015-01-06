using UnityEngine;
using System.Collections;

public class Food : Item 
{
    public UseableItemInventory m_Inventory;
    public ItemEffect m_Effect;
    public AudioClip m_FoodSound;
   
    public bool m_IsPerishable = false;

    public float m_Timer = 0.0f;
    public float m_HungerAmount = 0.0f;     //how much you want to subtract from player hunger
    public float m_SpeedModifier = 1.0f;    //scalar to change player speed
    public float m_Happiness = 0.0f;        //used to increase player's happiness
    public float m_HungerSpeed = 0.0f;      //this is for items that increase "Rate of Hunger returning"

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
            AudioSource.PlayClipAtPoint(m_FoodSound, transform.position);
            m_UseCount--;  //subtract 1 from the count of uses

            //uses the AddEffect function which sets all values based on the ItemEffect specified 
            playerData.AddEffect(m_Effect);
            playerData.m_HungerMeter -= m_HungerAmount;     //subtract from player's hunger
            if(playerData.m_HungerMeter < 0.0f)
            {
                playerData.m_HungerMeter = 0.0f;
            }
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

    public float MORE_HUNGER = 40;
    public float MEDIOCRE_HUNGER = 20;
    public float LITTLE_HUNGER = 5;

    public float MORE_HAPPINESS = 40;
    public float MEDIOCRE_HAPPINESS = 20;
    public float LITTLE_HAPPINESS = 5;

    public override string GetDescription()
    {
        string retval = "";
        
        //HUNGER
        if(m_HungerAmount > MORE_HUNGER)
        {
            retval = "Very filling food item. ";
        }
        else if(m_HungerAmount > MEDIOCRE_HUNGER)
        {
            retval = "Satisfies hunger. ";
        }
        else if(m_HungerAmount > LITTLE_HUNGER)
        {
            retval = "Not filling at all. ";
        }

        //HAPPINESS
        if(m_Happiness > MORE_HAPPINESS)
        {
            retval += "Fills you with joy! ";
        }
        else if(m_Happiness > MEDIOCRE_HAPPINESS)
        {
            retval += "Makes you happy. ";
        }
        else if(m_Happiness > LITTLE_HAPPINESS)
        {
            retval += "This happiness won't last. ";
        }

        //SPEED
        if(m_SpeedModifier == 1.0f)
        {
            retval += "Things are going to slow down. ";
        }
        else if(m_SpeedModifier < 1.0f)
        {
            retval += "Can time go this slow?! ";
        }

        //hungerRATE
        if(m_HungerSpeed == 0)
        {
            retval += "This isn't going to help your Hunger. ";
        }
        else if(m_HungerSpeed < 0)
        {
            retval += "Feeding your appetite is a better idea.. ";
        }

        return retval;
    }
}
