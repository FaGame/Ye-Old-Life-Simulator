using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerData : MonoBehaviour 
{
    public ArrayList[] m_Skill;

    public string m_JobName;

    public Canvas m_PlayerCanvas;

    private float currTime_;
    private float maxTime_;

    private bool isInfected_ = false;

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
