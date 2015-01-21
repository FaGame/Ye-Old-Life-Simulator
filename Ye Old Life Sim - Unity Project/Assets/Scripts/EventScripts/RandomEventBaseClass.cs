using UnityEngine;
using System.Collections;

public abstract class RandomEventBaseClass 
{
	public static int m_RndEvent;
	public static int m_EventIndex;
	public static int m_MoneyLost;
	public static int m_MoneyGained;
	public static float m_ExperienceGained;
	public static float m_HappinesChange;
	public static float m_TimeChange;

	public PlayerData m_Player;
	public string m_EventDesc;

	public abstract string PlayEvent(PlayerData m_Player, string m_EventDesc);
//	public static string UpdateEvent(PlayerData m_Player, string m_EventDesc){ return m_EventDesc;}

}
