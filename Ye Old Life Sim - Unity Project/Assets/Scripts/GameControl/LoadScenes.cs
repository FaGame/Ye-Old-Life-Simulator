using UnityEngine;
using System.Collections;

public class LoadScenes : MonoBehaviour 
{

	public void LoadMainMenu()
    {
        Application.LoadLevel(0);
    }

    public void LoadGame()
    {
        Application.LoadLevel(1);
    }
}
