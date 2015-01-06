using UnityEngine;
using System.Collections;

public class WeekendJob : RandomEventBaseClass
{
	private Skill SkillTrainedIn_;
	public JobData[] m_JobData;
	public PlayerData pData;
	public JobData jData;

	private int SkillSelect_;

	public override string PlayEvent(PlayerData pData, string tData)
	{
		m_ExperienceGained = Random.Range(ValueConstants.EXPERIENCE_FROM_WEEKEND_JOB_MIN, ValueConstants.EXPERIENCE_FROM_WEEKEND_JOB_MAX);

		SkillSelect_ = Random.Range(ValueConstants.LOWEST_RANGE_NUMBER, pData.m_Skills.Count);
		if(SkillSelect_ != 0)
		{
			pData.m_Skills[SkillSelect_].m_Amount += m_ExperienceGained;
		}
		else
		{
			
		}
		
		


//		SkillSelect_ = Random.Range(1, GetComponent<Skill>().m_Skills.Length);

		//The player gets 5-25 xp in a random skill based upon a randomly selected day job.
//		GetComponent<SkillGain>().ImproveSkill(SkillTrainedIn_, m_ExperienceGained);

		tData = "During your downtime you work a little bit harder and get some extra experience.";
		return tData;
//Finishing this requires being able to increase your skill level
	}
	public override string UpdateEvent(PlayerData m_Player, string m_EventDesc)
	{
		throw new System.NotImplementedException();
	}
	//
}
