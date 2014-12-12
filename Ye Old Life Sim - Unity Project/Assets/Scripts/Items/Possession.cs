using UnityEngine;
using System.Collections;

public class Possession : MonoBehaviour 
{
    public PlayerData m_Player;

    public int m_InitialCost = 0;               //how much the item costs to buy
    public int m_ResellValue = 0;               //how much the item can be re-sold at
    public int m_ReputationBonus = 0;           //how much reputation bonus the player gets 
    public int m_SkillValueNeeded = 0;          //hopw much skill the player neeeds to use the bonus for that item 

    public SkillAndAmount m_Skill;              //sets what skill and amount that item is for 
    public Skill.Skills m_SkillNeeded;          //determines what skill is needed for the item to be used by the player

    public float m_HappinessAmount = 0.0f;      //how much of a happpiness increase is applied to the player 

    public string m_AquirredFrom = "";          //Just used to say who the item was aquirred from
    public string m_UsedBy = "";                //Who the item is used by 

    void Start()
    {
        ApplyValueChange();
    }

    public void ApplyValueChange()
    {
        //increases the player's happiness by the happiness variable
        m_Player.m_Happiness += m_HappinessAmount;

        //checks if the player's skill matches the skill needed and that the player's skill amount for that job matches the skill value needed then increases reputation
        if(m_Skill.m_Skill == m_SkillNeeded && m_Skill.m_Amount >= m_SkillValueNeeded)
        {
            m_Player.m_Reputation += m_ReputationBonus;
        }
    }
}
