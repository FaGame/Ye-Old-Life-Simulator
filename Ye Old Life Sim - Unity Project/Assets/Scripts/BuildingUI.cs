using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuildingUI : MonoBehaviour 
{
    public GameObject m_BuildingGUI;
    public Button m_WorkButton;
    public Button m_ApplyButton;

    private bool buildingsActive_ = false;
    private bool jobApplied_ = false;

	// Use this for initialization
	void Start () 
    {
        m_BuildingGUI.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void BuildingGUI()
    {
        buildingsActive_ = !buildingsActive_;
        m_BuildingGUI.SetActive(buildingsActive_);
    }

    public void ApplyForJob()
    {
        jobApplied_ = true;
        m_WorkButton.interactable = true;
        if (jobApplied_)
        {
            m_ApplyButton.interactable = false;
        }
    }
}
