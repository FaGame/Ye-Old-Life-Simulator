using UnityEngine;
using System.Collections;

public class GameMenuCam : MonoBehaviour {

    public Camera m_mainCamera;

   
	// Use this for initialization
	void Start () 
    {
       
	}
	
	// Update is called once per frame
	void Update () 
    {
        m_mainCamera.transform.Rotate(0.0f, Time.deltaTime * 4.0f, 0.0f, Space.World);
	}
}
