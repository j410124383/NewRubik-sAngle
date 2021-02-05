using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RubiksAndAngie
{

    [System.Serializable]
    public class ShadowAixBase
    {
        [HideInInspector] [SerializeField] string nameShadowAix;

        [Tooltip("控制影子对象")] [SerializeField] GameObject controllerModelObj;
        [Tooltip("影子模型对象")] [SerializeField] GameObject modelObj;
        [Tooltip("影子模型父对象（aixY）")] [SerializeField] GameObject aixObj;
        [Tooltip("影子模型父对象 Z 轴位置 （影子位置）")] [SerializeField] float shadowOffsetPosZ;
        [Tooltip("影子模型变化控制器")] [SerializeField] DynaMesh dynaMesh;

        [HideInInspector] [SerializeField] Transform aixY;
        [HideInInspector] [SerializeField] Transform aixX;
        [HideInInspector] [SerializeField] Transform aixZ;

        public ShadowAixBase(GameObject _controllerModelObj,GameObject _modelObj, Transform _aixY, Transform _aixX, Transform _aixZ, float _shadowOffsetPosZ)
        {
            if (_modelObj.GetComponent<DynaMesh>() == null)
            {
                this.dynaMesh = _modelObj.AddComponent<DynaMesh>();
            }
            else this.dynaMesh = _modelObj.GetComponent<DynaMesh>();

            this.dynaMesh.InitDynaMesh();

            this.controllerModelObj = _controllerModelObj;
            this.modelObj = _modelObj;
            this.aixObj = _aixY.gameObject;

            this.aixY = _aixY;
            this.aixX = _aixX;
            this.aixZ = _aixZ;

            this.shadowOffsetPosZ = _shadowOffsetPosZ;
            this.nameShadowAix = _modelObj.name;
        }

        public GameObject ControllerModelObj { get { return controllerModelObj; } }
        public GameObject ModelObj { get { return modelObj; } }
        public GameObject AixObj { get { return aixObj; } }

        public Transform AixY { get { return aixY; } }
        public Transform AixX { get { return aixX; } }
        public Transform AixZ { get { return aixZ; } }
        public float ShadowOffsetPosZ { get { return shadowOffsetPosZ; } set { shadowOffsetPosZ = value; } }



        /// <summary>
        /// 更新旋转数据
        /// </summary>
        /// <param name="_aixY">aixY 输入 Y轴 的旋转值</param>
        /// <param name="_aixX">aixX 输入 X轴 的旋转值</param>
        /// <param name="_aixZ">aixZ 输入 Z轴 的旋转值</param>
        public void UpdatedRotateAix(Transform _aixY, Transform _aixX, Transform _aixZ)
        {
            if (this.aixY == null || this.aixX == null || this.aixZ == null) return;

            if (this.aixY.rotation != _aixY.rotation || this.aixX.rotation != _aixX.rotation || this.aixZ.rotation != _aixZ.rotation)
            {
                UpdatedDynaMesh();
                this.aixY.rotation = _aixY != null ? _aixY.rotation : this.aixY.rotation;
                this.aixX.rotation = _aixX != null ? _aixX.rotation : this.aixX.rotation;
                this.aixZ.rotation = _aixZ != null ? _aixZ.rotation : this.aixZ.rotation;
            }
            //Debug.Log(" this.aixZ " + this.aixZ);
        }

        /// <summary>
        /// 更新位置数据
        /// </summary>
        /// <param name="_transPos">输入 XY轴 （世界坐标）</param>
        public void UpdatedPostionAix(Transform _transPos)
        {

            if (this.aixY == null || this.aixX == null || this.aixZ == null) return;

            this.aixY.position = _transPos != null ? _transPos.position : this.aixY.position;
            this.aixY.localPosition -= Vector3.forward * this.aixY.localPosition.z;
            this.aixY.localPosition += Vector3.forward * this.shadowOffsetPosZ;
        }

        /// <summary>
        /// 更新深度值
        /// </summary>
        public void UpdatedPostionOffsetZ(float _offsetPosZ)
        {
            if (this.aixY == null) return;

            if (this.shadowOffsetPosZ != _offsetPosZ)
            {
                this.shadowOffsetPosZ = _offsetPosZ;
                this.aixY.localPosition -= Vector3.forward * this.aixY.localPosition.z;
                this.aixY.localPosition += Vector3.forward * this.shadowOffsetPosZ;
            }
        }

        /// <summary>
        /// 更新影子碰撞体
        /// </summary>
        public void UpdatedDynaMesh()
        {
            dynaMesh.LateUpdatedCollider();
        }

    }

}