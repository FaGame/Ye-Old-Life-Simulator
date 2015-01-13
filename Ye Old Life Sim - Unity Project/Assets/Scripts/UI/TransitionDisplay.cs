using UnityEngine;
using System.Collections;

public class TransitionDisplay : MonoBehaviour 
{
    public float m_TransitionSpeedMultiplier = 4.0f;

    private CanvasRenderer[] canvasRenderer_;
    private bool transitionToVisible_ = false;
    private bool isIransitioning_ = false;
    public delegate void CleanupCode();
    //private bool isTransitionCompleted_ = false;
    //private bool initialEnable_ = true;
    //private bool initialDisable_ = true;
    private float transitionAlpha_;
    private CleanupCode cleanupCode_;

    void Start()
    {
        setupVars();
    }

    void Update()
    {
        transitionGUI();
    }

    /*void OnEnable()
    {
        if(initialEnable_)
        {
            initialEnable_ = !initialEnable_;
        }
        else if (!isIransitioning_)
        {
            kickoffTransitionGUI(true);
        }
    }*/
    public void PrepareForFadeIn()
    {
        gameObject.SetActive(true);
        if (canvasRenderer_ == null)
        {
            setupVars();
        }
        transitionAlpha_ = 0.0f;
        setAllCanvasAlphas();
        canvasRenderer_ = null;
    }
    public void FadeIn()
    {
        if(canvasRenderer_ == null)
        {
            setupVars();
        }
        kickoffTransitionGUI(true);
    }

    public bool IsTransitioning()
    {
        return isIransitioning_;
    }

    /*void OnDisable()
    {
        if(initialDisable_)
        {
            initialDisable_ = !initialDisable_;
        }
        else
        {
            if (isTransitionCompleted_)
            {
                isTransitionCompleted_ = !isTransitionCompleted_;
            }
            else if (!isIransitioning_)
            {
                gameObject.SetActive(true);
                kickoffTransitionGUI(false);
            }
        }
    }*/
    public void FadeOut(CleanupCode cCode)
    {
        cleanupCode_ = cCode;
        if (canvasRenderer_ == null)
        {
            setupVars();
        }
        kickoffTransitionGUI(false);
    }

    void kickoffTransitionGUI(bool toVisible)
    {
        if (!isIransitioning_)
        {
            transitionToVisible_ = toVisible;
            transitionAlpha_ = transitionToVisible_ ? 0.0f : 1.0f;
            //canvasRenderer_.SetAlpha(transitionAlpha_);
            setAllCanvasAlphas();
            gameObject.SetActive(true);
            isIransitioning_ = true;
        }
    }

    void transitionGUI()
    {
        if (isIransitioning_)
        {
            transitionAlpha_ += (transitionToVisible_ ? Time.deltaTime : -Time.deltaTime) * m_TransitionSpeedMultiplier;
            //canvasRenderer_.SetAlpha(transitionAlpha_);
            setAllCanvasAlphas();
            if ((transitionAlpha_ <= 0.0f) || (transitionAlpha_ >= 1.0f))
            {
                isIransitioning_ = false;
                if (!transitionToVisible_)
                {
                    //isTransitionCompleted_ = true;
                    gameObject.SetActive(false);
                    if(cleanupCode_ != null)
                    {
                        cleanupCode_();
                    }
                    //m_BuildingGUI.SetActive(buildingsActive_);
                }
                canvasRenderer_ = null;
            }
        }
    }

    void setAllCanvasAlphas()
    {
        for(int i = 0; i < canvasRenderer_.Length; ++i)
        {
            canvasRenderer_[i].SetAlpha(transitionAlpha_);
        }
    }

    void setupVars()
    {
        if(canvasRenderer_ != null)
        {
            return;
        }
        //canvasRenderer_ = gameObject.GetComponent<CanvasRenderer>();
        canvasRenderer_ = gameObject.GetComponentsInChildren<CanvasRenderer>();
        transitionToVisible_ = false;
        isIransitioning_ = false;
        //isTransitionCompleted_ = false;
    }
}
