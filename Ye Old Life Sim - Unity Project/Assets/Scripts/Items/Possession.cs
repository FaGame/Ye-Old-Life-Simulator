using UnityEngine;
using System.Collections;

public class Possession : MonoBehaviour 
{
    public int m_InitialCost = 0;
    public int m_ResellValue = 0;
    public int m_ReputationBonus = 0;

    public SkillAndAmount m_Skill;

    public float m_SkillAmount = 0.0f;
    
    void Start()
    {
        m_Skill.m_Amount = m_SkillAmount;
        
    }
}
