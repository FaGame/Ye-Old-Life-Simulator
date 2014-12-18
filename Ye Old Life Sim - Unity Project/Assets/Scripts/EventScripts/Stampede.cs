using UnityEngine;
using System.Collections;

public class Stampede : RandomEventManager
{
	
	public void PlayEvent()
	{
		m_TimeChange = -10.0f;

		GetComponent<PlayerData>().m_CurrTime += m_TimeChange;

		m_EventText.text = "A stampede is running towards you, and you cleverly decide to RUN. After running for a while, you are now able to go where you like. "
						+ " .";
		//Place the player in a random place on the map and make them lose 10 seconds of their turn.
	}
}
