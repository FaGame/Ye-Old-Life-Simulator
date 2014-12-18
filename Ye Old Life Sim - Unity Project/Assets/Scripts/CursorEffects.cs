using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CursorEffects : MonoBehaviour
{

    public ParticleEmitter mPEmitter;


    public ParticleEmitter m_ParticleTwo;
    public GameObject m_BuildingUI; //This is the game object that has the image component on it for the building UI
    public GameObject m_StatsUI; //This is the game object that has the image component on it for the Stats UI
    public HUDScript m_HUDUI; //This is the HUD script so we can access the boolean that will tell us if there is any HUD active at all

    private Image bUIImage_;
    private Image statsImage_;

    // Use this for initialization
    void Start()
    {
        mPEmitter.emit = false;
        m_ParticleTwo.emit = false;

        bUIImage_ = m_BuildingUI.GetComponent<Image>();
        statsImage_ = m_StatsUI.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {


        //have the particle emitter follow the mouse
        Ray cursor = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        mPEmitter.transform.position = cursor.GetPoint(75);

        // Debug.Log(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(cursor, out hit))
                //If the HUD element is not active, play the first particle emitter if the ground is clicked
                if (!m_HUDUI.HUDActive)
                {
                    if (Physics.Raycast(cursor, out hit))
                    {
                        switch (hit.collider.tag)
                        {
                            case "Ground":
                                mPEmitter.emit = true;
                                break;
                        }
                    }
                }
                //If it is active, play the second particle emitter when the user clicks
                else
                {
                    if (Physics.Raycast(cursor, out hit))
                    {
                        if (bUIImage_ != null)
                        {
                            if (hit.point.x > bUIImage_.rectTransform.position.x && hit.point.y < bUIImage_.rectTransform.position.y &&
                            hit.point.x < bUIImage_.rectTransform.position.x + bUIImage_.rectTransform.rect.width && hit.point.y > bUIImage_.rectTransform.position.y + bUIImage_.rectTransform.rect.height)
                            {
                                m_ParticleTwo.transform.position = cursor.GetPoint(100);
                                m_ParticleTwo.transform.LookAt(Camera.main.transform);
                                m_ParticleTwo.transform.Rotate(new Vector3(90.0f, 0.0f, 0.0f));
                                m_ParticleTwo.emit = true;
                            }
                        }
                        if (statsImage_ != null)
                        {
                            Debug.Log("Click point " + hit.point);
                            Debug.Log("Image X " + statsImage_.rectTransform.position.x + " Image Y " + statsImage_.rectTransform.position.y);
                            if (hit.point.x > statsImage_.rectTransform.position.x && hit.point.y < statsImage_.rectTransform.position.y &&
                            hit.point.x < statsImage_.rectTransform.position.x + statsImage_.rectTransform.rect.width && hit.point.y > statsImage_.rectTransform.position.y + statsImage_.rectTransform.rect.height)
                            {
                                Debug.Log("Inside the HUD");
                                m_ParticleTwo.transform.position = cursor.GetPoint(100);
                                m_ParticleTwo.transform.LookAt(Camera.main.transform);
                                m_ParticleTwo.transform.Rotate(new Vector3(90.0f, 0.0f, 0.0f));
                                m_ParticleTwo.emit = true;
                            }
                        }
                    }
                }
        }
    }
}
