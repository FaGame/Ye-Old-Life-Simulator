using UnityEngine;
using System.Collections;

public class BitByRats : RandomEventManager 
{
	public void PlayEvent()
	{
		//pop up window
		m_EventText.text = "Woken up in the middle of the night by rats biting you. Looks like a late start when you get up in the morning.";
		GetComponentInChildren<PlayerData>().m_CurrTime -= 10.0f;
	}
}
