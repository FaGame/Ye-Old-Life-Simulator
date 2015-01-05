using UnityEngine;
using System.Collections;

public class RailCamera : MonoBehaviour 
{
    public Transform m_Target;
    public float m_ZOffset;
    public float m_YOffset;
    public float m_RotAmount;
    public float m_YSpeed;
    public float m_YMinLimit;
    public float m_YMaxLimit;
    public float m_Distance;

    private float yawSpeed_;
    private float yDeg_ = 0.0f;
   
    void Start()
    {
        yawSpeed_ = 0.0f;
        Vector3 angles = transform.eulerAngles;
        yDeg_ = angles.y;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(m_Target.position.x, m_Target.position.y + m_YOffset, m_Target.position.z + m_ZOffset);
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
