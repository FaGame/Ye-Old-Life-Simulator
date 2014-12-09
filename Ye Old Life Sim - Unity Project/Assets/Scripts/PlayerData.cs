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
