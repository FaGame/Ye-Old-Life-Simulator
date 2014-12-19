using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerData : MonoBehaviour 
{
    //test
    public Habitat m_Home;

    public List<SkillAndAmount> m_Skills;

    public JobData m_Job;
    public Building m_Building;

    public Canvas m_PlayerCanvas;

    public ParticleSystem m_InfectedParticle;

    public float m_DefaultSpeed = 10.0f;
    public float m_CurrTime = 0.0f;
    public float m_MaxTime = ValueConstants.PLAYER_MAX_TIME;
    public float m_HungerMeter = 0.0f;
    public float m_MaxHunger = ValueConstants.PLAYER_MAX_HUNGER;
    public float m_FoodPenalty = 0.0f;
    public float m_Happiness = 0.0f;
    public float m_Speed;

    public int m_Reputation = 0;
    public int m_Shillings = 0;
    public float m_EarningScalar = ValueConstants.PLAYER_DEFAULT_MONEY_SCALAR;    //scalar that is used to determine how much the player will earn that turn for work

    public bool m_IsInfected = false;       //variable used for when the player catches a disease
	public bool m_HasMount = false;			//variable used for when the player has a mount

    public List<ItemEffect> m_StatusEffects = new List<ItemEffect>();

    public UseableItemInventory m_UseableInventory;

    private PlayerController playerController_;

    private Food playerFood_;

    void Start()
    {
        m_CurrTime = m_MaxTime;
        m_Speed = m_DefaultSpeed;
        StartTurn();
        m_Job = null;
        m_Building = null;
        playerController_ = GetComponent<PlayerController>();
        m_MaxHunger = ValueConstants.PLAYER_MAX_HUNGER;
    }
	
	void Update () 
    {
        UpdateStatusEffects();
	}

    public void CalculateFoodPenalty()
    {
        float applyTimePenalty = ValueConstants.PLAYER_HUNGER_PENALTY_LEVEL;
        float maxFoodPenalty = ValueConstants.PLAYER_MAX_FOOD_PENALTY;
        
        if(m_HungerMeter >= applyTimePenalty)
        {
            //if the hunger meter is 75% full apply a 5 second penalty 
            m_FoodPenalty = maxFoodPenalty;
        }
    }

    public void StartTurn()
    {
        //loop through the player's inventory and find objects with the food script
        if(m_UseableInventory != null)
        {
            foreach (KeyValuePair<string, UseableItemInventory.ItemInventoryEntry> entry in m_UseableInventory.m_UseableItemInventory)
            {
                if (entry.Value.item is Food)
                {
                    //set the food variable if the entry is a food type
                    playerFood_ = (Food)entry.Value.item;
                    playerFood_.RemoveFood();   //removes perishable food item
                }
            } 
        }
        m_EarningScalar = ValueConstants.PLAYER_DEFAULT_MONEY_SCALAR;
        //calculate the curr time 
        //Temp commented out since we dont have those penalties set up yet
        m_CurrTime = m_MaxTime; //- m_Home.CalculateHomePenalty() - m_FoodPenalty;
    }

    public void AddEffect(ItemEffect itemEffect)
    {
        m_StatusEffects.Add(itemEffect);

        switch (itemEffect.m_Type)
        {
            case ItemEffect.EffectType.INCOME_MODIFIER:
                m_EarningScalar = itemEffect.m_Value;
                break;
            case ItemEffect.EffectType.SPEED:
                m_Speed *= itemEffect.m_Value;
                break;
        }
    }

    public void UpdateStatusEffects()
    {
        for (int i = 0; i < m_StatusEffects.Count; ++i)
        {
            m_StatusEffects[i].m_Timer -= Time.deltaTime;
            if (m_StatusEffects[i].m_Timer <= 0.0f)
            {
                switch (m_StatusEffects[i].m_Type)
                {
                    case ItemEffect.EffectType.INCOME_MODIFIER:
                        m_EarningScalar = ValueConstants.PLAYER_DEFAULT_MONEY_SCALAR;
                        break;

                    case ItemEffect.EffectType.SPEED:
                        m_Speed /= m_StatusEffects[i].m_Value;
                        break;
                }
                m_StatusEffects.RemoveAt(i);
                i--;
            }
        }
    }

    public bool RemoveSchillings(int amount)
    {
        if(m_Shillings < amount)
        {
            return false;
        }
        else
        {
            m_Shillings -= amount;
            return true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Waypoint")
        {
            Debug.Log("Name: " + other.gameObject.transform.parent.name);
            Building building = other.gameObject.transform.parent.gameObject.GetComponent<Building>();
            if (building != null)
            {
                playerController_.enabled = false;
                building.m_BuildingUI.LoadBuildingData(playerController_, other.gameObject.transform.parent.gameObject);
            }

            Habitat habitat = other.gameObject.transform.parent.gameObject.GetComponent<Habitat>();
            if (habitat != null)
            {
                playerController_.enabled = false;
                habitat.m_HabitatUI.LoadHabitatData(playerController_, other.gameObject.transform.parent.gameObject);
            }
        }
    }

    void UpdatePlayerInfectedStatus(ParticleSystem particle)
    {
        
    }
}
