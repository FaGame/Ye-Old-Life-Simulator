using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour 
{
    public string m_Name;
    public string[] m_Description;
    public Texture2D m_Image;
    public JobData[] m_JobData;
    public bool m_PlayerWorksHere;

	// Use this for initialization
	void Start () 
    {
        m_PlayerWorksHere = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void Work(PlayerData pData, JobData jData)
    {
        bool isSkillFound = false;
        float actualWorkTime = pData.m_CurrTime < ValueConstants.WORK_TIME ? pData.m_CurrTime : ValueConstants.WORK_TIME;

        for (int i = 0; i < jData.m_SkillGain.Length; ++i)
        {
            for (int j = 0; j < pData.m_Skills.Count; ++j)
            {
                if (jData.m_SkillGain[i].m_Skill == pData.m_Skills[j].m_Skill)
                {
                    pData.m_Skills[j].m_Amount += jData.m_SkillGain[i].m_Amount * actualWorkTime;
                    isSkillFound = true;
                }
                continue;
            }
            if(!isSkillFound)
            {
                pData.m_Skills.Add(new SkillAndAmount(jData.m_SkillGain[i].m_Skill, jData.m_SkillGain[i].m_Amount * actualWorkTime));
            }
        }
        pData.m_Shillings += (int)(jData.GetWage() * actualWorkTime);
    }

    public void BuildingInteraction()
    {

    }

    public string GetDescription()
    {
        return m_Description[Random.Range(0, m_Description.Length)];
    }

    /*void BuyItem(PlayerData pData, ItemData iData)
    {

    }

    void Interact(PlayerData pData, InteractionData iData)
    {

    }*/

    // null == SUCCESS!  (a good thing.)
    public SkillAndAmount ApplyForJob(PlayerData pData, JobData jData)
    {
        SkillAndAmount failedSkill = null;

        for (int i = 0; i < jData.m_SkillRequirement.Length; ++i)
        {
            for(int j = 0; j < pData.m_Skills.Count; ++j)
            {
                if(jData.m_SkillRequirement[i].m_Skill == pData.m_Skills[j].m_Skill)
                {
                    if(jData.m_SkillRequirement[i].m_Amount > pData.m_Skills[j].m_Amount)
                    {
                        failedSkill.m_Skill = jData.m_SkillRequirement[i].m_Skill;
                        failedSkill.m_Amount = jData.m_SkillRequirement[i].m_Amount - pData.m_Skills[j].m_Amount;
                        return failedSkill;
                    }
                    continue;
                }
            }
        }

        if(failedSkill == null)
        {
            // Remove last job's reputation before adding new job's reputation
            if(pData.m_Job != null)
            {
                pData.m_Reputation -= (int)pData.m_Job.m_ReputationGain;
            }
            pData.m_Job = jData;

            if(pData.m_Building != null)
            {
                pData.m_Building.m_PlayerWorksHere = false;
            }
            m_PlayerWorksHere = true;
            pData.m_Building = this;
            
            
            pData.m_Reputation += (int)jData.m_ReputationGain;
        }

        return failedSkill;
    }
}
