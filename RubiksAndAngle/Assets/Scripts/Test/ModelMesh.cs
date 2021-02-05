using MyTools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ModelMesh : MonoBehaviour
{

    public List<Vector3> meshPoints;
    public float size = 1;
    [Range( 0,1)]public float range = 1;
    MeshFilter meshFilter;
    [SerializeField] bool isDrawMesh = false;

#if UNITY_EDITOR

    private void OnValidate()
    {
        InitModeMesh();
    }

#endif


    private void Awake()
    {
        InitModeMesh();
    }

    [ContextMenu("InitModeMesh")]
    public void InitModeMesh()
    {

        if (!isDrawMesh) { if (meshPoints != null) meshPoints.Clear(); return; }

        if (meshFilter == null)
            meshFilter = GetComponent<MeshFilter>();

        if (meshFilter == null) { MyDebug.LogRed("meshFilter == null");  return; }

        Mesh mesh = meshFilter.sharedMesh;

        List<string> pointStrings = new List<string>();

        if (mesh == null) return;

        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            string vstr = Vector2String(this.transform.TransformPoint(mesh.vertices[i]));

            if (!pointStrings.Contains(vstr))
            {
                pointStrings.Add(vstr);
            }

        }

        if (meshPoints == null) meshPoints = new List<Vector3>();

        meshPoints.Clear();

        for (int i = 0; i < pointStrings.Count; i++)
        {
            meshPoints.Add(String2Vector(pointStrings[i]));
        }
        

    }

    string Vector2String(Vector3 v)
    {
        StringBuilder str = new StringBuilder();
        str.Append(v.x).Append(",").Append(v.y).Append(",").Append(v.z);
        return str.ToString();
    }

    Vector3 String2Vector(string vstr)
    {
        try
        {
            string[] strings = vstr.Split(',');
            return new Vector3(float.Parse(strings[0]), float.Parse(strings[1]), float.Parse(strings[2]));
        }
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
            return Vector3.zero;
        }
    }

}
