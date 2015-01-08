using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseCursor : MonoBehaviour 
{
    public List<Sprite> m_MouseNormalAnimate;
    public List<Sprite> m_MouseClickAnimate;
    public Vector2 m_HotSpot = Vector2.zero;
    public CursorMode m_CursorMode = CursorMode.Auto;
    public float m_FramesPerSecond = 30.0f;

    public bool isMouseButtonDown_;
    private List<Sprite> currentMouseCursorAnim_;
    private int currFrame_;
    private float currTime_;
    //private delegate void mouseStatus_();

	// Use this for initialization
	void Start () 
    {
        currentMouseCursorAnim_ = m_MouseNormalAnimate;
        Cursor.SetCursor(currentMouseCursorAnim_[0].texture, m_HotSpot, m_CursorMode);
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
            AnimateMouseCursor();
        }
        else
        {
            NormalMouseCursor();
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
        Cursor.SetCursor(frame, m_HotSpot, m_CursorMode);

        ++currFrame_;
        if(currFrame_ >= currentMouseCursorAnim_.Count)
        {
            currFrame_ = 0;
        }
	}

    void NormalMouseCursor()
    {
        /*if (!isMouseButtonDown_)
        {
            return;
        }*/
        if (currentMouseCursorAnim_ == m_MouseNormalAnimate)
        {
            return;
        }
        currFrame_ = 0;
        currTime_ = 0.0f;
        currentMouseCursorAnim_ = m_MouseNormalAnimate;
        isMouseButtonDown_ = false;
    }

    void AnimateMouseCursor()
    {
        /*if(isMouseButtonDown_)
        {
            return;
        }*/
        if (currentMouseCursorAnim_ == m_MouseClickAnimate)
        {
            return;
        }
        currFrame_ = 0;
        currTime_ = 0.0f;
        currentMouseCursorAnim_ = m_MouseClickAnimate;
        isMouseButtonDown_ = true;
    }
}
