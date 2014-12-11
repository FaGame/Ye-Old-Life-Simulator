using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerData : MonoBehaviour 
{
    public Habitat m_Home;

    public List<SkillAndAmount> m_Skills;

    public string m_JobName;

    public Canvas m_PlayerCanvas;

    public float m_CurrTime = 0.0f;
    public float m_MaxTime = ValueConstants.PLAYER_MAX_TIME;
    public float m_HungerMeter = 0.0f;
    public float m_MaxHunger = ValueConstants.PLAYER_MAX_HUNGER;
    public float m_FoodPenalty = 0.0f;
    public float m_Happiness = 0.0f;

    public int m_Reputation = 0;
    public int m_Shillings = 0;
    public int m_EarningScalar = ValueConstants.PLAYER_DEFAULT_MONEY_SCALAR;    //scalar that is used to determine how much the player will earn that turn for work

    public bool m_IsInfected = false;       //variable used for when the player catches a disease

	void Start () 
    {
        StartTurn();
	}
	
	void Update () 
    {
	
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
        m_EarningScalar = ValueConstants.PLAYER_DEFAULT_MONEY_SCALAR;
        //calculate the curr time 
        m_CurrTime = m_MaxTime - m_Home.m_Penalty - m_FoodPenalty;
    }  
}
