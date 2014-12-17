using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class REvntScript : MonoBehaviour 
{
	public Canvas m_EventScroll; //Player's HUD canvas
	public Text m_EventText; //Text that displays when a radom event occurs
	public GameObject[] m_EventList;

	public float m_EventTextTimer;

	public REvntScript[] eventsText_;
    
    public void EventFunction()
    {
        string name = gameObject.name;
        //get prefab name
        
        //make a case statement, if this name then this function/find script


    }

	// Use this for initialization
	void Start () 
	{
			
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
