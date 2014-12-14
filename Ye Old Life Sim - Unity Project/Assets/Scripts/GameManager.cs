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

    private float maxRep_;
    private float maxCurrency_;
    private float maxHappy_;
    private float maxTurns_;
    private int Turns_;
    private int IncrementTurns_ = 1;
    private bool turnsGame_ = false;
    private bool objectivesGame_ = false;
    private bool sandboxGame_ = false;

    void Start()
    {
        m_PlayerData = m_EmptyObjPlayer.GetComponent<PlayerData>();
        m_AIData = m_EmptyObjAI.GetComponent<PlayerData>();

        isPlayerTurn_ = true;
        isAiTurn_ = false;

        GameMenu.m_isTurnGame = turnsGame_;
        GameMenu.m_isSandboxGame = sandboxGame_;
        GameMenu.m_isObjectivesGame = objectivesGame_;

        //starts player at current home
        //m_Player.transform.position = m_PlayerData.m_Home.transform.position;

    }

    void Update()
    {
        TurnManager();
        SetObjGame();
        SetTurnsGame();
    }

    void TurnManager()
    {
        //Debug.Log(m_PlayerData.m_CurrTime);
        //Debug.Log(m_AIData.m_CurrTime);
        //start turn is the player, once their time runs out, disables player and activates AI and AI turn
        if (isPlayerTurn_ == true)
        {
            m_Player.SetActive(true);

            if (m_PlayerData.m_CurrTime <= 0)
            {
                Turns_ += IncrementTurns_;

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

    void CheckForObjWin()
    {
        if (m_PlayerData.m_Happiness >= maxHappy_ && m_PlayerData.m_Reputation >= (int)maxRep_ && m_PlayerData.m_Shillings >= (int)maxCurrency_)
        {
            //You Win Objectives gametype, do shit
            Debug.Log("You Win!!");
        }
    }

    void CheckForTurnWin()
    {
       if(Turns_ >= maxTurns_)
       {
           //You Win Turns gametype, do shit
           Debug.Log("You Win!!");
       }
    }

    void SetObjGame()
    {
        if(objectivesGame_ == true)
        {
            maxCurrency_ = GameMenu.m_selectedCurrency;
            maxHappy_ = GameMenu.m_selectedHappiness;
            maxRep_ = GameMenu.m_selectedRep;
        }

        CheckForObjWin();
    }

    void SetTurnsGame()
    {
        if(turnsGame_ == true)
        {
            maxTurns_ = GameMenu.m_selectedTurns;
        }

        CheckForTurnWin();
    }
}
