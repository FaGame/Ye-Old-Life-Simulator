using UnityEngine;
using System.Collections;

public class Food : Item 
{
    public bool m_StartTimer = false;       //used to call the UpdateTimer function
    public bool m_SlowDownPlayer = false;   //set to true if you want the player speed to decrease

    public NavMeshAgent m_NavMeshAgent;     //player NavMeshAgent

    public float m_Time = 0.0f;             //used to keep track of time
    public float m_HungerAmount = 0.0f;     //how much you want to subtract from player hunger

    public Vector3 m_DefaultPlayerSpeedAmount = new Vector3(0.0f, 0.0f, 0.0f);   //default player speed
    public Vector3 m_DecreaseSpeedAmount = new Vector3(0.0f, 0.0f, 0.0f);       //how slow you want the player to move
    public Vector3 m_IncreaseSpeedAmount = new Vector3(0.0f, 0.0f, 0.0f);       //how fast you want the player to move

    void Update()
    {
        if(m_StartTimer)
        {
            UpdateTimer();
        }
    }

    //when the function is called subtract a value from the hunger meter and increase or decrease player speed
    public override void UseItem(PlayerData player)
    {
        //Get the player's NavmeshAgent
        m_NavMeshAgent = player.GetComponent<NavMeshAgent>();

        //if you want the player to slow down or speed up change the bool 'm_SlowDownPlayer'
        if(m_SlowDownPlayer)
        {
            m_NavMeshAgent.velocity = m_DecreaseSpeedAmount;
        }
        else
        {
            m_NavMeshAgent.velocity = m_IncreaseSpeedAmount;
        }
        m_StartTimer = true;
        player.m_HungerMeter -= m_HungerAmount;
    }

    public override void UpdateTimer()
    {
        //this function increases the m_Time variable for a set amount of time then flips the timer off and resets player speed to its default speed.
        float switchTime = 10.0f;
        m_Time += Time.deltaTime;

        if(m_Time >= switchTime)
        {
            //reset the player's speed to its default and reset timer
            m_NavMeshAgent.velocity = m_DefaultPlayerSpeedAmount;
            m_Time = 0.0f;
            m_StartTimer = false;
        }
    }
}
