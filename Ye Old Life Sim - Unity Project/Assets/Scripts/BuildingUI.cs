using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuildingUI : MonoBehaviour 
{
    public GameObject m_BuildingGUI;
    public GameObject m_ApplyMenu;
    public Button m_WorkButton;
    public Button m_ApplyButton;

    private bool buildingsActive_ = false;
    private bool jobApplied_ = false;
    private GameObject selectedBuilding_;
    private Text[] buildingMenuText_;
    private Text[] applyMenuText_;
    private Text descriptionText_;

	// Use this for initialization
	void Start () 
    {
        buildingMenuText_ = m_BuildingGUI.GetComponentsInChildren<Text>();
        
        descriptionText_ = buildingMenuText_[0];
        m_BuildingGUI.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_BuildingGUI.SetActive(buildingsActive_);
        if (Input.GetMouseButtonDown(0) && !buildingsActive_)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit))
            {
                if (rayHit.collider.tag == "Building")
                {
                    LoadBuildingData(rayHit.collider.name);
                }
            }
        }
	}

    void LoadBuildingData(string name)
    {
        buildingsActive_ = true;
        selectedBuilding_ = GameObject.Find(name);
        descriptionText_.text = selectedBuilding_.GetComponent<Building>().GetDescription();

        Debug.Log("Loading " + selectedBuilding_.name + "'s data");
    }

    public void BuildingGUI()
    {
        buildingsActive_ = !buildingsActive_;
        m_BuildingGUI.SetActive(buildingsActive_);
    }

    //This function is used on button press in the building UI
    //If pressed, it will populate and show the Apply for Job menu, in which you can choose a job to apply for
    public void ApplyMenu()
    {
        jobApplied_ = true;
        m_WorkButton.interactable = true;
        m_ApplyMenu.SetActive(true);
        applyMenuText_ = m_ApplyMenu.GetComponentsInChildren<Text>();

        for (int i = 0; i < selectedBuilding_.GetComponent<Building>().m_JobData.Length; ++i)
        {
            applyMenuText_[i].text = selectedBuilding_.GetComponent<Building>().m_JobData[i].name;
        }
    }

    public void ApplyForJob()
    {
        //selectedBuilding_.GetComponent<Building>().ApplyForJob(selectedBuilding_.GetComponent<Building>().m_JobData);
    }
}