using UnityEngine;
using System.Collections;

public class KingdomAtWar : RandomEventBaseClass
{
	public static string m_BuildingName;//Probably better to check against the jobs available than the building itself, but this is how it is for now
	private static int EndOfWar_;
	private static GameObject m_GameManager;

	public override string PlayEvent(PlayerData pData, string tData)
	{

		int turnTimer =  GameObject.Find("GameManager Holder").GetComponent<GameManager>().m_Turns; 
		//turnTimer is first assigned the current turn number

		int TurnsUntilWarEnds = Random.Range(ValueConstants.MIN_DAYS_UNTIL_WAR_ENDS, ValueConstants.MAX_DAYS_UNTIL_WAR_ENDS);
		//turns until the end of the war is assigned a random value between two constants

		turnTimer += TurnsUntilWarEnds;
		//turnTimer is increased by the number of turns until the war ends

		EndOfWar_ = turnTimer;
		//The turn the war ends on is assigned the current turn + the number of turns until the war ends

		if (pData.m_Building != null)
		{
			if (pData.m_Building.m_BuildingName == "Barracks")
			{
				pData.m_EarningScalar = ValueConstants.EARNING_SCALAR_CHANGE_FOR_BARRACK_KAW;
			}
			else
			{
				pData.m_EarningScalar = ValueConstants.EARNING_SCALAR_CHANGE_FOR_BUILDINGS_KAW;
			}
		}
		else
		{
			pData.m_EarningScalar = ValueConstants.EARNING_SCALAR_CHANGE_FOR_BUILDINGS_KAW;
		}
		//Money from all jobs (with the exception of the Barracks) now pay half
		//Barracks pay doubles
		tData = "A war has started and the kingdom's funds are being redirected to the war effort, unless you're a soldier, you probably won't be making any money. (Actually right now it's the same.)";
//		tData = "Kingdom At War is active, but does nothing.";
		m_GameManager.GetComponent<GameManager>().m_KAWIsActive = true;
		return tData;
	}

	public static string UpdateEvent(PlayerData pData, string tData)
	{

		if (EndOfWar_ == m_GameManager.GetComponent<GameManager>().m_Turns)
		{
			if (pData.m_Building.m_BuildingName == "Barracks")
			{
				pData.m_EarningScalar = ValueConstants.EARNING_SCALAR_CHANGE_FOR_BARRACK_KAW_ENDS;
			}
			else
			{
				pData.m_EarningScalar = ValueConstants.EARNING_SCALAR_CHANGE_FOR_BUILDINGS_KAW_ENDS;
			}
			tData += " The war has ended. Who won doesn't matter, what matters is all the jobs pay their usual amount again. Probably.";
//			tData = "Kingdom At War is still not working, but if it was stuff would happen now.";
			m_GameManager.GetComponent<GameManager>().m_KAWIsActive = false;
			
		}
		return tData;
		
	}
	//
}
