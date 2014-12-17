using UnityEngine;
using System.Collections;

public class ThePlague : RandomEventManager
{
	public void PlayEvent()
	{
		GetComponent<PlayerData>().m_IsInfected = true;

		m_EventText.text = "You've caught the plague.";
	}
}
