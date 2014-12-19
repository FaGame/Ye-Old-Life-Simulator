using UnityEngine;
using System.Collections;
using System;

public class BarrelOfMead : RandomEventManager 
{
	private int HangoverTurn_;
	private int currTurn_;

	public string PlayEvent(PlayerData pData, string tData)
	{
		GetComponent<PlayerData>();
		currTurn_ = GetComponent<GameManager>().m_Turns;
		HangoverTurn_ = currTurn_ + 1;

		tData = "A barrel of mead falls off a wagon in front of your home. It's gonna be a great night tonight!" ;
		return tData;
	}

	public string Update(PlayerData pData, string tData)
	{
		if(GetComponent<GameManager>().m_Turns == HangoverTurn_)
		{
			m_TimeChange = -15.0f;
			GetComponentInChildren<PlayerData>().m_CurrTime -= m_TimeChange; //Time player loses on their net turn
			//pop up window
			tData = "Your hangover has lost you quite a bit of time.";
			//
		}
		return tData;
		
	}
		

}
