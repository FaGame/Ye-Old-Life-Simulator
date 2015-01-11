using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float m_TimeToWait;
    private Text text_;

	// Use this for initialization
	void Start() 
    {
	}
	
	// Update is called once per frame
	void LateUpdate () 
    {
       Wait(text_);
	}
    
    public void Wait(Text text)
    { 
        if (m_TimeToWait > 0)
        {
            m_TimeToWait -= Time.deltaTime;
            Debug.Log("Waiting" + m_TimeToWait);
        }
        if(m_TimeToWait == 0)
        {
            text.enabled = false;
        }
    }
}
