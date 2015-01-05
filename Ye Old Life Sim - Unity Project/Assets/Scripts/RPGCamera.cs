using UnityEngine;
using System.Collections;

public class RPGCamera : MonoBehaviour 
{
    public Transform m_Target;
    public float m_HeightOffset;
    public float m_Distance;
    public float m_MaxDistance;
    public float m_MinDistance;
    public float m_XSpeed;
    public float m_YSpeed;
    public float m_YMinLimit;
    public float m_YMaxLimit;
    public float m_ZoomSpeed;
    public float m_AutoRotationSpeed = 3.0f;
    public float m_AutoZoomSpeed = 5.0f;
    public float m_RotAngle;
    public bool m_AlwaysRotateToRearOfTarget = false;
    public bool m_AllowMouseInputX = true;
    public bool m_AllowMouseInputY = true;

    private float xDeg_ = 0.0f;
    private float yDeg_ = 0.0f;
    private float currentDistance_;
    private float desiredDistance_;
    private float correctedDistance_;
    private bool rotateBehind_ = false;
    private bool mouseSideButton_ = false;

    public float m_minFov = 15.0f;
    public float m_maxFov = 90.0f;
    public float m_Sensitivity = 10.0f;

    private float fov_;
 

	// Use this for initialization
	void Start () 
    {
        Vector3 angles = transform.eulerAngles;
        xDeg_ = angles.x;
        yDeg_ = angles.y;
        currentDistance_ = m_Distance;
        desiredDistance_ = m_Distance;
        correctedDistance_ = m_Distance;

        if(m_AlwaysRotateToRearOfTarget)
        {
            rotateBehind_ = true;
        }

        m_Target.Rotate(Vector3.right, m_RotAngle);
	}

    void LateUpdate()
    {
        UpdateFovZoom();
        if(GUIUtility.hotControl == 0)
        {
            if(Input.GetMouseButton(1))
            {
                if(m_AllowMouseInputX)
                {
                    xDeg_ += Input.GetAxis("Mouse X") * m_XSpeed * Time.deltaTime;
                }
                else
                {
                    RotateBehindTarget();
                }

                if(m_AllowMouseInputY)
                {
                    yDeg_ += Input.GetAxis("Mouse Y") * m_YSpeed * Time.deltaTime;
                }

                if(!m_AlwaysRotateToRearOfTarget)
                {
                    rotateBehind_ = false;
                }
            }
            else if(Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0
                    || rotateBehind_ || mouseSideButton_)
            {
                RotateBehindTarget();
            }
        }

        yDeg_ = ClampAngle(yDeg_, m_YMinLimit, m_YMaxLimit);

        Quaternion rotation = Quaternion.Euler(yDeg_, xDeg_, 0.0f);

        Vector3 vTargetOffset = new Vector3(0.0f, m_HeightOffset, 0.0f);
        Vector3 position = m_Target.position - (rotation * Vector3.forward * desiredDistance_)
                            + vTargetOffset;

        Vector3 trueTargetPosition = m_Target.position + vTargetOffset;
        
        currentDistance_ = Mathf.Clamp(currentDistance_, m_MinDistance, m_MaxDistance);

        position = m_Target.transform.position - 
            (rotation * Vector3.forward * currentDistance_) + vTargetOffset;

        transform.rotation = rotation;
        transform.position = position;

        transform.Rotate(Vector3.right, m_RotAngle);

    }
	
    private void RotateBehindTarget()
    {
        float targetRotationAngle = m_Target.eulerAngles.y;
        float currentRotationAngle = transform.eulerAngles.y;

        xDeg_ = Mathf.LerpAngle(currentRotationAngle, targetRotationAngle, m_AutoRotationSpeed
             * Time.deltaTime);

        if(targetRotationAngle == currentRotationAngle)
        {
            if (!m_AlwaysRotateToRearOfTarget)
            {
                rotateBehind_ = false;
            }
        }
        else
        {
            rotateBehind_ = true;
        }

    }


    private float ClampAngle(float angle, float min, float max)
    {
        if(angle < -360.0f)
        {
            angle += 360.0f;
        }

        if (angle > 360.0f)
        {
            angle -= 360.0f;
        }

        return Mathf.Clamp(angle, min, max);
    }

    void UpdateFovZoom()
    {
        fov_ = Camera.main.fieldOfView;
        fov_ -= Input.GetAxis("Mouse ScrollWheel") * m_Sensitivity;
        fov_ = Mathf.Clamp(fov_, m_minFov, m_maxFov);
        Camera.main.fieldOfView = fov_;
    }
}
