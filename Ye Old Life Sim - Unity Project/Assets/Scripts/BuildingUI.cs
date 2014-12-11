using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuildingUI : MonoBehaviour 
{
    public GameObject m_BuildingGUI;
    public GameObject m_ApplyMenu;
    public GameObject m_ApplyMenuButtonPrefab;
    public GameObject m_ScrollMaskContent;
    public Button m_WorkButton;
    public Button m_ApplyButton;

    private bool buildingsActive_ = false;
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
        float startYPos = 180.0f;
        float yPosOffset = 70.0f;
        m_WorkButton.interactable = true;
        m_ApplyMenu.SetActive(true);

        //Create the necessary amount of buttons to display on screen
        for (int i = 0; i < selectedBuilding_.GetComponent<Building>().m_JobData.Length; ++i)
        {
            GameObject go = (GameObject)Instantiate(m_ApplyMenuButtonPrefab, new Vector3(0, startYPos, 0), Quaternion.identity);
            go.gameObject.transform.SetParent(m_ScrollMaskContent.transform, false);
            startYPos -= yPosOffset;
        }

        applyMenuText_ = m_ApplyMenu.GetComponentsInChildren<Text>();
        int j = 0;
        int k = 0;
        for (int i = 0; i < selectedBuilding_.GetComponent<Building>().m_JobData.Length * 3; i += 3)
        {
            applyMenuText_[i].text = selectedBuilding_.GetComponent<Building>().m_JobData[j].name;
            applyMenuText_[i + 1].text = selectedBuilding_.GetComponent<Building>().m_JobData[j].m_JobDescription;
            if (selectedBuilding_.GetComponent<Building>().m_JobData[j].m_SkillGain.Length == 3)
            {
                applyMenuText_[i + 2].text = selectedBuilding_.GetComponent<Building>().m_JobData[j].m_Wage.ToString() + " shilling(s) and " +
                selectedBuilding_.GetComponent<Building>().m_JobData[j].m_SkillGain[k].m_Amount.ToString() + " point(s) in " + selectedBuilding_.GetComponent<Building>().m_JobData[j].m_SkillGain[k].m_Skill.ToString() + ", " +
                selectedBuilding_.GetComponent<Building>().m_JobData[j].m_SkillGain[k + 1].m_Amount.ToString() + " point(s) in " + selectedBuilding_.GetComponent<Building>().m_JobData[j].m_SkillGain[k + 1].m_Skill.ToString() + "\nand " +
                selectedBuilding_.GetComponent<Building>().m_JobData[j].m_SkillGain[k + 2].m_Amount.ToString() + " point(s) in " + selectedBuilding_.GetComponent<Building>().m_JobData[j].m_SkillGain[k + 2].m_Skill.ToString() + ".";
            }
            else if (selectedBuilding_.GetComponent<Building>().m_JobData[j].m_SkillGain.Length == 2)
            {
                applyMenuText_[i + 2].text = selectedBuilding_.GetComponent<Building>().m_JobData[j].m_Wage.ToString() + " shilling(s) and " +
                selectedBuilding_.GetComponent<Building>().m_JobData[j].m_SkillGain[k].m_Amount.ToString() + " point(s) in " + selectedBuilding_.GetComponent<Building>().m_JobData[j].m_SkillGain[k].m_Skill.ToString() + " and " +
                selectedBuilding_.GetComponent<Building>().m_JobData[j].m_SkillGain[k + 1].m_Amount.ToString() + " point(s) in " + selectedBuilding_.GetComponent<Building>().m_JobData[j].m_SkillGain[k + 1].m_Skill.ToString() + ".";
            }
            else
            {
                applyMenuText_[i + 2].text = selectedBuilding_.GetComponent<Building>().m_JobData[j].m_Wage.ToString() + " shilling(s) and " +
                selectedBuilding_.GetComponent<Building>().m_JobData[j].m_SkillGain[k].m_Amount.ToString() + " point(s) in " + selectedBuilding_.GetComponent<Building>().m_JobData[j].m_SkillGain[k].m_Skill.ToString() + ".";
            }
            j++;
        }
    }

    public void CloseCurrentMenu()
    {
        if(m_ApplyMenu.activeSelf)
        {
            foreach(RectTransform child in m_ScrollMaskContent.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            m_ApplyMenu.SetActive(false);
        }
        else if(buildingsActive_)
        {
            buildingsActive_ = false;
        }
    }

    public void ApplyForJob()
    {
        //selectedBuilding_.GetComponent<Building>().ApplyForJob(selectedBuilding_.GetComponent<Building>().m_JobData);
    }
}