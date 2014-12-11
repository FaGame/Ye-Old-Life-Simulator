using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
    public GameObject m_Player;
    public GameObject m_EmptyObjPlayer;
    public GameObject m_EmptyObjAI;
    public GameObject m_AI;
    public PlayerData m_AIData;
    public PlayerData m_PlayerData;

    private bool isPlayerTurn_;
    private bool isAiTurn_;

    void Start()
    {
        m_PlayerData = m_EmptyObjPlayer.GetComponent<PlayerData>();
        m_AIData = m_EmptyObjAI.GetComponent<PlayerData>();

        isPlayerTurn_ = true;
        isAiTurn_ = false;

        //starts player at current home
        //m_Player.transform.position = m_PlayerData.m_Home.transform.position;

    }

    void Update()
    {
        //Debug.Log(m_PlayerData.m_CurrTime);
        //Debug.Log(m_AIData.m_CurrTime);
        //start turn is the player, once their time runs out, disables player and activates AI and AI turn
        if(isPlayerTurn_ == true)
        {
            m_Player.SetActive(true);

            if (m_PlayerData.m_CurrTime <= 0)
            {
                
                m_Player.SetActive(false);
                
                isPlayerTurn_ = false;

                isAiTurn_ = true;

                m_PlayerData.StartTurn();
            }
        }


        if (isAiTurn_ == true)
        {
            //When player turn is over the AI does the same as the player above
            m_AI.SetActive(true);

            if (m_AIData.m_CurrTime <= 0)
            {
                m_AI.SetActive(false);

                isAiTurn_ = false;

                isPlayerTurn_ = true;

                m_AIData.StartTurn();
            }
        }
    }
}
