using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerData : MonoBehaviour 
{
    public Habitat m_Home;

    public SkillAndAmount[] m_Skills;

    public string m_JobName;

    public Canvas m_PlayerCanvas;

    public float m_CurrTime = 0.0f;
    public float m_MaxTime = 45.0f;
    public float m_HungerMeter = 0.0f;
    public float m_MaxHunger = 100.0f;
    public float m_FoodPenalty = 0.0f;
    public float m_Happiness = 0.0f;

    public int m_Rep = 0;
    public int m_Shillings = 0;

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
        float applyTimePenalty = 75.0f;
        float maxFoodPenalty = 5.0f;
        
        if(m_HungerMeter >= applyTimePenalty)
        {
            //if the hunger meter is 75% full apply a 5 second penalty 
            m_FoodPenalty = maxFoodPenalty;
        }
    }

    public void StartTurn()
    {
        //calculate the curr time 
        m_CurrTime = m_MaxTime - m_Home.m_Penalty - m_FoodPenalty;
    }  
}
