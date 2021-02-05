using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ModelMesh))]
[CanEditMultipleObjects]//add this
public class ModelMeshEditor : Editor
{
    public void OnSceneGUI()
    {
        var path = target as ModelMesh;

        if (path.meshPoints != null && path.meshPoints.Count > 0)
        {
            for (int i = 0; i < path.meshPoints.Count; i++)
            {
                    Handles.Label(path.meshPoints[i], i.ToString());
                    Handles.color = Color.yellow;
            }
        }


    }

    [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
    static void OnDrawGizmo(ModelMesh path, GizmoType gizmoType)
    {

        Handles.color = Handles.xAxisColor;

        if (path.meshPoints != null && path.meshPoints.Count > 0)
        {
            for (int i = 0; i < path.meshPoints.Count; i++)
            {
                Handles.SphereHandleCap(0,
                path.meshPoints[i],
                path.transform.rotation,
                path.size,
                EventType.Repaint);
            }
        }

        Handles.color = Handles.zAxisColor;

        if (path.meshPoints != null && path.meshPoints.Count > 0)
        {
            for (int i = 0; i < path.meshPoints.Count; i++)
            {
                Vector3 startPos = path.meshPoints[i];

                for (int j = 0; j < path.meshPoints.Count; j++)
                {
                    if (i != j && Vector3.Distance(startPos, path.meshPoints[j]) < path.range)
                    {
                        Handles.DrawLine(startPos, path.meshPoints[j]);
                    }
                }

            }
        }


    }

}

