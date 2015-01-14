using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RestartUI : MonoBehaviour 
{
    public GameObject m_RestartGUI; //Building UI element
    public GameManager m_GameManager;
    public Text m_Description;
    public Text m_WhoWon;
    public Text m_ObjectiveScores;
    public PlayerData m_Player;
    public PlayerData m_Opponent;
    public TransitionDisplay m_TransitionDisplay;

    private bool opponentDataSet_ = false;

	// Use this for initialization
	void Start () 
    {
        m_RestartGUI.SetActive(false);
	}

    void Update()
    {
        if (!opponentDataSet_)
        {
            if (m_GameManager.TwoPlayerGame)
            {
                m_Opponent = m_GameManager.m_PlayerTwoData;
                opponentDataSet_ = true;
            }
            else if (m_GameManager.AIGame)
            {
                m_Opponent = m_GameManager.m_AIData;
                opponentDataSet_ = true;
            }
            else if (!m_GameManager.AIGame && !m_GameManager.TwoPlayerGame)
            {
                m_Opponent = null;
                opponentDataSet_ = true;
            }
        }
    }

    public void PlayerDied()
    {
        m_RestartGUI.SetActive(true);
        m_TransitionDisplay.PrepareForFadeIn();
        m_Description.text = "You have decided that it was not a good day to win.";
        if(m_GameManager.TwoPlayerGame)
        {
            m_WhoWon.text = "Enemy has won";
            m_ObjectiveScores.text = "Player's Happiness: " + m_Player.m_Happiness +
                                     "\nPlayer's Shillings " + m_Player.m_Shillings +
                                     "\nPlayer's Reputation " + m_Player.m_Reputation +
                                     "\nOpponent's Happiness: " + m_Opponent.m_Happiness +
                                     "\nOpponent's Shillings " + m_Opponent.m_Shillings +
                                     "\nOpponent's Reputation " + m_Opponent.m_Reputation;
        }
        else
        {
            m_WhoWon.text = "You have lost";
            m_ObjectiveScores.text = "Happiness: " + m_Player.m_Happiness +
                                     "\nShillings " + m_Player.m_Shillings +
                                     "\nReputation " + m_Player.m_Reputation;                            
        }
     
        m_TransitionDisplay.FadeIn();
    }

    public void PlayerWon()
    {
        m_RestartGUI.SetActive(true);
        m_TransitionDisplay.PrepareForFadeIn();
        if (m_GameManager.TwoPlayerGame)
        {
            m_WhoWon.text = "Player has won";
            m_ObjectiveScores.text = "Player's Happiness: " + m_Player.m_Happiness +
                                     "\nPlayer's Shillings " + m_Player.m_Shillings +
                                     "\nPlayer's Reputation " + m_Player.m_Reputation +
                                     "\nOpponent's Happiness: " + m_Opponent.m_Happiness +
                                     "\nOpponent's Shillings " + m_Opponent.m_Shillings +
                                     "\nOpponent's Reputation " + m_Opponent.m_Reputation;
        }
        else
        {
            m_WhoWon.text = "You have won!";
            m_ObjectiveScores.text = "Happiness: " + m_Player.m_Happiness +
                                     "\nShillings " + m_Player.m_Shillings +
                                     "\nReputation " + m_Player.m_Reputation;                              
        }
        m_Description.text = "You have decided that it was not a good day to die.";
       
        m_TransitionDisplay.FadeIn();   
    }
	
    public void RestartGame()
    {
        m_TransitionDisplay.FadeOut(null);
        Application.LoadLevel(1);
    }
}