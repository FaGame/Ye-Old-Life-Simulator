using UnityEngine;
using System.Collections;

public class Food : Item 
{
    public float m_HungerAmount;
    public float m_DecreaseSpeedAmount;
    public float m_IncreaseSpeedAmount;

    public override void UseItem(PlayerData player)
    {
        player.GetComponent<NavMeshAgent>();
 
        player.m_HungerMeter -= m_HungerAmount;
    }
}
