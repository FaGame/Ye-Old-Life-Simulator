using UnityEngine;
using System.Collections;

public class MouseCursor : MonoBehaviour 
{
    public Texture2D m_MouseCursorTexture;
    public Vector2 m_HotSpot = Vector2.zero;
    public CursorMode m_CursorMode = CursorMode.Auto;

	// Use this for initialization
	void Start () 
    {
        Cursor.SetCursor(m_MouseCursorTexture, m_HotSpot, m_CursorMode);
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
