using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataCollection : MonoBehaviour 
{
    public PlayerData m_PlayerData;
    public PlayerData m_AIData;
    public GameManager m_Game;
    public BuildingUI m_BUI;
    public HabitatUI m_HUI;
    public RandomEventManager1 m_REM;
   
    public List<string> m_StringThings = new List<string>();
    public List<int> m_IntThings = new List<int>();
    public List<float> m_playerStats = new List<float>();
    public List<int> m_habitatsInteractedWith = new List<int>();

    private List<float> playerStats_ = new List<float>();
    private List<int> Turns_ = new List<int>();
    private List<int> RandomEvents_ = new List<int>();
    private List<float> TimeWhenThingHappened_ = new List<float>();
    private int homeRating_;
    
	public void PopulateStats()
    {
        playerStats_.Add((float)m_PlayerData.m_Home.m_Rating);
        playerStats_.Add(m_PlayerData.m_HungerMeter);
        playerStats_.Add(m_PlayerData.m_Reputation);
        playerStats_.Add(m_PlayerData.m_Shillings);
        playerStats_.Add(m_PlayerData.m_Happiness);
    }

    public void SetStats()
    {
        m_playerStats[0] = homeRating_;
        m_AIData.m_HungerMeter = m_playerStats[1];
        m_AIData.m_Reputation = (int)m_playerStats[2];
        m_AIData.m_Shillings = (int)m_playerStats[3];
        m_AIData.m_Happiness = m_playerStats[4];
    }

    void SetHome()
    {
        //tie into rating enums with homeRating_
    }

    public void AddTurns()
    {
        Turns_.Add(m_Game.m_Turns);
    }

    public void AddIntThings()
    {
        m_IntThings.Add(m_BUI.m_currInt);

        if (m_BUI.ReturnAction() >= 17)
        {
            m_IntThings.Add(m_BUI.ReturnAction());
        }

       // m_IntThings.Add(m_UII.m_currInt);

    }

    public void AddRandomEvents()
    {
        RandomEvents_.Add(m_REM.ReturnRdmEvent());
    }

}
