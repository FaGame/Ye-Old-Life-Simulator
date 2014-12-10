using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour 
{
    public string m_Name;
    public string[] m_Description;
    public Texture2D m_Image;
    public JobData[] m_JobData;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void Work(PlayerData pData, JobData jData)
    {
        bool isSkillFound = false;

        for (int i = 0; i < jData.m_SkillGain.Length; ++i)
        {
            for (int j = 0; j < pData.m_Skills.Count; ++j)
            {
                if (jData.m_SkillGain[i].m_Skill == pData.m_Skills[j].m_Skill)
                {
                    pData.m_Skills[j].m_Amount += jData.m_SkillGain[i].m_Amount * ValueConstants.WORK_TIME;
                    isSkillFound = true;
                }
                continue;
            }
            if(!isSkillFound)
            {
                pData.m_Skills.Add(new SkillAndAmount(jData.m_SkillGain[i].m_Skill, jData.m_SkillGain[i].m_Amount * ValueConstants.WORK_TIME));
            }
        }
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

        return failedSkill;
    }
}
