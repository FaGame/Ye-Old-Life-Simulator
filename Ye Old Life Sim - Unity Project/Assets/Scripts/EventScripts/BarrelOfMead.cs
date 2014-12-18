using UnityEngine;
using System.Collections;
using System;

public class BarrelOfMead : RandomEventManager 
{
	private int HangoverTurn_;
	private int currTurn_;

	public void PlayEvent()
	{
		GetComponent<PlayerData>();
		currTurn_ = GetComponent<GameManager>().m_Turns;
		HangoverTurn_ = currTurn_ + 1;

		m_EventText.text = "A barrel of mead falls off a wagon in front of your home. It's gonna be a great night tonight!" ;
	}	

	void Update()
	{
		if(GetComponent<GameManager>().m_Turns == HangoverTurn_)
		{
			m_TimeChange = -15.0f;
			GetComponentInChildren<PlayerData>().m_CurrTime -= m_TimeChange; //Time player loses on their net turn
			//pop up window
			m_EventText.text = "Your hangover has lost you quite a bit of time.";
		}
	}
		

}
