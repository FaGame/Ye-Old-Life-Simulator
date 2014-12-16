using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RestartUI : MonoBehaviour 
{
    public GameObject m_RestartGUI; //Building UI element

	// Use this for initialization
	void Start () 
    {
        m_RestartGUI.SetActive(false);
	}

    public void PlayerDied()
    {
        m_RestartGUI.SetActive(true);
    }
	
    public void RestartGame()
    {
        Application.LoadLevel(0);
    }
}