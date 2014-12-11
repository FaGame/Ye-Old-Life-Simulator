using UnityEngine;
using System.Collections;

public class Elixir : Item 
{
    public NavMeshAgent m_NavMeshAgent;     //player NavMeshAgent

    public bool m_StartTimer = false;       //used to call the UpdateTimer function

    public float m_SwitchTimerOff = 10.0f;  //time you want the UpdateTimer to stop at
    public float m_Time = 0.0f;             //used to keep track of time
    public float m_SpeedModifier = 0.0f;    //scalar to change player speed

    public int m_EarningsModifier = 0;      //modifier that changes the scale of which the player's earnings are calculated 

    void Update()
    {
        if (m_StartTimer)
        {
            UpdateTimer();
        }
    }

    public override void UseItem(PlayerData playerData, PlayerController playerController)
    {
        //Get the player's NavmeshAgent and set the speed based on the modifier
        m_NavMeshAgent = playerController.GetComponent<NavMeshAgent>();
        m_NavMeshAgent.speed += m_NavMeshAgent.speed * m_SpeedModifier;

        //set the player's earnings scalar
        playerData.m_EarningScalar = m_EarningsModifier;
    }

    public override void UpdateTimer()
    {
        //this function increases the m_Time variable for a set amount of time then flips the timer off and resets player speed to its default speed.
        m_Time += Time.deltaTime;

        if (m_Time >= m_SwitchTimerOff)
        {
            //reset the player's speed to its default and reset timer
            m_NavMeshAgent.speed = ValueConstants.PLAYER_DEFAULT_SPEED;
            m_Time = 0.0f;
            m_StartTimer = false;
        }
    }
}
