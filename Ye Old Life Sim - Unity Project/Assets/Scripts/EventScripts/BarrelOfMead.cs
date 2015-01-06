using UnityEngine;
using System.Collections;
using System;

public class BarrelOfMead : RandomEventBaseClass 
{
	private int HangoverTurn_;

	public GameObject m_GameManager;

	public override string PlayEvent(PlayerData pData, string tData)
	{
//		GetComponent<PlayerData>();
		int currTurn = m_GameManager.GetComponent<GameManager>().m_Turns;
		HangoverTurn_ = currTurn + ValueConstants.HANGOVER_DAY;

		tData = "A barrel of mead falls off a wagon in front of your home. It's gonna be a great night tonight!" ;
		return tData;
	}

	public override string UpdateEvent(PlayerData pData, string tData)
	{
		if (m_GameManager.GetComponent<GameManager>().m_Turns == HangoverTurn_)
		{
			m_TimeChange = ValueConstants.TIME_CHANGE_FROM_HANGOVER;
			pData.m_CurrTime -= m_TimeChange; //Time player loses on their net turn
			//pop up window
			tData = "Your hangover has lost you quite a bit of time.";
		}
		return tData;
		
	}
		

}
