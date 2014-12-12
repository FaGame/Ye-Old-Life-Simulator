using UnityEngine;
using System.Collections;

public class JobData : MonoBehaviour 
{
    public SkillAndAmount[] m_SkillRequirement;
    public SkillAndAmount[] m_SkillGain;

    public string m_JobDescription;
    public float m_Wage;
    public float m_MinWage;
    public float m_MaxWage;
    public float m_ReputationGain;

	void Start () 
    {
        //m_MinWage = 0;
       // m_MaxWage = 0;
	}

    //public function setting the wage and returning it
    public float GetWage()
    {
        //if the wage has a min and max value inputed, it selecets a random number between them
        if(m_MinWage != m_MaxWage)
        {
            m_Wage = Random.Range(m_MinWage, m_MaxWage);
        }
        return m_Wage;
    }
}
