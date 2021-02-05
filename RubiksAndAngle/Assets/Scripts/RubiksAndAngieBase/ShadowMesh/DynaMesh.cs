using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YProjectBase;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SkinnedMeshRenderer))]
[DisallowMultipleComponent]
public class DynaMesh : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer meshRenderer;
    [SerializeField] MeshCollider dynaCollider;
    [SerializeField] Rigidbody rig;


    private void OnDisable()
    {
        MonoMgr.GetInstance().RemoveLateUpdateListener(LateUpdatedCollider);
    }
    private void OnDestroy()
    {
        MonoMgr.GetInstance().RemoveLateUpdateListener(LateUpdatedCollider);
    }


    public void LateUpdatedCollider()
    {
        //Mesh colliderMesh = new Mesh();//内存泄漏
        //meshRenderer.BakeMesh(colliderMesh);
        if (dynaCollider != null && meshRenderer != null)
        {
            meshRenderer.sharedMesh.RecalculateNormals();
            dynaCollider.sharedMesh = meshRenderer.sharedMesh;
        }
        else
        {
            if (dynaCollider == null)
                dynaCollider = GetComponent<MeshCollider>();
            if (meshRenderer == null)
                meshRenderer = GetComponent<SkinnedMeshRenderer>();
        }
    }

    [ContextMenu("InitDynaMesh")]
    public void InitDynaMesh()
    {
        if (meshRenderer == null)
            meshRenderer = GetComponent<SkinnedMeshRenderer>();

        if (rig == null)
        {
            rig = GetComponent<Rigidbody>();
            rig.useGravity = false;
            rig.mass = 1000;
            rig.isKinematic = true;
            rig.drag = 0;
            rig.angularDrag = 0;
            rig.constraints = RigidbodyConstraints.FreezeAll;
        }


        Collider[] cols = GetComponents<Collider>();
        foreach (var col in cols)
        {
            Debug.Log("Collider   : " + col);

            if (Application.isEditor)
            {
                DestroyImmediate(col);
            }
        }

        dynaCollider = gameObject.AddComponent<MeshCollider>();

        LateUpdatedCollider();
    }

}
