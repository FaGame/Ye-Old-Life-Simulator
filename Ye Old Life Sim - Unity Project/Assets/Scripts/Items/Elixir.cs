using UnityEngine;
using System.Collections;

public class Elixir : Item 
{
    public PlayerData m_Enemy;                              //this is what the enemy will be assigned to
    public ItemEffect m_PlayerSpeedEffect;                  //effect for player's speed
    public ItemEffect m_PlayerEarningsEffect;               //effect for player's earnings 
    public ItemEffect m_EnemySpeedEffect;                   //effeect for enimies speed
    public ItemEffect m_EnemyEarningEffect;                 //effect for enimies earnings

    public float m_PlayerSpeedTimer = 0.0f;                 //Player's speed timer
    public float m_PlayerEarningsTimer = 0.0f;              //Player's earning timer
    public float m_EnemySpeedTimer = 0.0f;                  //Enemies speed timer  
    public float m_EnemyEarningsTimer = 0.0f;               //Enemies earnings timer  
    public float m_PlayerSpeedModifier = 0.0f;              //scalar to change player speed
    public float m_EnemySpeedModifier = 0.0f;               //scalar to change the enemy speed
    public float m_PlayerEarningsModifier = 1.0f;           //modifier that changes the scale of which the player's earnings are calculated 
    public float m_EnemyEarningModifier = 1.0f;             //modifier that changes the scale of which the enemies earnings are calculated 

    void Start()
    {
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
        //uses the AddEffect function which sets all values based on the ItemEffect specified 
        playerData.AddEffect(m_PlayerSpeedEffect);
        playerData.AddEffect(m_PlayerEarningsEffect);

        m_Enemy.AddEffect(m_EnemySpeedEffect);
        m_Enemy.AddEffect(m_EnemyEarningEffect);
    }

}
