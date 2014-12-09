using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour 
{
    public Camera mCamera;
    public Terrain mTerrain;

    private Vector3 currTarget_;
    private bool atCurrTarget_;
    private Animator animator_;
    private NavMeshAgent navAgent_;
    private float lastRot_;

    public void SetTarget(Vector3 target)
    {
        currTarget_ = target;
        atCurrTarget_ = false;
        navAgent_.SetDestination(target);
    }

    // Use this for initialization
    void Start()
    {
        currTarget_ = Vector3.zero;
        atCurrTarget_ = true;
        animator_ = GetComponent<Animator>();
        navAgent_ = GetComponent<NavMeshAgent>();
        lastRot_ = transform.rotation.eulerAngles.y;

    }

    void Update()
    {
        UpdateAnimation();

        Ray pickingRay = mCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit rayData;

        if (Input.GetMouseButtonDown(0))
        {
            int layerMask = ~LayerMask.GetMask("Terrain");
            if (Physics.Raycast(pickingRay, out rayData, 3000.0f, layerMask))
            {
                
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            if (transform != null)
            {
                int layerMask = LayerMask.GetMask("Terrain");
                Physics.Raycast(pickingRay, out rayData, 3000.0f, layerMask);
                SetTarget(rayData.point);
            }
        }
    }

    private void UpdateAnimation()
    {
        float speedRatio = navAgent_.velocity.magnitude / navAgent_.speed;
        float angularSpeed = transform.rotation.eulerAngles.y - lastRot_;
        lastRot_ = transform.rotation.eulerAngles.y;
        animator_.SetFloat("speed", speedRatio);
        animator_.SetFloat("xDir", 0.0f);
        animator_.SetFloat("yDir", speedRatio);
        animator_.SetFloat("rotation", Mathf.Min(1.0f, Mathf.Abs(angularSpeed / (navAgent_.angularSpeed * Time.deltaTime))));
        animator_.SetFloat("rotDir", Mathf.Clamp(angularSpeed / (navAgent_.angularSpeed * Time.deltaTime), -1.0f, 1.0f));
    }
}
