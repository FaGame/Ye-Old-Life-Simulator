using UnityEngine;
using System.Collections;

public class ProjectM : MonoBehaviour 
{
    public float m_MLength = 1.0f;
    public Material m_Material;

    private GameObject gObjCopy_;
    private MeshFilter[] meshFilters_;

	// Use this for initialization
	void Start () 
    {
        GameObject gObj = GameObject.FindGameObjectWithTag("Building");

        gObjCopy_ = Instantiate(gObj) as GameObject;

        meshFilters_ = gObjCopy_.GetComponentsInChildren<MeshFilter>();
        foreach (MeshFilter mFilter in meshFilters_)
        {
            Vector3[] vertices = mFilter.mesh.vertices;
            Vector3[] normals = mFilter.mesh.normals;

            for(int i = 0; i < vertices.Length; ++i)
            {
                /*vertices[i].x += normals[i].x * m_MLength;
                vertices[i].y += normals[i].y * m_MLength;
                vertices[i].z += normals[i].z * m_MLength;*/
                vertices[i] += normals[i] * m_MLength;
            }

            mFilter.mesh.vertices = vertices;
            mFilter.renderer.material = m_Material;
            for (int i = 0; i < mFilter.renderer.materials.Length; ++i)
            {
                mFilter.renderer.materials[i] = m_Material;
            }
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        MeshFilter[] mFilters = gObjCopy_.GetComponentsInChildren<MeshFilter>();
        for(int i = 0; i < mFilters.Length; ++i)
        {
            mFilters[i] = meshFilters_[i];
        }
    }
}
