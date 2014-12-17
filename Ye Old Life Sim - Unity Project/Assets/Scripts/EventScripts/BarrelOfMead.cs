using UnityEngine;
using System.Collections;
using System;

public class BarrelOfMead : RandomEventManager 
{
	private int HangoverTurn_;
	private int currTurn_;
	private float HangoverTime_;

	private bool ShowText_ = false;
	private bool TextCondition_ = true;
	private float CurrentTime_ = 0.0f;
	private float ExecutedTime_ = 0.0f;
	private float TimeToWait_ = 5.0f;

	public void PlayEvent()
	{
		GetComponent<PlayerData>();
		HangoverTime_ = GetComponentInChildren<PlayerData>().m_CurrTime;
		currTurn_ = GetComponentInChildren<GameManager>().m_Turns;
		HangoverTurn_ = currTurn_ + 1;

		CurrentTime_ = Time.time;
		if(TextCondition_)
		{
			ShowText_ = true;
		}
		else
		{
			ShowText_ = false;
		}

		if(ExecutedTime_ != 0.0f)
		{
			if(CurrentTime_ - ExecutedTime_ > TimeToWait_)
			{
				ExecutedTime_ = 0.0f;
				TextCondition_ = false;
			}
		}
		//pop up window
//		StartCoroutine(ShowMessage("A barrel of mead falls off a wagon in front of your home.", 3));
//		IEnumerator (string text, float delay)
//		{

//		}

		//You found a barrel of mead that fell off of a wagon.
		//You bring it home and plan to drink it tonight
		
		//changes the player speed talk to Brandon
	}

	void OnGui()
	{
		if(ShowText_)
		{
			GUI.Label(new Rect(0, 0, 100, 100), "A barrel of mead falls off a wagon in front of your home.");
		}
	}

	

	void Update()
	{
		if(currTurn_ == HangoverTurn_)
		{
			//pop up window
			//Your hangover has lost you quite a bit of time.
//			HangoverTime_ -= 15.0f;
			GetComponentInChildren<PlayerData>().m_CurrTime -= 15.0f;

			Debug.Log(HangoverTime_);
		}
	}
		

}
