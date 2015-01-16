using UnityEngine;
using System.Collections;

public class Training : SpecialEffect
{
    public Skill.Skills m_LearnedSkill;
    public static bool m_HaveLearnedSkill = true;

    public override ResultValue DoSpecialEffect(PlayerData pData)
    {
        ResultValue ret = new ResultValue(ResultCode.Codes.SUCCESS, 0);

        if(pData.m_CurrTime < ValueConstants.UNIVERSITY_TUITION_TIME_COST)
        {
            m_HaveLearnedSkill = false;
            ret.m_Code = ResultCode.Codes.NOT_ENOUGH_TIME;
            ret.m_Value = (int)ValueConstants.UNIVERSITY_TUITION_TIME_COST - (int)pData.m_CurrTime;
            return ret;
        }
        if (pData.m_Shillings < ValueConstants.UNIVERSITY_TUITION_SCHILLING_COST)
        {
            m_HaveLearnedSkill = false;
            ret.m_Code = ResultCode.Codes.NOT_ENOUGH_SCHILLINGS;
            ret.m_Value = ValueConstants.UNIVERSITY_TUITION_SCHILLING_COST - pData.m_Shillings;
            return ret;
        }

        bool isSkillFound = false;
        pData.m_Shillings -= ValueConstants.UNIVERSITY_TUITION_SCHILLING_COST;
        pData.m_CurrTime -= ValueConstants.UNIVERSITY_TUITION_TIME_COST;
        pData.m_HungerMeter += (int)ValueConstants.UNIVERSITY_TUITION_TIME_COST;
        for (int i = 0; i < pData.m_Skills.Count; ++i)
        {
            if(pData.m_Skills[i].m_Skill == m_LearnedSkill)
            {
                isSkillFound = true;
                pData.m_Skills[i].m_Amount += ValueConstants.UNIVERSITY_TUITION_SKILL_GAIN;
                break;
            }
        }
        if(!isSkillFound)
        {
            m_HaveLearnedSkill = true;
            pData.m_Skills.Add(new SkillAndAmount(m_LearnedSkill, (float)ValueConstants.UNIVERSITY_TUITION_SKILL_GAIN));
        }
        ret.m_Value = ValueConstants.UNIVERSITY_TUITION_SKILL_GAIN;

        return ret;
    }

    public override void TurnEnded()
    {

    }
}
