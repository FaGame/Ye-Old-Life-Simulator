using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataCollection : MonoBehaviour 
{
    public PlayerData m_PlayerData;
    public GameManager m_Game;
    public BuildingUI m_BUI;
    public HabitatUI m_HUI;

    private List<float> playerStats_ = new List<float>();
    private List<int> buildingsInteractedWith_ = new List<int>();
    private List<int> actionsTaken_ = new List<int>();
    private List<int> Turns_ = new List<int>();
    private List<int> habitatsInteractedWith_ = new List<int>();
    
	public void PopulateStats()
    {
        playerStats_.Add((float)m_PlayerData.m_Home.m_Rating);
        playerStats_.Add(m_PlayerData.m_HungerMeter);
        playerStats_.Add(m_PlayerData.m_Reputation);
        playerStats_.Add(m_PlayerData.m_Shillings);
        playerStats_.Add(m_PlayerData.m_Happiness);
    }

    public void AddTurns()
    {
        Turns_.Add(m_Game.m_Turns);
    }

    public void AddBuildings()
    {
        buildingsInteractedWith_.Add(m_BUI.ReturnBuilding());
    }

    public void Actions()
    {
        actionsTaken_.Add(m_BUI.ReturnAction());
    }

    public void AddHabitats()
    {
        habitatsInteractedWith_.Add(m_HUI.ReturnHabitat());
    }
}
