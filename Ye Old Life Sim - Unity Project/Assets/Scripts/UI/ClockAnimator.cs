using UnityEngine;
using System.Collections;

public class ClockAnimator : MonoBehaviour 
{
	public Transform m_ClockHand;
	public PlayerData m_TurnTimer;
	private const float m_Time = 180.0f / 40.0f;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		float clockTime = m_TurnTimer.GetComponent<PlayerData>().m_CurrTime;
		m_ClockHand.localRotation = Quaternion.Euler(0f, 0f, clockTime * -m_Time);
	}
}
