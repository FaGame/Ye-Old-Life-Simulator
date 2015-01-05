using UnityEngine;
using System.Collections;

public class RailCam2 : MonoBehaviour 
{
    public PlayerController m_Player;
    public Transform m_Target;
    public Transform m_CentreTarget;
    public float m_ZOffset;
    public float m_adjustedDistance;
    public float m_RotAmount;
    public float m_YSpeed;
    public float m_YMinLimit;
    public float m_YMaxLimit;
    public float m_Distance;
    public bool m_RotateY = false;

    private float yawSpeed_;
    private float yDeg_ = 0.0f;
    private float toPlayer_ = 0.0f;
    private Vector3 CamPos_;

    void Start()
    {
        yawSpeed_ = 0.0f;
    }

    void LateUpdate()
    {
        //transform.position = new Vector3(m_Target.position.x, m_Target.position.y + m_YOffset, m_Target.position.z + m_ZOffset);
        //Mathf.Clamp(m_Target.position.z, -15.0f, -15.0f);
        UpdateCamRotation();

       /* toPlayer_ = m_Target.position.z - transform.position.z;

        if (toPlayer_ > m_ZOffset)
        {
            CamPos_ = new Vector3(transform.position.x, transform.position.y, transform.position.z - m_adjustedDistance);
        }

        transform.position = CamPos_;*/

    }

    void UpdateCamRotation()
    {
        transform.LookAt(m_Target);

        if (m_Player.m_IsMoving == true)
        {
            transform.RotateAround(m_CentreTarget.position, Vector3.up, m_Distance * Time.deltaTime);
        }
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
