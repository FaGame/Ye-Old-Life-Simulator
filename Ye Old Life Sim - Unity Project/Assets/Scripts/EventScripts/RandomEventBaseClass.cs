using UnityEngine;
using System.Collections;

public abstract class RandomEventBaseClass 
{
	public int m_RndEvent;
	public int m_EventIndex;
	public int m_MoneyLost;
	public int m_MoneyGained;
	public float m_ExperienceGained;
	public float m_HappinesChange;
	public float m_TimeChange;

	public PlayerData m_Player;
	public string m_EventDesc;

	public abstract string PlayEvent(PlayerData m_Player, string m_EventDesc);
	public abstract string UpdateEvent(PlayerData m_Player, string m_EventDesc);

}
