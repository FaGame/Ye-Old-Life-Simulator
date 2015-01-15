using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class ButtonPusher : MonoBehaviour 
{
    private GameObject buttonsScrollMask_;
    private Button[] buttons_;
    private Button closeButton_;

	// Use this for initialization
	void Start () 
    {
        SetCloseButton();
	}

    void OnEnable()
    {
        SetCloseButton();
    }

    void SetCloseButton()
    {
        Button[] buttons = gameObject.GetComponentsInChildren<Button>();
        foreach (Button btn in buttons)
        {
            if (btn.name == "CloseButton")
            {
                closeButton_ = btn;
                break;
            }
        }
    }
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void ButtonsAreInstantiated(GameObject scrollMask)
    {
        buttonsScrollMask_ = scrollMask;
        buttons_ = scrollMask.GetComponentsInChildren<Button>();
    }

    public void SelectButton(int buttonNum)
    {
        buttons_[buttonNum].onClick.Invoke();
    }

    public void SelectCloseButton()
    {
        closeButton_.onClick.Invoke();
    }
}
