using UnityEngine;
using System.Collections;

public class Stampede : RandomEventManager
{
	private Vector3 m_PlayerPos;
	private float m_XPos;
	private float m_ZPos;

	public string PlayEvent(PlayerData pData, string tData)
	{
		m_TimeChange = -10.0f;

		GetComponent<PlayerData>().m_CurrTime += m_TimeChange;

		m_XPos = Random.Range(-15, 26);
		m_ZPos = Random.Range(-15, 26);

		m_PlayerPos = new Vector3(m_Player.rigidbody.position.x + m_XPos, m_Player.rigidbody.position.y, m_Player.rigidbody.position.z + m_ZPos);

		tData = "A stampede is running towards you, and you cleverly decide to RUN. After running for a while, you are now able to go where you like. "
						+ " .";
		//Place the player in a random place on the map and make them lose 10 seconds of their turn.
		return tData;
	}
	//
}
