using UnityEngine;
using System.Collections;

public class RailCam2 : MonoBehaviour 
{
    public Transform m_Target;
    public Transform m_CentreTarget;
    public float m_YOffset;
    public float m_Distance;
    public GameManager m_GameManager;

    private float Zero_ = 0.0f;

    void Update()
    {
        if (m_GameManager.AITurn)
        {
            m_Target = GameObject.Find("Player Two").transform;
        }
        else if (m_GameManager.PlayerTurn)
        {
            m_Target = GameObject.Find("Char_2").transform;
        }
        else if (m_GameManager.PlayerTwoTurn)
        {
            m_Target = GameObject.Find("Player Two").transform;
        }
    }

    void LateUpdate()
    {
        Vector3 toPlayer_ = m_Target.position - m_CentreTarget.position;
        toPlayer_.y = Zero_;
        toPlayer_ = toPlayer_.normalized;
        transform.position = new Vector3(m_CentreTarget.position.x + toPlayer_.x * m_Distance, m_Target.position.y + m_YOffset, 
                                        m_CentreTarget.position.z + toPlayer_.z * m_Distance);

        UpdateCamRotation();

    }
    
    void UpdateCamRotation()
    {
        transform.LookAt(m_Target);
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360.0f)
        {
            angle += 360.0f;
        }

        if (angle > 360.0f)
        {
            angle -= 360.0f;
        }

        return Mathf.Clamp(angle, min, max);
    }
}
