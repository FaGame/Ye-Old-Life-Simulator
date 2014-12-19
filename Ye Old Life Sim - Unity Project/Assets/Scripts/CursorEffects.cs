using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CursorEffects : MonoBehaviour
{

    public ParticleEmitter mPEmitter;


    public ParticleEmitter m_ParticleTwo;
    public GameObject m_BuildingUI; //This is the game object that has the image component on it for the building UI
    public GameObject m_StatsUI; //This is the game object that has the image component on it for the Stats UI
    public RectTransform m_StatsCanvas;
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

        m_StatsCanvas = GameObject.Find("UI").GetComponent<RectTransform>();

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
                        mPEmitter.emit = true;
                        /*switch (hit.collider.tag)
                        {
                            case "Ground":
                                mPEmitter.emit = true;
                                break;
                        }*/
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
                                m_ParticleTwo.transform.position = cursor.GetPoint(10);
                                m_ParticleTwo.transform.LookAt(Camera.main.transform);
                                m_ParticleTwo.transform.Rotate(new Vector3(90.0f, 0.0f, 0.0f));
                                m_ParticleTwo.emit = true;
                            }
                        }
                        if (statsImage_ != null)
                        {
                            Debug.Log("Click point " + Input.mousePosition);

                            Vector2 pos;
                            RectTransformUtility.ScreenPointToLocalPointInRectangle(statsImage_.rectTransform, Input.mousePosition, m_StatsCanvas.camera, out pos);
                            if (statsImage_.rectTransform.rect.Contains(pos))
                            {
                                Debug.Log("IN");
                                m_ParticleTwo.transform.position = cursor.GetPoint(75);
                                m_ParticleTwo.transform.LookAt(Camera.main.transform);
                                m_ParticleTwo.transform.rotation = Quaternion.AngleAxis(90.0f, new Vector3(1.0f, 0.0f, 0.0f));
                                m_ParticleTwo.emit = true;
                            }

                            float left = Camera.main.WorldToScreenPoint(m_StatsCanvas.position).x - (statsImage_.rectTransform.rect.width / 2.0f) * statsImage_.rectTransform.localScale.x;
                            float bottom = Camera.main.WorldToScreenPoint(m_StatsCanvas.position).y - (statsImage_.rectTransform.rect.height / 2.0f) * statsImage_.rectTransform.localScale.y;
                            float right = Camera.main.WorldToScreenPoint(m_StatsCanvas.position).x + (statsImage_.rectTransform.rect.width / 2.0f) * statsImage_.rectTransform.localScale.x;
                            float top = Camera.main.WorldToScreenPoint(m_StatsCanvas.position).y + (statsImage_.rectTransform.rect.height / 2.0f) * statsImage_.rectTransform.localScale.y;

                            Debug.Log("Top: " + top + " Left: " + left + " Right: " + right + " Bottom: " + bottom);

                            if (hit.point.x > left && hit.point.y < top && hit.point.x < right && hit.point.y > bottom)
                            {
                                Debug.Log("Inside the HUD");
                                m_ParticleTwo.transform.position = cursor.GetPoint(75);
                                m_ParticleTwo.transform.LookAt(Camera.main.transform);
                                m_ParticleTwo.transform.rotation = Quaternion.AngleAxis(90.0f, new Vector3(1.0f, 0.0f, 0.0f));
                                m_ParticleTwo.emit = true;
                            }
                        }
                    }
                }
        }
        else
        {
            mPEmitter.emit = false;
            m_ParticleTwo.emit = false;
        }
    }
}
