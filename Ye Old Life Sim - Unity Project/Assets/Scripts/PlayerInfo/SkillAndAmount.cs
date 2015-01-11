using UnityEngine;
using System.Collections;

[System.Serializable]
public class SkillAndAmount
{
    public Skill.Skills m_Skill;
    public float m_Amount;

    public SkillAndAmount(Skill.Skills s, float a)
    {
        m_Skill = s;
        m_Amount = a;
    }
}
