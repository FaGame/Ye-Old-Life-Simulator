using UnityEngine;
using System.Collections;

public class KingdomAtWar : RandomEventManager
{
	public string m_BuildingName;//Probably better to check against the jobs available than the building itself, but this is how it is for now.
	public JobData[] m_JobData;
	public PlayerData pData;
	public JobData jData;

	private int EndOfWar_;

	public void PlayEvent()
	{
		int TurnsUntilWarEnds = Random.Range(1, 6);
		EndOfWar_ = GetComponent<GameManager>().m_Turns += TurnsUntilWarEnds;

		//Money from all jobs (with the exception of the Barracks) now pay half

		if (m_BuildingName == "BARRACKS")
		{
			GetComponent<JobData>().m_Wage *= 2;
		}
		
		else
		{
			GetComponent<JobData>().m_Wage *= (int)0.5;
		}

		//Barracks pay doubles
		m_EventText.text = "A war has started and the kingdom's funds are being redirected to the war effort, unless you're a soldier, you probably won't be making any money.";
	}

	public void Update()
	{
		if(GetComponent<GameManager>().m_Turns == EndOfWar_)
		{
			if (m_BuildingName == "BARRACKS")
			{
				GetComponent<JobData>().m_Wage *= (int)0.5;
			}

			else
			{
				GetComponent<JobData>().m_Wage *= 2;
			}
			m_EventText.text = "The war has ended. Who won doesn't matter, what matters is all the jobs pay their usual amount again. Probably.";

		}
	}
}
