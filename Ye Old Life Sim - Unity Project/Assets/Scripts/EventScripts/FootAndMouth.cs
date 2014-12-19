﻿using UnityEngine;
using System.Collections;

public class FootAndMouth : RandomEventManager
{

	// Use this for initialization
	public override string PlayEvent(PlayerData pData, string tData)
	{
		if(GetComponent<PlayerData>().m_HasMount)
		{
			GetComponent<PlayerData>().m_HasMount = false;
			tData = "Your mount contracted a disease and is now dead.";
		}
		else
		{
			tData = "There's a horse spittin' sickness goin' 'round these parts.";
		}
		return tData;
	}

	public override string UpdateEvent(PlayerData m_Player, string m_EventDesc)
	{
		throw new System.NotImplementedException();
	}
	//
}
