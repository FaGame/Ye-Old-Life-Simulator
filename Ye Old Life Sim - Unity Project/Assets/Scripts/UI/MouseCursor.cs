using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseCursor : MonoBehaviour 
{
    public List<Sprite> m_MouseNormalAnimate;
    public List<Sprite> m_MouseClickAnimate;
    public Vector2 m_HotSpot = Vector2.zero;
    public List<Sprite> m_HardwareMouseNormalAnimate;
    public List<Sprite> m_HardwareMouseClickAnimate;
    public Vector2 m_HardwareHotSpot = Vector2.zero;
    public CursorMode m_CursorMode = CursorMode.Auto;
    public float m_FramesPerSecond = 30.0f;

    public bool isMouseButtonDown_;
    private List<Sprite> currentMouseCursorAnim_;
    private Vector2 hotSpot_;
    private int currFrame_;
    private float currTime_;
    //private delegate void mouseStatus_();

	// Use this for initialization
	void Start () 
    {
        SetCursorValues(false);
        Cursor.SetCursor(currentMouseCursorAnim_[0].texture, hotSpot_, m_CursorMode);
        currFrame_ = 0;
        currTime_ = 0.0f;
        isMouseButtonDown_ = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        // Well this is no fun! :( 
        //mouseStatus_ mStatus;
        //mStatus = (Input.GetMouseButtonDown(0)) ? (AnimateMouseCursor) : (NormalMouseCursor);

        if(Input.GetMouseButton(0))
        {
            SetAnimatedMouseCursor();
        }
        else
        {
            SetNormalMouseCursor();
        }

        currTime_ += Time.deltaTime;
        if(currTime_ < (1.0f / m_FramesPerSecond))
        {
            return;
        }
        currTime_ = 0.0f;

        Color[] pix = currentMouseCursorAnim_[currFrame_].texture.GetPixels((int)currentMouseCursorAnim_[currFrame_].rect.x, (int)currentMouseCursorAnim_[currFrame_].rect.y,
            (int)currentMouseCursorAnim_[currFrame_].rect.width, (int)currentMouseCursorAnim_[currFrame_].rect.height);
        Texture2D frame = new Texture2D((int)currentMouseCursorAnim_[currFrame_].rect.width, (int)currentMouseCursorAnim_[currFrame_].rect.height);
        frame.SetPixels(pix);
        frame.Apply();
        Cursor.SetCursor(frame, hotSpot_, m_CursorMode);

        ++currFrame_;
        if(currFrame_ >= currentMouseCursorAnim_.Count)
        {
            currFrame_ = 0;
        }
	}

    void SetNormalMouseCursor()
    {
        /*if (!isMouseButtonDown_)
        {
            return;
        }*/
        if (currentMouseCursorAnim_ == NormalMouseCursor())
        {
            return;
        }
        currFrame_ = 0;
        currTime_ = 0.0f;
        SetCursorValues(true);
        isMouseButtonDown_ = false;
    }

    void SetAnimatedMouseCursor()
    {
        /*if(isMouseButtonDown_)
        {
            return;
        }*/
        if (currentMouseCursorAnim_ == AnimatedMouseCursor())
        {
            return;
        }
        currFrame_ = 0;
        currTime_ = 0.0f;
        SetCursorValues(true);
        isMouseButtonDown_ = true;
    }

    void SetCursorValues(bool isClickAnimated)
    {
        currentMouseCursorAnim_ = isClickAnimated ? AnimatedMouseCursor() : NormalMouseCursor();
        hotSpot_ = m_CursorMode == CursorMode.Auto ? m_HardwareHotSpot : m_HotSpot;
    }

    List<Sprite> NormalMouseCursor()
    {
        return m_CursorMode == CursorMode.Auto ? m_HardwareMouseNormalAnimate : m_MouseNormalAnimate;
    }

    List<Sprite> AnimatedMouseCursor()
    {
        return m_CursorMode == CursorMode.Auto ? m_HardwareMouseClickAnimate : m_MouseClickAnimate;
    }
}
