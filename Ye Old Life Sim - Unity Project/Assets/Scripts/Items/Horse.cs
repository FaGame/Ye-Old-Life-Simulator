using UnityEngine;
using System.Collections;

public class Horse : Item 
{
    public ItemEffect m_Effect;
    public bool m_BoughtHorse;
    public float m_PlayerSpeedModifier = 0.0f;

    public AudioSource m_HorseGallop;

	void Start () 
    {
        m_BoughtHorse = false;

        //set the player's speed effect values
        m_Effect.m_Type = ItemEffect.EffectType.SPEED;
        m_Effect.m_Timer = 0.0f;
        m_Effect.m_Value = m_PlayerSpeedModifier;
	}
	
	void Update () 
    {

	}

    public override void UseItem(PlayerData playerData)
    {
        if (m_BoughtHorse)
        {
            return;
        }
        else
        {
            m_BoughtHorse = true;
            playerData.AddMountSpeed(m_Effect);
            playerData.GetComponent<PlayerController>().ActivateHorse();
        }
    }
}
