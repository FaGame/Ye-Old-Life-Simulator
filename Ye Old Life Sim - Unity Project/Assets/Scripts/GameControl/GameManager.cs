using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject m_Player;
    public GameObject m_PlayerTwo;
    public GameObject m_AI;
	public GameObject m_RandomEventManagerP1;
	public GameObject m_RandomEventManagerP2;
	public GameObject m_RandomEventManagerAI;
	public RandomEventBaseClass m_RandomEventUpdater;

	public BarrelOfMead m_BOMUpdater;
	public KingdomAtWar m_KAWUpdater;

	public bool m_KAWIsActive = false;
	public bool m_BOMIsActive = false;

    public PlayerData m_AIData;
    public PlayerData m_PlayerData;
    public PlayerData m_PlayerTwoData;
    public BuildingUI m_BuildingUI;
    public RestartUI m_RestartUI;
    public DataCollection m_DataCollection;
    public CloseAllOpenMenus closeAllOpenMenus_;



	public int m_Turns;

    public bool m_CanCheckWinLoss = true;       //set to true when you want to check the win/loss conditions 
	public bool m_RandomEventP1Play = true;
	public bool m_RandomEventP2Play = false;
	public bool m_RandomEventAIPlay = false;

	private bool isPlayerTurn_;
    private bool isPlayerTwoTurn_;
    private bool isAiTurn_;

    private float maxRep_ = 1000.0f;
    private float maxCurrency_ = 1000.0f;
    private float maxHappy_ = 1000.0f;
    private float maxTurns_;
    private float Players_;
    private float hungerTimer_;
    private int IncrementTurns_ = 1;
    private bool turnsGame_ = false;
    private bool objectivesGame_ = false;
    private bool sandboxGame_ = false;

    private bool AIBeingUsed_ = false;
    private bool TwoPlayerGame_ = false;
    private bool SetActivePlayersBool = false;

	public string StringPlaceHolder_ = "";

    public bool PlayerTurn
    {
        get { return isPlayerTurn_; }
    }

    public bool PlayerTwoTurn
    {
        get { return isPlayerTwoTurn_; }
    }

    public bool AITurn
    {
        get { return isAiTurn_; }
    }

    public float MaxRep
    {
        get { return maxRep_; }
    }

    public float MaxCurrency
    {
        get { return maxCurrency_; }
    }

    public float MaxHappy
    {
        get { return maxHappy_; }
    }

    public bool TwoPlayerGame
    {
        get { return TwoPlayerGame_; }
    }

    public bool AIGame
    {
        get { return AIBeingUsed_; }
    }

    public enum WinCodes
    {
        DEFAULT = 0,
        PLAYER_WINS = 1,
        OPPONENT_WINS = 2
    };

    void Start()
    {
        isPlayerTurn_ = true;
        isPlayerTwoTurn_ = false;
        SetActivePlayersBool = false;
        isAiTurn_ = false;
        //check to see if the AI is being used
        AIBeingUsed_ = GameMenu.m_AIBeingUsed;
        //check to see if it is a two player game
        TwoPlayerGame_ = GameMenu.m_TwoPlayerGame;

        //starts player at current home
        //m_Player.transform.position = m_PlayerData.m_Home.transform.position;

    }

    void Update()
    {
        if(!SetActivePlayersBool)
        {
            SetActivePlayers();
            SetActivePlayersBool = true;
        }

        TurnManager();
        SetObjGame();
        SetTurnsGame();

        if (turnsGame_)
        {
            CheckForTurnWin();
        }
        else if(objectivesGame_)
        {
            CheckForObjWin();
        }

        if (m_CanCheckWinLoss)
        {
            //CheckForWin();
            CheckForLoss();
        }   

        SetGameBools();
        SetPlayers();
        DecreaseTime();

        if(m_PlayerData.m_HungerMeter >= ValueConstants.PLAYER_MAX_HUNGER)
        {
            KillPlayer();
        }
    }

    void SetActivePlayers()
    {
            m_AIData.EndTurn();
            m_PlayerTwoData.EndTurn();
    }

    void TurnManager()
    {
        //start turn is the player, once their time runs out, disables player and activates AI and AI turn
        if (isPlayerTurn_ == true)
        {
			if(m_RandomEventP1Play) //Check to see if the RandomEvent has already been played
			{
				//if not already playerd, Play a Random Event
				m_RandomEventManagerP1.GetComponent<RandomEventManager1>().PlayRandomEvent();
				string eventText = "";

//				m_RandomEventUpdater.UpdateEvent(m_PlayerData, eventText);
				if(m_KAWIsActive)
				{
//					m_KAWUpdater.GetComponentInChildren<GameObject>().UpdateEvent(m_PlayerData, eventText);
//					m_KAWUpdater.UpdateEvent(m_PlayerData, eventText);
				}
				if(m_BOMIsActive)
				{
//					m_BOMUpdater.UpdateEvent(m_PlayerData, eventText);
//					m_BOMUpdater.GetComponent<GameObject>().
//					m_BarrelOfMeadUpdater.UpdateEvent(m_PlayerData, eventText);
				}
				
				//Make sure that another random event doesn't play this turn
				m_RandomEventP1Play = false;
			}

            if (m_PlayerData.m_CurrTime <= 0)
            {
                closeAllOpenMenus_.CloseAll();
                m_Turns += IncrementTurns_;

                if(TwoPlayerGame_)
                {
                    //if it is a two player game, end the first persons turn and start the next person's turn
                    isPlayerTurn_ = false;
                    isPlayerTwoTurn_ = true;
                    m_PlayerData.EndTurn();					
                    m_PlayerTwoData.StartTurn();
					m_RandomEventP2Play = true; //Set it so P2 will get a random event at the start of their turn
                }
                else if (AIBeingUsed_)
                {
                    //if the AI is being used, end the player's turn and start the AI's turn
                    isPlayerTurn_ = false;
                    isAiTurn_ = true;
                    m_PlayerData.EndTurn();
                    m_AIData.StartTurn();
					m_RandomEventAIPlay = true; //Set it the AI will get a random event at the start of their turn
                }
                else
                {
                     //if it is a single player game, start the player's next turn
                     //m_DataCollection.AddTurns();
                     //m_DataCollection.PopulateStats();
                     m_PlayerData.EndTurn();
					 m_RandomEventP1Play = true;
                     m_PlayerData.StartTurn();
					 
                }
            }
        }
 
        if (isAiTurn_ == true)
        {
			if (m_RandomEventAIPlay) //Check to play Random Event just once a turn for the AI
			{
				m_RandomEventManagerAI.GetComponent<RandomEventManagerAI>().PlayRandomEvent();
				m_RandomEventAIPlay = false;
			}

            if (m_AIData.m_CurrTime <= 0)
            {
                //end the AI's turn and start the player's turn
                isPlayerTurn_ = true;
                isAiTurn_ = false;

                m_AIData.EndTurn();
                m_PlayerData.StartTurn();
				m_RandomEventP1Play = true; //Set up so P1 will Get a Random event at the start of their turn
            }
        }

		if (isPlayerTwoTurn_ == true) //Check to play Random Event just once a turn for P2
        {
			if (m_RandomEventP2Play)
			{
				m_RandomEventManagerP2.GetComponent<RandomEventManager2>().PlayRandomEvent();
				m_RandomEventP2Play = false;
			}
            if (m_PlayerTwoData.m_CurrTime <= 0)
            {			
                //end the second player's turn and start the player's turn
                isPlayerTurn_ = true;
                isPlayerTwoTurn_ = false;
                m_PlayerTwoData.EndTurn();
                m_PlayerData.StartTurn();
				m_RandomEventP1Play = true; //Set up so P1 will Get a Random event at the start of their turn
            }
        }
    }

    void CheckForObjWin()
    {
        if(AIBeingUsed_ || TwoPlayerGame_)
        {
            if(m_PlayerData.CheckScoresBetweenPlayers() == WinCodes.PLAYER_WINS)
            {
                m_RestartUI.PlayerWon();
            }
            else if(m_PlayerData.CheckScoresBetweenPlayers() == WinCodes.OPPONENT_WINS)
            {
                m_RestartUI.PlayerLost();
            }
        }
    }


    void CheckForTurnWin()
    {
        if (AIBeingUsed_ || TwoPlayerGame_)
        {
            if (m_Turns >= maxTurns_)
            {
                if (m_PlayerData.CheckScoresBetweenPlayers() == WinCodes.PLAYER_WINS)
                {
                    //You Win Turns gametype, do shit
                    m_RestartUI.PlayerWon();
                    Debug.Log("You Win Turns Game!!");
                }
                else if (m_PlayerData.CheckScoresBetweenPlayers() == WinCodes.OPPONENT_WINS)
                {
                    m_RestartUI.PlayerLost();
                }
            }
        }
        else if(m_Turns >= maxTurns_)
        {
            m_RestartUI.PlayerWon();
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
    }

    void SetTurnsGame()
    {
        if(turnsGame_ == true)
        {
            maxTurns_ = GameMenu.m_selectedTurns;
        }
    }

    /*void CheckForWin()
    {
        int winValue = 3;

        m_PlayerData.CheckScoresBetweenPlayers();

        if (m_PlayerData.m_BetterCategoryCounter >= winValue)
        {   
            m_RestartUI.PlayerWon();
            m_CanCheckWinLoss = false;
        }
    }*/

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
            m_RestartUI.PlayerLost();
            m_CanCheckWinLoss = false;
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
        if (m_PlayerTwoData.m_CurrTime != 0.0f)
        {
            if (m_PlayerTwo.GetComponent<PlayerController>().m_IsMoving && isPlayerTwoTurn_ == true)
            {
                m_PlayerTwoData.m_CurrTime -= Time.deltaTime;
                hungerTimer_ += Time.deltaTime;
                if (hungerTimer_ >= 1.0f)
                {
                    m_PlayerTwoData.m_HungerMeter += 1.0f;
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

    void KillPlayer()
    {
        m_PlayerData.m_IsDead = true;
    }
}
