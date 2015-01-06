using UnityEngine;
using System.Collections;

public class ThePlague : RandomEventBaseClass
{
	public override string PlayEvent(PlayerData pData, string tData)
	{
		pData.m_IsInfected = true;

		tData = "You've caught the plague.";
		return tData;
	}
	public override string UpdateEvent(PlayerData m_Player, string m_EventDesc)
	{
		throw new System.NotImplementedException();
	}
	//
}
