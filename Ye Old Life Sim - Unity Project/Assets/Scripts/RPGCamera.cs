using UnityEngine;
using System.Collections;

public class RPGCamera : MonoBehaviour 
{
    public Transform mTarget;
    public float mHeightOffset;
    public float mDistance;
    public float mMaxDistance;
    public float mMinDistance;
    public float mXSpeed;
    public float mYSpeed;
    public float mYMinLimit;
    public float mYMaxLimit;
    public float mZoomSpeed;
    public float mAutoRotationSpeed = 3.0f;
    public float mAutoZoomSpeed = 5.0f;
    public bool mAlwaysRotateToRearOfTarget = false;
    public bool mAllowMouseInputX = true;
    public bool mAllowMouseInputY = true;

    private float xDeg_ = 0.0f;
    private float yDeg_ = 0.0f;
    private float currentDistance_;
    private float desiredDistance_;
    private float correctedDistance_;
    private bool rotateBehind_ = false;
    private bool mouseSideButton_ = false;

	// Use this for initialization
	void Start () 
    {
        Vector3 angles = transform.eulerAngles;
        xDeg_ = angles.x;
        yDeg_ = angles.y;
        currentDistance_ = mDistance;
        desiredDistance_ = mDistance;
        correctedDistance_ = mDistance;

        if(mAlwaysRotateToRearOfTarget)
        {
            rotateBehind_ = true;
        }
	}

    void LateUpdate()
    {
        if(GUIUtility.hotControl == 0)
        {
            if(Input.GetMouseButton(1))
            {
                if(mAllowMouseInputX)
                {
                    xDeg_ += Input.GetAxis("Mouse X") * mXSpeed * Time.deltaTime;
                    //ClampAngle(xDeg_, 5.0f, 40.0f);
                }
                else
                {
                    RotateBehindTarget();
                }

                if(mAllowMouseInputY)
                {
                    yDeg_ += Input.GetAxis("Mouse Y") * mYSpeed * Time.deltaTime;
                }

                if(!mAlwaysRotateToRearOfTarget)
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

        yDeg_ = ClampAngle(yDeg_, mYMinLimit, mYMaxLimit);

        Quaternion rotation = Quaternion.Euler(yDeg_, xDeg_, 0.0f);

        Vector3 vTargetOffset = new Vector3(0.0f, mHeightOffset, 0.0f);
        Vector3 position = mTarget.position - (rotation * Vector3.forward * desiredDistance_)
                            + vTargetOffset;

        Vector3 trueTargetPosition = mTarget.position + vTargetOffset;
        
        currentDistance_ = Mathf.Clamp(currentDistance_, mMinDistance, mMaxDistance);

        position = mTarget.transform.position - 
            (rotation * Vector3.forward * currentDistance_) + vTargetOffset;

        transform.rotation = rotation;
        transform.position = position;

    }
	
    private void RotateBehindTarget()
    {
        float targetRotationAngle = mTarget.eulerAngles.y;
        float currentRotationAngle = transform.eulerAngles.y;

        xDeg_ = Mathf.LerpAngle(currentRotationAngle, targetRotationAngle, mAutoRotationSpeed
             * Time.deltaTime);

        if(targetRotationAngle == currentRotationAngle)
        {
            if (!mAlwaysRotateToRearOfTarget)
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

	// Update is called once per frame
	void Update () 
    {
	
	}
}
