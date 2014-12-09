using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerData : MonoBehaviour 
{
    public ArrayList[] m_Skill;

    public string m_JobName;

    public Canvas m_PlayerCanvas;

    public float m_CurrTime;
    public float m_MaxTime;
    public float m_HungerMeter;
    public float m_MaxHunger;
    public float m_Happiness;

    public int m_Rep;
    public int m_Shillings;

    public bool m_IsInfected = false;       //variable used for when the player catches a disease

	void Start () 
    {
	
	}
	
	void Update () 
    {
	
	}

    void UpdateTime()
    {

    }

}
