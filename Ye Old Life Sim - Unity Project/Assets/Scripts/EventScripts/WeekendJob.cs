using UnityEngine;
using System.Collections;

public class WeekendJob : RandomEventManager
{

	public void PlayEvent()
	{
		//The player gets 5-25 xp in a random skill based upon a randomly selected day job.
		m_EventText.text = ".";
	}
}
