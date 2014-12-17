using UnityEngine;
using System.Collections;

public class FootAndMouth : RandomEventManager
{

	// Use this for initialization
	void Start () 
	{
		if(GetComponent<PlayerData>().m_HasMount)
		{
			GetComponent<PlayerData>().m_HasMount = false;
			m_EventText.text = "Your mount contracted a disease and is now dead.";
		}
		else
		{
			m_EventText.text = "";
		}
	}
}
