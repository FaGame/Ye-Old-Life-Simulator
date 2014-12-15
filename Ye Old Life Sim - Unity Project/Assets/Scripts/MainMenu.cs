using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public void StartNewGame()
    {
        Debug.Log("Load New Game");
    }

    public void LoadGame()
    {
        Debug.Log("Load Saved Game");
    }

    public void CloseGame()
    {
        Debug.Log("Close Game");
    }

}
