using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectM : MonoBehaviour 
{
    public float m_LengthInside = 0.01f;
    public float m_LengthOutside = 0.1f;
    public Material m_Material;

    private GameObject[] gObjects_;
    private List<GameObject> gObjInside_;
    private List<GameObject> gObjOutside_;
    private MeshFilter[] meshFilters_;

	// Use this for initialization
	void Start () 
    {
        gObjects_ = GameObject.FindGameObjectsWithTag("Building");
        gObjInside_ = new List<GameObject>();
        gObjOutside_ = new List<GameObject>();

        for (int i = 0; i < gObjects_.Length; ++i)
        {
            gObjInside_.Add(Instantiate(gObjects_[i]) as GameObject);
            MakeGameObjectInert(gObjInside_[i]);

            meshFilters_ = gObjInside_[i].GetComponentsInChildren<MeshFilter>();
            foreach (MeshFilter mFilter in meshFilters_)
            {
                Vector3[] vertices = mFilter.mesh.vertices;
                Vector3[] normals = mFilter.mesh.normals;

                for (int j = 0; j < vertices.Length; ++j)
                {
                    vertices[j] += normals[j] * m_LengthInside;
                }

                mFilter.mesh.vertices = vertices;
                //mFilter.renderer.material = m_Material;
                //mFilter.renderer.material.SetFloat("_Coefficient", 1.1f);
                //mFilter.renderer.material.SetFloat("_Power", 1.4f);

                Renderer[] renderers = gObjInside_[i].GetComponentsInChildren<Renderer>();
                for (int k = 0; k < renderers.Length; ++k)
                {
                    Material[] mats = renderers[k].materials;

                    for (int j = 0; j < mats.Length; ++j)
                    {
                        mats[j] = m_Material;
                        mats[j].SetFloat("_Coefficient", 1.1f);
                        mats[j].SetFloat("_Power", 1.4f);
                    }

                    renderers[k].materials = mats;
                }

                /*for (int j = 0; j < mFilter.renderer.materials.Length; ++j)
                {
                    mFilter.renderer.materials[j] = m_Material;
                    mFilter.renderer.materials[j].SetFloat("_Coefficient", 1.1f);
                    mFilter.renderer.materials[j].SetFloat("_Power", 1.4f);
                }*/
            }

            gObjOutside_.Add(Instantiate(gObjects_[i]) as GameObject);
            MakeGameObjectInert(gObjOutside_[i]);

            meshFilters_ = gObjOutside_[i].GetComponentsInChildren<MeshFilter>();
            foreach (MeshFilter mFilter in meshFilters_)
            {
                Vector3[] vertices = mFilter.mesh.vertices;
                Vector3[] normals = mFilter.mesh.normals;

                for (int j = 0; j < vertices.Length; ++j)
                {
                    /*vertices[i].x += normals[i].x * m_MLength;
                    vertices[i].y += normals[i].y * m_MLength;
                    vertices[i].z += normals[i].z * m_MLength;*/
                    vertices[j] += normals[j] * m_LengthOutside;
                }

                mFilter.mesh.vertices = vertices;

                Renderer[] renderers = gObjOutside_[i].GetComponentsInChildren<Renderer>();
                for (int k = 0; k < renderers.Length; ++k)
                {
                    Material[] mats = renderers[k].materials;

                    for (int j = 0; j < mats.Length; ++j)
                    {
                        mats[j] = m_Material;
                        mats[j].SetFloat("_Coefficient", 0.1f);
                        mats[j].SetFloat("_Power", 1.2f);
                    }

                    renderers[k].materials = mats;
                }

                /*for (int j = 0; j < mFilter.renderer.materials.Length; ++j)
                {
                    mFilter.renderer.materials[j] = m_Material;
                    mFilter.renderer.materials[j].SetFloat("_Coefficient", 0.1f);
                    mFilter.renderer.materials[j].SetFloat("_Power", 1.2f);
                }*/
            }

            gObjInside_[i].SetActive(false);
            gObjOutside_[i].SetActive(false);
        }
        
    }

    void MakeGameObjectInert(GameObject gObj)
    {
        Building bldg = gObj.GetComponent<Building>();
        if(bldg != null)
        {
            bldg.enabled = false;
        }

        Habitat hbt = gObj.GetComponent<Habitat>();
        if (hbt != null)
        {
            hbt.enabled = false;
        }

        gObj.collider.enabled = false;

        Transform[] posEs = gObj.GetComponentsInChildren<Transform>();
        foreach (Transform tForm in posEs)
        {
            if (tForm.CompareTag("Waypoint")  || (tForm.GetComponent<ParticleSystem>() != null))
            {
                tForm.gameObject.SetActive(false);
                //break;
            }
        }
    }

    public void TurnOn(GameObject building)
    {
        //Debug.Log("ON: " + building.name);
        /*if (building.name.Contains("(Clone)"))
        {
            return;
        }*/
        SwitchBuilding(building);
    }

    public void TurnOffAll()
    {
        //Debug.Log("OFF ALL");
        for (int i = 0; i < gObjInside_.Count; ++i)
        {
            //gObjects_[i].SetActive(true);
            gObjInside_[i].SetActive(false);
            gObjOutside_[i].SetActive(false);
        }
    }


    void SwitchBuilding(GameObject building)
    {
        string bName = building.name + "(Clone)";

        for (int i = 0; i < gObjInside_.Count; ++i)
        {
            if (gObjInside_[i].name == bName)
            {
                gObjInside_[i].SetActive(true);
                gObjOutside_[i].SetActive(true);
                //gObjects_[i].SetActive(false);
            }
            else
            {
                //gObjects_[i].SetActive(true);
                gObjInside_[i].SetActive(false);
                gObjOutside_[i].SetActive(false);
            }
        }
    }
	
	// Update is called once per frame
	void Update () 
    {
        /*MeshFilter[] mFilters = gObjInside_.GetComponentsInChildren<MeshFilter>();
        for(int i = 0; i < mFilters.Length; ++i)
        {
            mFilters[i] = meshFilters_[i];
        }

        mFilters = gObjOutside_.GetComponentsInChildren<MeshFilter>();
        for (int i = 0; i < mFilters.Length; ++i)
        {
            mFilters[i] = meshFilters_[i];
        }*/
    }
}
