using UnityEngine;
using System.Collections;

public class WeekendJob : RandomEventManager
{
	private Skill SkillTrainedIn_;
	public JobData[] m_JobData;
	public PlayerData pData;
	public JobData jData;

	private int SkillSelect_;

	public void PlayEvent()
	{
		m_ExperienceGained = Random.Range(5, 26);

		SkillSelect_ = Random.Range(1, GetComponent<PlayerData>().m_Skills.Count);
		if(SkillSelect_ == 0)
		{
			return;
		}
		else
		{
			pData.m_Skills[SkillSelect_].m_Amount += m_ExperienceGained;
		}
		
		


//		SkillSelect_ = Random.Range(1, GetComponent<Skill>().m_Skills.Length);

		//The player gets 5-25 xp in a random skill based upon a randomly selected day job.
//		GetComponent<SkillGain>().ImproveSkill(SkillTrainedIn_, m_ExperienceGained);

		m_EventText.text = "During your downtime you work a little bit harder and get some extra experience.";

//Finishing this requires being able to increase your skill level
	}
	//
}
