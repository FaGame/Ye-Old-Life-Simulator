using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Building : MonoBehaviour 
{
    public PlayerData m_Player;

    public string m_BuildingName;
    public string[] m_Description;
    public Texture2D m_Image;
    public JobData[] m_JobData;
    public Item[] m_Items;
    public SpecialEffect[] m_SpecialEffects;
    public bool m_PlayerWorksHere;
    public BuildingUI m_BuildingUI;

    private bool isHighlighted_;
    private Renderer[] renderers_;
    private List<List<Color>> originalColours_ = new List<List<Color>>();

    public enum Buildings
    {
        BLACKSMITH = 0,
        TAVERN = 1,
        CHURCH = 2,
        CASTLE = 3,
        SEXSHOP = 4
    };
    public Buildings m_buildingEnum;

	// Use this for initialization
	void Start () 
    {
        m_PlayerWorksHere = false;
        renderers_ = GetComponentsInChildren<Renderer>();

        for (int i = 0; i < renderers_.Length; ++i)
        {
            originalColours_.Add(new List<Color>());
            for (int j = 0; j < renderers_[i].renderer.materials.Length; ++j)
            {
                originalColours_[i].Add(renderers_[i].renderer.materials[j].color);
            }
            //originalColours_.Add(renderers_[i].renderer.material.color);        
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public float Work(PlayerData pData, JobData jData)
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
        pData.m_CurrTime -= actualWorkTime;
        pData.m_Shillings += (int)(jData.GetWage() * actualWorkTime);
        AudioSource.PlayClipAtPoint(jData.m_AudioClip, pData.transform.position);
        return actualWorkTime;
    }

    public void BuildingInteraction()
    {

    }

    public string GetDescription()
    {
        return m_Description[Random.Range(0, m_Description.Length)];
    }

    public void BuyItem(PlayerData pData, Item iData)
    {
        if (pData.RemoveSchillings(iData.m_Cost))
        {
            if (iData.GetComponent<Elixir>() || iData.GetComponent<Food>())
            {
                pData.m_UseableInventory.AddToInventory(iData.name, iData.m_ItemEntryData);
            }
            else if (iData.GetComponent<Possession>())
            {
                pData.m_PossessionInventory.AddToInventory(iData.name, iData.m_ItemEntryData);
            }
        }
    }

    /*void Interact(PlayerData pData, InteractionData iData)
    {

    }*/

    // null == SUCCESS!  (a good thing.)
    public SkillAndAmount ApplyForJob(PlayerData pData, JobData jData)
    {
        SkillAndAmount failedSkill = null;

        // If there is a skill requirement and the player has no skill, they are an idiot.
        if((jData.m_SkillRequirement.Length > 0) && (pData.m_Skills.Count == 0))
        {
            failedSkill = new SkillAndAmount(jData.m_SkillRequirement[0].m_Skill, jData.m_SkillRequirement[0].m_Amount);
            return failedSkill;
        }

        for (int i = 0; i < jData.m_SkillRequirement.Length; ++i)
        {
            for(int j = 0; j < pData.m_Skills.Count; ++j)
            {
                if((jData.m_SkillRequirement[i].m_Skill == pData.m_Skills[j].m_Skill) && (jData.m_SkillRequirement[i].m_Amount > pData.m_Skills[j].m_Amount))
                {
                    failedSkill = new SkillAndAmount(jData.m_SkillRequirement[i].m_Skill, jData.m_SkillRequirement[i].m_Amount);
                    return failedSkill;
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

    void OnMouseEnter()
    {
        for (int i = 0; i < GetComponentsInChildren<Renderer>().Length; ++i)
        {
            for (int j = 0; j < GetComponentsInChildren<Renderer>()[i].renderer.materials.Length; ++j)
            {
                GetComponentsInChildren<Renderer>()[i].renderer.materials[j].color = Color.yellow;
            }
            //m_DisplayedBuilding.GetComponentsInChildren<Renderer>()[i].renderer.material.color = Color.yellow;
        }
    }

    void OnMouseExit()
    {
        for (int i = 0; i < GetComponentsInChildren<Renderer>().Length; ++i)
        {
           for (int j = 0; j < renderers_[i].renderer.materials.Length; ++j)
            {
                GetComponentsInChildren<Renderer>()[i].renderer.materials[j].color = originalColours_[i][j];
                //originalColours_[i].Add(renderers_[i].renderer.materials[j].color);
            }
            //m_DisplayedBuilding.GetComponentsInChildren<Renderer>()[i].renderer.material.color = originalColours_[i];
        }
    }
}
