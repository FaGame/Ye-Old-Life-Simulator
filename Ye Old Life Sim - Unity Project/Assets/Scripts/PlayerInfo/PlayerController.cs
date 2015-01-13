using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour 
{
    public AudioSource m_PlayerRunning;
    public Horse m_Horse;
    public Animator m_HorseAnimator;
    public Animator m_PlayerAnimator;

    public PlayerData m_PlayerData;     //object to hold the player data, if that wasn't obvious 
    public Camera mCamera;
    public Terrain mTerrain;
    public Transform mblackSmithWaypoint;
    public GameObject m_WaypointObject;
    public bool m_IsMoving;
	public float m_v;                   //1 = walk animiation
                                        //0 = idle animation
                                        //TODO: Make Enum

    private Vector3 currTarget_;
    private bool atCurrTarget_;
    private NavMeshAgent navAgent_;
    private float lastRot_;
    private float hungerTimer_;
    private Animator animator_;
    private ProjectM projM_;

    public void SetTarget(Vector3 target)
    {
        currTarget_ = target;
        atCurrTarget_ = false;
        navAgent_.SetDestination(target);
    }

    // Use this for initialization
    void Start()
    {
        animator_ = GetComponent<Animator>();
        navAgent_ = GetComponent<NavMeshAgent>();
        currTarget_ = Vector3.zero;
        atCurrTarget_ = true;
        lastRot_ = transform.rotation.eulerAngles.y;
        TargetReached();
        projM_ = Camera.main.GetComponent<ProjectM>();
        //SetTarget(m_PlayerData.transform.position);
    }
	
	public void TargetReached()
    {
        //currTarget_ = Vector3.zero;
        atCurrTarget_ = true;
        lastRot_ = transform.rotation.eulerAngles.y;
        m_WaypointObject = null;
    }

    void OnDisable()
    {
        m_IsMoving = false;
    }

    void Update()
    {
        UpdateAnimation();

        Ray pickingRay = mCamera.ScreenPointToRay(Input.mousePosition);

        //RaycastHit rayData;

        RaycastHit hitInfo = new RaycastHit();
        bool hit = Physics.Raycast(pickingRay, out hitInfo);
        if (hit)
        {
            if (hitInfo.transform.gameObject.tag == "Building")
            {
                projM_.TurnOn(hitInfo.transform.gameObject);
            }
            else
            {
                //Debug.Log("OFF: " + hitInfo.transform.gameObject.name + " TAG: " + hitInfo.transform.gameObject.tag);
                projM_.TurnOffAll();
            }
        }

        if (Input.GetMouseButtonUp(0) && hit)
        {
            /*if (transform != null)
            {
                int layerMask = LayerMask.GetMask("Terrain");
                Physics.Raycast(pickingRay, out rayData, 3000.0f, layerMask);
                //SetTarget(rayData.point);
            }*/

            //if (hit)
            //{
				m_WaypointObject = hitInfo.transform.gameObject;
                if (m_WaypointObject.tag == "Building")
                {
                    Debug.Log("It's working!");
                    GotoBuilding(m_WaypointObject);
                    m_PlayerData.ReopenMenu(hitInfo.transform.gameObject);
                }
            //}
        }

        DecreaseTime();
		/*if(!atCurrTarget_)
        {
            GetDistenceToWaypoint(waypointObject_);
        }*/
		
        if(navAgent_.velocity.magnitude > 0.0f)
        {
            m_v = 1;
        }
        else
        {
            m_v = 0;
        }
    
        navAgent_.speed = m_PlayerData.GetComponent<PlayerData>().m_Speed;

        UpdateWalkSound();
    }

    public void GotoBuilding(GameObject goHere)
    {
        Transform[] posEs = goHere.GetComponentsInChildren<Transform>();
        foreach (Transform tForm in posEs)
        {
            if (tForm.CompareTag("Waypoint"))
            {
                Debug.Log("Found the waypoint.");
                SetTarget(tForm.position);
                break;
            }
        }
    }

    private void UpdateAnimation()
    {
        //float speedRatio = navAgent_.velocity.magnitude / navAgent_.speed;
        //float angularSpeed = transform.rotation.eulerAngles.y - lastRot_;
        //lastRot_ = transform.rotation.eulerAngles.y;
        //animator_.SetFloat("speed", speedRatio);
        //animator_.SetFloat("xDir", 0.0f);
        //animator_.SetFloat("yDir", speedRatio);
        //animator_.SetFloat("rotation", Mathf.Min(1.0f, Mathf.Abs(angularSpeed / (navAgent_.angularSpeed * Time.deltaTime))));
        //animator_.SetFloat("rotDir", Mathf.Clamp(angularSpeed / (navAgent_.angularSpeed * Time.deltaTime), -1.0f, 1.0f));

        //---------------------------------------------------------------------------- test code ---------------------------------------------------------------------------------
        //m_PlayerData.m_HasMount = true;
        //------------------------------------------------------------------------- end of test code -----------------------------------------------------------------------------

        animator_.SetFloat("Walk", m_v);
        /*
        if (m_PlayerData.m_HasMount)
        {
            m_PlayerAnimator.gameObject.SetActive(false);
            m_HorseAnimator.gameObject.SetActive(true);
            m_HorseAnimator.SetFloat("Walk", m_v);
        }
        else
        {
            m_HorseAnimator.gameObject.SetActive(false);
            m_PlayerAnimator.gameObject.SetActive(true);
            m_PlayerAnimator.SetFloat("Walk", m_v);
        }*/

    }

    private void DecreaseTime()
    {
        //when the player is moving decrease time by Time.deltaTime
        if (navAgent_.velocity.magnitude > 0.0f && m_PlayerData.m_CurrTime != 0.0f)
        {
            m_PlayerData.m_CurrTime -= Time.deltaTime;
            hungerTimer_ += Time.deltaTime;
            if(hungerTimer_ >= 1.0f)
            {
                m_PlayerData.m_HungerMeter += 1.0f;
                hungerTimer_ = 0.0f;
            }
        }
    }

    private void UpdateWalkSound()
    {
        if(m_PlayerData.m_HasMount == true)
        {
            if (m_IsMoving && !m_Horse.m_HorseGallop.isPlaying)
            {
                m_Horse.m_HorseGallop.Play();
            }
        }
        else
        {
            if (m_IsMoving && !m_PlayerRunning.isPlaying)
            {
                m_PlayerRunning.Play();
            }
        }
    }
}
