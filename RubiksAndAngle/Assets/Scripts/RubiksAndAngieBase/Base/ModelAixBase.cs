using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ModelAixBase
{

    [HideInInspector] [SerializeField] string nameAix;

    [Tooltip("模型对象")] [YProjectBase.Disabled] [SerializeField] GameObject modelObj;
    [Tooltip("模型父对象")] [YProjectBase.Disabled] [SerializeField] GameObject aixObj;

    [Tooltip("模型父对象（aixY）位置")] [SerializeField] Vector3 modelPos;
    [Tooltip("旋转数据 X = aixX ，Y = aixY ，Z = aixZ ")][SerializeField] Vector3 modelRotate;

    [HideInInspector] [SerializeField] Transform aixY;
    [HideInInspector] [SerializeField] Transform aixX;
    [HideInInspector] [SerializeField] Transform aixZ;

    [YProjectBase.Disabled] [SerializeField] Vector3 originRotateAix;
    [YProjectBase.Disabled] [SerializeField] Vector3 originPostionAix;

    Vector3 modelPosOld;
    Vector3 aixYRotateOld;
    Vector3 aixXRotateOld;
    Vector3 aixZRotateOld;


    public GameObject ModelObj { get { return modelObj; } }
    public GameObject AixObj { get { return aixObj; } }

    public Transform AixY { get { return aixY; } }
    public Transform AixX { get { return aixX; } }
    public Transform AixZ { get { return aixZ; } }

    public Vector3 ModelPos { get { return modelPos; } }
    public Vector3 ModelRotate { get { return modelRotate; } }
    public Vector3 OriginRotateAix { get { return originRotateAix; } set { originRotateAix = value; } }
    public Vector3 OriginPostionAix { get { return originPostionAix; } set { originPostionAix = value; } }

    public ModelAixBase(GameObject _modelObj, Transform _aixY, Transform _aixX, Transform _aixZ, Vector3 _originAix)
    {
        this.modelObj = _modelObj;
        this.aixObj = _aixY.gameObject;
        this.aixY = _aixY;
        this.aixX = _aixX;
        this.aixZ = _aixZ;
        this.originRotateAix = _originAix;
        this.originPostionAix = _aixY.transform.localPosition;

        this.aixYRotateOld = _aixY.localRotation.eulerAngles;
        this.aixXRotateOld = _aixX.localRotation.eulerAngles;
        this.aixZRotateOld = _aixZ.localRotation.eulerAngles;

        this.modelPos = _aixY.transform.localPosition;
        this.modelPosOld = _aixY.transform.localPosition;
        this.modelRotate = _aixY.transform.localRotation.eulerAngles + _aixX.transform.localRotation.eulerAngles + _aixZ.transform.localRotation.eulerAngles;
        this.nameAix = _modelObj.name;
    }


    /// <summary>
    /// 瞬间重置数据
    /// </summary>
    public void ResetRotateAix()
    {
        if (this.aixY == null || this.aixX == null || this.aixZ == null) return;
        this.aixY.localRotation = Quaternion.Euler(0, this.originRotateAix.y, 0);
        this.aixX.localRotation = Quaternion.Euler(this.originRotateAix.x, 0, 0);
        this.aixZ.localRotation = Quaternion.Euler(0, 0, this.originRotateAix.z);
    }

    /// <summary>
    /// Play 状态下 重置移动数据
    /// </summary>
    public void ResetPostionAix()
    {
        if (this.aixY == null || this.aixX == null || this.aixZ == null) return;
        this.aixY.localPosition = this.originPostionAix;
         this.modelPosOld = this.originPostionAix;
         this.modelPos = this.originPostionAix;
    }


#if UNITY_EDITOR

    /// <summary>
    /// 更新旋转数据
    /// </summary>
    public void UpdatedRotateAix()
    {
        if (this.aixY == null || this.aixX == null || this.aixZ == null) return;
        if (this.aixYRotateOld != (Vector3.up * this.modelRotate.y) || this.aixXRotateOld != (Vector3.right * this.modelRotate.x) || this.aixZRotateOld != (Vector3.forward * this.modelRotate.z))
        {
            this.aixY.localEulerAngles = Vector3.up * this.modelRotate.y;
            this.aixX.localEulerAngles = Vector3.right * this.modelRotate.x;
            this.aixZ.localEulerAngles = Vector3.forward * this.modelRotate.z;
            this.originRotateAix = new Vector3(this.modelRotate.x, this.modelRotate.y, this.modelRotate.z);
        }

    }

    /// <summary>
    /// Editor 状态下 调用更新移动数据
    /// </summary>
    public void UpdatedPostionAix()
    {
        if (this.modelPosOld != this.modelPos)
        {
            if(this.aixY != null)
            {
                this.aixY.localPosition = this.modelPos;
                this.modelPosOld = this.modelPos;
                this.originPostionAix = this.modelPos;
            }
        }
    }

#endif

    //public void UpdatedRotateAix(Quaternion _aixY, Quaternion _aixX, Quaternion _aixZ)
    //{

    //}

}
