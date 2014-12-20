using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RestartUI : MonoBehaviour 
{
    public GameObject m_RestartGUI; //Building UI element
    public Text m_Description;
    public Text m_WhoWon;
    public Text m_ObjectiveScores;
    public PlayerData m_Player;
    public PlayerData m_Opponent;

	// Use this for initialization
	void Start () 
    {
        m_RestartGUI.SetActive(false);
	}

    public void PlayerDied()
    {
        m_Description.text = "You have decided that it was not a good day to die.";
        m_WhoWon.text = "Enemy has won";
        m_ObjectiveScores.text = "Player's Happiness: " + m_Player.m_Happiness +
                                 "\nPlayer's Shillings " + m_Player.m_Shillings +
                                 "\nPlayer's Reputation " + m_Player.m_Reputation +
                                 "\nOpponent's Happiness: " + m_Opponent.m_Happiness +
                                 "\nOpponent's Shillings " + m_Opponent.m_Shillings +
                                 "\nOpponent's Reputation " + m_Opponent.m_Reputation;
        m_RestartGUI.SetActive(true);
    }

    public void PlayerWon()
    {
        m_Description.text = "You have decided that it was a good day to win.";
        m_WhoWon.text = "Player has won";
        m_ObjectiveScores.text = "Player's Happiness: " + m_Player.m_Happiness +
                                 "\nPlayer's Shillings " + m_Player.m_Shillings +
                                 "\nPlayer's Reputation " + m_Player.m_Reputation +
                                 "\nOpponent's Happiness: " + m_Opponent.m_Happiness +
                                 "\nOpponent's Shillings " + m_Opponent.m_Shillings +
                                 "\nOpponent's Reputation " + m_Opponent.m_Reputation;

        m_RestartGUI.SetActive(true);
    }
	
    public void RestartGame()
    {
        Application.LoadLevel(0);
    }
}