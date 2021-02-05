using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowAix : MonoBehaviour
{
    public Transform AixY;
    public Transform AixX;
    public Transform AixZ;

    [SerializeField] Vector3 shadowOffsetPos = new Vector3();

    public void ToUpdateAix(Transform _aixY, Transform _aixX, Transform _aixZ)
    {

        AixY.rotation = _aixY != null ?  _aixY.rotation : AixY.rotation;
        AixX.rotation = _aixX != null ? _aixX.rotation : AixX.rotation;
        AixZ.rotation = _aixZ != null ? _aixZ.rotation : AixZ.rotation;


#if UNITY_EDITOR

        UpdateModel();

#endif

    }

    public void ToUpdateTrans(Transform _transPos)
    {
        transform.position = _transPos != null ? new Vector3(_transPos.position.x, _transPos.position.y, shadowOffsetPos.z) : transform.position;

#if UNITY_EDITOR

        UpdateModel();

#endif
    }


#if UNITY_EDITOR

    public ModelMesh modelMesh;

   public void UpdateModel()
   {
      if(modelMesh!=null)
         modelMesh.InitModeMesh();
   }

#endif

}
