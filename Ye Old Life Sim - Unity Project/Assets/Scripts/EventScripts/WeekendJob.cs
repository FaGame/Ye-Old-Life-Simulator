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

		bool isSkillFound = false;

		for (int i = 0; i < jData.m_SkillGain.Length; ++i)
		{
			for (int j = 0; j < pData.m_Skills.Count; ++j)
			{
				if (jData.m_SkillGain[i].m_Skill == pData.m_Skills[j].m_Skill)
				{
					pData.m_Skills[j].m_Amount += jData.m_SkillGain[i].m_Amount = m_ExperienceGained; //really not sure if this is right
					isSkillFound = true;
				}
				continue;
			}
			if (!isSkillFound)
			{
				pData.m_Skills.Add(new SkillAndAmount(jData.m_SkillGain[i].m_Skill, jData.m_SkillGain[i].m_Amount = m_ExperienceGained)); //same here
			}
		}


//		SkillSelect_ = Random.Range(1, GetComponent<Skill>().m_Skills.Length);

		//The player gets 5-25 xp in a random skill based upon a randomly selected day job.
//		GetComponent<SkillGain>().ImproveSkill(SkillTrainedIn_, m_ExperienceGained);

		m_EventText.text = "During your downtime you work a little bit harder and get some extra experience.";

//Finishing this requires being able to increase your skill level
	}
}
