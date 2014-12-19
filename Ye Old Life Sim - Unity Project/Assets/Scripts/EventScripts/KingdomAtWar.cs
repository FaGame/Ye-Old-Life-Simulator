using UnityEngine;
using System.Collections;

public class KingdomAtWar : RandomEventManager
{
//	public string m_BuildingName;//Probably better to check against the jobs available than the building itself, but this is how it is for now.
	public JobData[] m_JobData;
	public JobData jData;

	public Building m_Building;

	private int EndOfWar_;

	public override string PlayEvent(PlayerData pData, string tData)
	{
		int TurnsUntilWarEnds = Random.Range(ValueConstants.MIN_DAYS_UNTIL_WAR_ENDS, ValueConstants.MAX_DAYS_UNTIL_WAR_ENDS);
		EndOfWar_ = GetComponent<GameManager>().m_Turns += TurnsUntilWarEnds;

		//Money from all jobs (with the exception of the Barracks) now pay half

		if(GetComponent<PlayerData>().m_Building.m_BuildingName == "Barracks")
		{
			GetComponent<PlayerData>().m_EarningScalar = ValueConstants.EARNING_SCALAR_CHANGE_FOR_BARRACK_KAW;
		}
		else
		{
			GetComponent<PlayerData>().m_EarningScalar = ValueConstants.EARNING_SCALAR_CHANGE_FOR_BUILDINGS_KAW;
		}

		//Barracks pay doubles
		tData = "A war has started and the kingdom's funds are being redirected to the war effort, unless you're a soldier, you probably won't be making any money.";
		return tData;
	}

	public override string UpdateEvent(PlayerData pData, string tData)
	{
		if(GetComponent<PlayerData>().m_Building.m_BuildingName == "Barracks")
		{
			GetComponent<PlayerData>().m_EarningScalar = ValueConstants.EARNING_SCALAR_CHANGE_FOR_BARRACK_KAW_ENDS;
		}
		else
		{
			GetComponent<PlayerData>().m_EarningScalar = ValueConstants.EARNING_SCALAR_CHANGE_FOR_BUILDINGS_KAW_ENDS;
		}
		tData = "The war has ended. Who won doesn't matter, what matters is all the jobs pay their usual amount again. Probably.";
		return tData;
		
	}
	//
}
