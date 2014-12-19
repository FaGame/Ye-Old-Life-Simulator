using UnityEngine;
using System.Collections;

public class KingdomAtWar : RandomEventManager
{
//	public string m_BuildingName;//Probably better to check against the jobs available than the building itself, but this is how it is for now.
	public JobData[] m_JobData;
	public JobData jData;

	public Building m_Building;

	private int EndOfWar_;

	public string PlayEvent(PlayerData pData, string tData)
	{
		int TurnsUntilWarEnds = Random.Range(1, 6);
		EndOfWar_ = GetComponent<GameManager>().m_Turns += TurnsUntilWarEnds;

		//Money from all jobs (with the exception of the Barracks) now pay half

		if(GetComponent<PlayerData>().m_Building.m_BuildingName == "Barracks")
		{
			GetComponent<PlayerData>().m_EarningScalar = 2;
		}
		else
		{
			GetComponent<PlayerData>().m_EarningScalar = (int)0.5;
		}

		//Barracks pay doubles
		tData = "A war has started and the kingdom's funds are being redirected to the war effort, unless you're a soldier, you probably won't be making any money.";
		return tData;
	}

	/*public string Update(string tData)
	{
		if(GetComponent<PlayerData>().m_Building.m_BuildingName == "Barracks")
		{
			GetComponent<PlayerData>().m_EarningScalar = 1;
		}
		else
		{
			GetComponent<PlayerData>().m_EarningScalar = 1;
		}
		tData = "The war has ended. Who won doesn't matter, what matters is all the jobs pay their usual amount again. Probably.";
		return tData;
		
	}*/
	//
}
