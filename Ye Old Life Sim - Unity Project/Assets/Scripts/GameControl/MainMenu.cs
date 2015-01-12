using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
  
    public void StartNewGame()
    {
        Debug.Log("Load New Game");
        Application.LoadLevel("NewGameMenu");
    }

    public void LoadGame()
    {
        Debug.Log("Load Saved Game");
    }

    public void CloseGame()
    {
        Application.Quit();
        Debug.Log("Close Game");
    }

}