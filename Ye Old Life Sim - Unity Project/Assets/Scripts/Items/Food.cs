using UnityEngine;
using System.Collections;

public class Food : Item 
{
    public bool m_StartTimer = false;       //used to call the UpdateTimer function

    public NavMeshAgent m_NavMeshAgent;     //player NavMeshAgent

    public float m_SwitchTimerOff = 10.0f;  //time you want the UpdateTimer to stop at
    public float m_Modifier = 0.0f;         //scalar to change player speed
    public float m_Time = 0.0f;             //used to keep track of time
    public float m_HungerAmount = 0.0f;     //how much you want to subtract from player hunger

    public Vector3 m_DefaultPlayerSpeedAmount = new Vector3(0.0f, 0.0f, 0.0f);   //default player speed
   

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
        //Get the player's NavmeshAgent and set the speed based on the modifier
        m_NavMeshAgent = player.GetComponent<NavMeshAgent>();
        m_NavMeshAgent.speed += m_NavMeshAgent.speed * m_Modifier;

        m_StartTimer = true;
        player.m_HungerMeter -= m_HungerAmount;
    }

    public override void UpdateTimer()
    {
        //this function increases the m_Time variable for a set amount of time then flips the timer off and resets player speed to its default speed.
        m_Time += Time.deltaTime;

        if (m_Time >= m_SwitchTimerOff)
        {
            //reset the player's speed to its default speed and reset timer
            m_NavMeshAgent.speed = ValueConstants.PLAYER_DEFAULT_SPEED;
            m_Time = 0.0f;
            m_StartTimer = false;
        }
    }
}
