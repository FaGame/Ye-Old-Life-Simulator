using UnityEngine;
using System.Collections;

public class Stampede : RandomEventBaseClass
{
	private Vector3 m_PlayerPos;
	private float m_XPos;
	private float m_ZPos;

	public override string PlayEvent(PlayerData pData, string tData)
	{
		m_TimeChange = ValueConstants.TIME_CHANGE_FROM_STAMPEDE;

		pData.m_CurrTime += m_TimeChange;

		m_XPos = Random.Range(ValueConstants.XPOS_CHANGE_MIN, ValueConstants.XPOS_CHANGE_MAX);
		m_ZPos = Random.Range(ValueConstants.ZPOS_CHANGE_MIN, ValueConstants.ZPOS_CHANGE_MAX);

		m_PlayerPos = new Vector3(m_Player.rigidbody.position.x + m_XPos, m_Player.rigidbody.position.y, m_Player.rigidbody.position.z + m_ZPos);

		tData = "A stampede is running towards you, and you cleverly decide to RUN. After running for a while, you are now able to go where you like. "
						+ " .";
		//Place the player in a random place on the map and make them lose 10 seconds of their turn.
		return tData;
	}

	public override string UpdateEvent(PlayerData m_Player, string m_EventDesc)
	{
		throw new System.NotImplementedException();
	}
	//
}
