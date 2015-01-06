using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour 
{
    public float m_minFov = 15.0f;
    public float m_maxFov = 90.0f;
    public float sensitivity = 10.0f;
 
    void Update () 
    {
       float fov = Camera.main.fieldOfView;
       fov -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
       fov = Mathf.Clamp(fov, m_minFov, m_maxFov);
       Camera.main.fieldOfView = fov;
    }
	
}
