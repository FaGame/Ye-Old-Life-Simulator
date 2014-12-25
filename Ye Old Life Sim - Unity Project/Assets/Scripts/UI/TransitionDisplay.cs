using UnityEngine;
using System.Collections;

public class TransitionDisplay : MonoBehaviour 
{
    public float m_TransitionSpeedMultiplier = 4.0f;

    private CanvasRenderer canvasRenderer_;
    private bool transitionToVisible_;
    private bool isIransitioning_;
    private bool isTransitionCompleted_;
    private bool initialEnable_ = true;
    private bool initialDisable_ = true;
    private float transitionAlpha_;

    void Start()
    {
        canvasRenderer_ = gameObject.GetComponent<CanvasRenderer>();
        transitionToVisible_ = false;
        isIransitioning_ = false;
        isTransitionCompleted_ = false;
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
    public void FadeIn()
    {
        kickoffTransitionGUI(true);
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
    public void FadeOut()
    {
        kickoffTransitionGUI(false);
    }

    void kickoffTransitionGUI(bool toVisible)
    {
        if (!isIransitioning_)
        {
            transitionToVisible_ = toVisible;
            transitionAlpha_ = transitionToVisible_ ? 0.0f : 1.0f;
            canvasRenderer_.SetAlpha(transitionAlpha_);
            gameObject.SetActive(true);
            isIransitioning_ = true;
        }
    }

    void transitionGUI()
    {
        if (isIransitioning_)
        {
            transitionAlpha_ += (transitionToVisible_ ? Time.deltaTime : -Time.deltaTime) * m_TransitionSpeedMultiplier;
            canvasRenderer_.SetAlpha(transitionAlpha_);
            if ((transitionAlpha_ <= 0.0f) || (transitionAlpha_ >= 1.0f))
            {
                isIransitioning_ = false;
                if (!transitionToVisible_)
                {
                    isTransitionCompleted_ = true;
                    gameObject.SetActive(false);
                    //m_BuildingGUI.SetActive(buildingsActive_);
                }
            }
        }
    }
}
