using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Possession : Item 
{
    public PlayerData m_Player;

    public int m_ReputationBonus = 0;           //how much reputation bonus the player gets 
    public int m_SkillValueNeeded = 0;          //hopw much skill the player neeeds to use the bonus for that item 

    public Skill.Skills m_SkillNeeded;          //determines what skill is needed for the item to be used by the player

    public float m_HappinessAmount = 0.0f;      //how much of a happpiness increase is applied to the player 

    public string m_AquirredFrom = "";          //Just used to say who the item was aquirred from
    public string m_UsedBy = "";                //Who the item is used by 

    private List<SkillAndAmount> skills_;       //sets what skill and amount that item is for 

    void Start()
    {
        skills_ = m_Player.m_Skills;    //assigns the skills variable to the player's skills
        ApplyValueChange();
    }

    public void ApplyValueChange()
    {
        //increases the player's happiness by the happiness variable
        m_Player.m_Happiness += m_HappinessAmount;

        //checks if one of the player's skills matches the skill needed and that the player's skill amount for that job matches the skill value needed then increases reputation
        for (int i = 0; i < skills_.Count; ++i)
        {
            if (skills_[i].m_Skill == m_SkillNeeded && skills_[i].m_Amount >= m_SkillValueNeeded)
            {
                m_Player.m_Reputation += m_ReputationBonus;
            }
        }     
    }

    public override void UseItem(PlayerData playerData) {}
}
