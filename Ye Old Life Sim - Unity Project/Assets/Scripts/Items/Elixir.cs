using UnityEngine;
using System.Collections;

public class Elixir : Item 
{
    public PlayerData m_Enemy;                              //this is what the enemy will be assigned to
    public ItemEffect m_PlayerSpeedEffect;                  //effect for player's speed
    public ItemEffect m_PlayerEarningsEffect;               //effect for player's earnings 
    public ItemEffect m_EnemySpeedEffect;                   //effeect for enimies speed
    public ItemEffect m_EnemyEarningEffect;                 //effect for enimies earnings

    public float m_PlayerTimeSpeedScalar = 1.0f;                //used to scale the the player's speed timer based on elixir
    public float m_EnemyTimeSpeedScalar = 1.0f;                 //used to scale the the enemy speed timer based on elixir
    public float m_PlayerTimeEarningsScalar = 1.0f;         //used to scale the the player's earnings timer based on elixir
    public float m_EnemyTimeEarningsScalar = 1.0f;          //used to scale the the enemy earnings timer based on elixir
    public float m_PlayerSpeedTimer = 0.0f;                 //Player's speed timer
    public float m_EnemySpeedTimer = 0.0f;                  //Enemies speed timer 
    public float m_PlayerEarningsTimer = 0.0f;              //Player's earning timer
    public float m_EnemyEarningsTimer = 0.0f;               //Enemies earnings timer  
    public float m_PlayerSpeedModifier = 0.0f;              //modifier to change player speed
    public float m_EnemySpeedModifier = 0.0f;               //modifier to change the enemy speed
    public float m_PlayerEarningsModifier = 1.0f;           //modifier that changes the scale of which the player's earnings are calculated 
    public float m_EnemyEarningModifier = 1.0f;             //modifier that changes the scale of which the enemies earnings are calculated 

    public override string GetDescription()
    {
        string retval = "";

        if(m_PlayerSpeedModifier == 0)
        {
            retval = "";
        }

        return ""; 
    }

    void Start()
    {
        //scales the player and enemy speed timers based on the scalar value
        m_PlayerSpeedTimer = ValueConstants.PLAYER_MAX_TIME * m_PlayerTimeSpeedScalar;
        m_EnemySpeedTimer = ValueConstants.PLAYER_MAX_TIME * m_EnemyTimeSpeedScalar;

        //scales the player and enemy earnings timers based on the scalr value
        m_PlayerEarningsTimer = ValueConstants.PLAYER_MAX_TIME * m_PlayerTimeEarningsScalar;
        m_EnemyEarningsTimer = ValueConstants.PLAYER_MAX_TIME * m_EnemyTimeEarningsScalar;

        //set the player's speed effect values
        m_PlayerSpeedEffect.m_Type = ItemEffect.EffectType.SPEED;
        m_PlayerSpeedEffect.m_Timer = m_PlayerSpeedTimer;
        m_PlayerSpeedEffect.m_Value = m_PlayerSpeedModifier;

        //set the player's earning effect values
        m_PlayerEarningsEffect.m_Type = ItemEffect.EffectType.INCOME_MODIFIER;
        m_PlayerEarningsEffect.m_Timer = m_PlayerEarningsTimer;
        m_PlayerEarningsEffect.m_Value = m_PlayerEarningsModifier;

        //set the enimies speed effect values
        m_EnemySpeedEffect.m_Type = ItemEffect.EffectType.SPEED;
        m_EnemySpeedEffect.m_Timer = m_EnemySpeedTimer;
        m_EnemySpeedEffect.m_Value = m_EnemySpeedModifier;

        //set the enimies earning effect values
        m_EnemyEarningEffect.m_Type = ItemEffect.EffectType.INCOME_MODIFIER;
        m_EnemyEarningEffect.m_Timer = m_EnemyEarningsTimer;
        m_EnemyEarningEffect.m_Value = m_EnemyEarningModifier;
    }

    public override void UseItem(PlayerData playerData)
    {
        if (m_UseCount != 0)
        {
            m_UseCount--;  //subtract 1 from the count of uses
            //uses the AddEffect function which sets all values based on the ItemEffect specified 
            playerData.AddEffect(m_PlayerSpeedEffect);
            playerData.AddEffect(m_PlayerEarningsEffect);

            m_Enemy.AddEffect(m_EnemySpeedEffect);
            m_Enemy.AddEffect(m_EnemyEarningEffect);
        }
    }
}
