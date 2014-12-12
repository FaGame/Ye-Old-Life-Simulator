using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuildingUI : MonoBehaviour 
{
    public GameObject m_BuildingGUI; //Building UI element
    public GameObject m_ApplyMenu; //Menu that appears after pressing "Apply for Job"
    public GameObject m_ApplyMenuButtonPrefab; //Prefab for the Apply Menu Button to Instantiate later
    public GameObject m_ScrollMaskContent; //Gameobject that is to have the job information as a parent to allow for scrolling
    public Button m_WorkButton; //Building UI "work" button
    public PlayerData m_PlayerData;

    private bool buildingsActive_ = false; //Flag to turn on and off the building UI
    private GameObject selectedBuilding_; //Selected building GameObject
    private Text[] buildingMenuText_; //Array of text on the building UI element (Buy Items, Interact, etc)..
    private Text[] applyMenuText_; //Array of text for the Apply For Job Menu
    private Text descriptionText_; //Building description text - the funny quip at the top of the building UI
    private SkillAndAmount jobGainedData_;

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

        //-------------TEMP CODE-------------
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
        //-------------END TEMP CODE-------------
	}

    //This function loads the building data based on which building was clicked
    void LoadBuildingData(string name)
    {
        buildingsActive_ = true;
        selectedBuilding_ = GameObject.Find(name);
        descriptionText_.text = selectedBuilding_.GetComponent<Building>().GetDescription();

        Debug.Log("Loading " + selectedBuilding_.name + "'s data");
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
            go.GetComponentInChildren<Button>().onClick.AddListener(delegate { ApplyForJob(go); });
            startYPos -= yPosOffset;
        }

        applyMenuText_ = m_ApplyMenu.GetComponentsInChildren<Text>();

        /*
         * i -- Used to get and populate the text elements on the Apply for Job Menu
         *   -- It is incremented by 3 each iteration because there are 3 text elements in each button prefab, so in order to move on to the next prefab without altering other text elements
         *   -- we need to jump over them each iteration
         *   
         * j -- Used to get the job data
         *   -- This is incremented at the end of each iteration of the for loop so it can get the appropriate job data
         * 
         * k -- Used to get the skill gain data
         *  NOTE: To remove the IF statements try using a for loop ---- ATTEMPT AT A LATER TIME WHEN NOT TIRED
         */
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

    //Button function - This function is called by the giant "X" in the top right corner of the building UI.
    //If the Apply for Job menu is active it will destroy all of it's children -- Pretty dark, right?
    //Otherwise it will just disable the menu.
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

    //Button function - The object passed into the function is the button itself to get the appropriate information
    //                - The function first finds the name of the job you selected based off of the text of the button the player pressed
    //                - It then will check to see if you were successful in your job application, if you are, the player has gotten a job!
    public void ApplyForJob(GameObject go)
    {
        for(int i = 0; i < selectedBuilding_.GetComponent<Building>().m_JobData.Length; ++i)
        {
            if(selectedBuilding_.GetComponent<Building>().m_JobData[i].name == go.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text)
            {
                jobGainedData_ = selectedBuilding_.GetComponent<Building>().ApplyForJob(m_PlayerData, selectedBuilding_.GetComponent<Building>().m_JobData[i]);

                if(jobGainedData_ == null)
                {
                    m_PlayerData.m_Job = selectedBuilding_.GetComponent<Building>().m_JobData[i];
                }
            }
        }
    }

    public void Work()
    {
        selectedBuilding_.GetComponent<Building>().Work(m_PlayerData, m_PlayerData.m_Job);
    }
}