using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerData : MonoBehaviour 
{
    float currTime_;
    float maxTime_;

    bool isInfected_ = false;

    public ArrayList[] m_Skill;

    public string m_JobName;

    Canvas m_PlayerCanvas;


	void Start () 
    {
	
	}
	
	void Update () 
    {
	
	}

    void UpdateTime()
    {

    }

    void UpdateMovement()
    {

    }

    public float GetCurrTime
    {
        get 
        { 
            return currTime_;
        }
    }

    public float GetMaxTime
    {
        get
        {
            return maxTime_;
        }
    }
}
