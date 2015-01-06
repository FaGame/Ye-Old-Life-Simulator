using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject m_Player;
    public GameObject m_AI;
    public PlayerData m_AIData;
    public PlayerData m_PlayerData;
    public BuildingUI m_BuildingUI;
    public RestartUI m_RestartUI;

	public int m_Turns;

    private bool isPlayerTurn_;
    private bool isAiTurn_;

    private float maxRep_;
    private float maxCurrency_;
    private float maxHappy_;
    private float maxTurns_;
    private float Players_;
    private float hungerTimer_;
    private int IncrementTurns_ = 1;
    private bool turnsGame_ = false;
    private bool objectivesGame_ = false;
    private bool sandboxGame_ = false;

    public bool PlayerTurn
    {
        get { return isPlayerTurn_; }
    }

    public bool AITurn
    {
        get { return isAiTurn_; }
    }

    void Start()
    {
        isPlayerTurn_ = true;
        isAiTurn_ = false;

        //starts player at current home
        //m_Player.transform.position = m_PlayerData.m_Home.transform.position;

    }

    void Update()
    {
        TurnManager();
        SetObjGame();
        SetTurnsGame();
        CheckForWin();
        CheckForLoss();
        SetGameBools();
        SetPlayers();
        DecreaseTime();
        //Debug.Log(maxHappy_);
        //Debug.Log(maxCurrency_);
        //Debug.Log(maxRep_);
    }

    void TurnManager()
    {
        //Debug.Log(m_PlayerData.m_CurrTime);
        //Debug.Log(m_AIData.m_CurrTime);

        //start turn is the player, once their time runs out, disables player and activates AI and AI turn
        if (isPlayerTurn_ == true)
        {
            // Set in StartTurn(), should only need to be called once
            //m_Player.SetActive(true);

            if (m_PlayerData.m_CurrTime <= 0)
            {
                m_Turns += IncrementTurns_;

                // Set in EndTurn()
                //m_Player.SetActive(false);

                isPlayerTurn_ = false;

                isAiTurn_ = true;

                m_PlayerData.EndTurn();
                m_AIData.StartTurn();
                // Not sure why we would want to start the player's turn when it is ending...
                //m_PlayerData.StartTurn();
            }
        }


        if (isAiTurn_ == true)
        {
            //When player turn is over the AI does the same as the player above
            // Set in StartTurn(), should only need to be called once
            //m_AI.SetActive(true);

            if (m_AIData.m_CurrTime <= 0)
            {
                // Set in EndTurn()
                //m_AI.SetActive(false);

                isAiTurn_ = false;

                isPlayerTurn_ = true;

                m_AIData.EndTurn();
                m_PlayerData.StartTurn();
                // Not sure why we would want to start the AI's turn when it is ending...
                //m_AIData.StartTurn();
            }
        }
    }

    void CheckForObjWin()
    {
        if (m_PlayerData.m_Happiness >= maxHappy_ && m_PlayerData.m_Reputation >= (int)maxRep_ && m_PlayerData.m_Shillings >= (int)maxCurrency_)
        {
            //You Win Objectives gametype, do shit
            Debug.Log("You Win Objectives Game!!");
        }
    }

    void CheckForTurnWin()
    {
       if(m_Turns >= maxTurns_)
       {
           //You Win Turns gametype, do shit
           Debug.Log("You Win Turns Game!!");
       }
    }

    void SetObjGame()
    {
        if(objectivesGame_ == true)
        {
            maxCurrency_ = GameMenu.m_selectedCurrency;
            maxHappy_ = GameMenu.m_selectedHappiness;
            maxRep_ = GameMenu.m_selectedRep;

            CheckForObjWin();
        }
    }

    void SetTurnsGame()
    {
        if(turnsGame_ == true)
        {
            maxTurns_ = GameMenu.m_selectedTurns;

            CheckForTurnWin();
        }
    }

    void CheckForWin()
    {
        int winValue = 2;

        m_PlayerData.CheckScoresBetweenPlayers();

        if (m_PlayerData.m_BetterCategoryCounter >= winValue)
        {   
            m_RestartUI.PlayerWon();
        }
    }

    void CheckForLoss()
    {
        int lossAmount = 2;
        m_AIData.CheckScoresBetweenPlayers();

        if(m_PlayerData.m_Happiness <= 0.0f)
        {
            //commit suicide
        }

        if(m_PlayerData.m_Shillings <= 0)
        {
            //take up prostitution
        }

        if(m_PlayerData.m_Reputation <= 0)
        {
            //drink your woes away in obscurity, try some narcotics
        }

        if (m_PlayerData.m_HungerMeter >= m_PlayerData.m_MaxHunger || m_Player.GetComponent<PlayerData>().m_IsDead || m_AIData.m_BetterCategoryCounter >= lossAmount)
        {
            Debug.Log("You have died, alone and forgotten behind the most uninspiring shrubbery");      
            m_RestartUI.PlayerDied();
        }
    }

    void SetGameBools()
    {
        turnsGame_ = GameMenu.m_isTurnGame;
        objectivesGame_ = GameMenu.m_isObjectivesGame;
        sandboxGame_ = GameMenu.m_isSandboxGame;
    }

    void SetPlayers()
    {
        Players_ = GameMenu.m_Players;
    }

    //Use this DecreaseTime function for movement only
    public void DecreaseTime()
    {
        if (m_PlayerData.m_CurrTime != 0.0f)
        {
            if (m_Player.GetComponent<PlayerController>().m_IsMoving && isPlayerTurn_ == true)
            {
                m_PlayerData.m_CurrTime -= Time.deltaTime;
                hungerTimer_ += Time.deltaTime;
                if (hungerTimer_ >= 1.0f)
                {
                    m_PlayerData.m_HungerMeter += 1.0f;
                    hungerTimer_ = 0.0f;
                }
            }
            if (m_AI.GetComponent<PlayerController>().m_IsMoving && isAiTurn_ == true)
            {
                m_AIData.m_CurrTime -= Time.deltaTime;
                hungerTimer_ += Time.deltaTime;
                if (hungerTimer_ >= 1.0f)
                {
                    m_AIData.m_HungerMeter += 1.0f;
                    hungerTimer_ = 0.0f;
                }
            }
        }
    }

    //Use this DecreaseTime for UI functions, to control the amount of time is decremented on each call
    public void DecreaseTime(float timeToDecrease)
    {
        if (m_PlayerData.m_CurrTime != 0.0f)
        {
            m_PlayerData.m_CurrTime -= timeToDecrease;
        }
    }
}
