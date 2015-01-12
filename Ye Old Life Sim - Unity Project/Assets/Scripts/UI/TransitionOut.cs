using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TransitionOut : MonoBehaviour 
{
    public float m_TransitionSpeed = 4.0f;
    public delegate void CleanupCode();

    private CleanupCode cleanupCode_;
    private bool isTransitioning_;
    private float currVertPos_;

	// Use this for initialization
	void Start () 
    {
        isTransitioning_ = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(isTransitioning_)
        {
            currVertPos_ += Time.deltaTime * m_TransitionSpeed;
            currVertPos_ = Mathf.Min(2.0f, currVertPos_);
            gameObject.GetComponent<ScrollRect>().verticalNormalizedPosition = currVertPos_;
            isTransitioning_ = currVertPos_ >= 2.0f ? false : true;
            if(!isTransitioning_)
            {
                cleanupCode_();
                gameObject.SetActive(false);
            }
        }
	}

    public void StartTransition(CleanupCode cCode)
    {
        cleanupCode_ = cCode;
        currVertPos_ = gameObject.GetComponent<ScrollRect>().verticalNormalizedPosition;
        isTransitioning_ = true;
    }

    public bool Transitioning()
    {
        return isTransitioning_;
    }
}
