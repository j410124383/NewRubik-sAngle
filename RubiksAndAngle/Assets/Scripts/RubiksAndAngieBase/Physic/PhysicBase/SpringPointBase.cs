using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YProjectBase;

namespace RubiksAndAngie
{

    public abstract class SpringPointBase : MonoBehaviour, IInit, IListener
    {
        public enum ColliderShapeTypes
        {
            Box,
            Sphere,
        }

        protected enum InputNormalizeTypes
        {
            DefaultNormal,
            Vector,
            Trans,
            GameObj,
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        [Header("Collider Setting (碰撞体设置)")]

        [Tooltip("碰撞体形状")] [SerializeField]
        protected ColliderShapeTypes eColliderShape = ColliderShapeTypes.Box;
        [Tooltip("碰撞体中心点")] [SerializeField] protected Vector3 vCenter;

        [Tooltip("Box 碰撞体大小")] [EnumHide("eColliderShape", 0, true)] [SerializeField]
        protected Vector3 vSize = new Vector3(1, 1, 1);
        [Tooltip("Sphere 碰撞体半径")] [EnumHide("eColliderShape", 1, true)] [SerializeField]
        protected float fRadius = 0.5f;

        [Tooltip("碰撞体层")] [SerializeField] protected LayerMask colliderLayer;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Header("Sprint Setting (弹射设置)")]

        [Tooltip("单位方向输入方式")] [SerializeField]
        protected InputNormalizeTypes normalizeTypes = InputNormalizeTypes.DefaultNormal;

        [Tooltip("输入作为单位方向的对象Trans")] [EnumHide("normalizeTypes", 3, true)] [SerializeField]
        protected GameObject oSpringNormalizeObj;
        [Tooltip("输入作为单位方向的对象Trans")] [EnumHide("normalizeTypes", 2, true)] [SerializeField]
        protected Transform tSpringNormalizeTrans;
        [Tooltip("弹射单位方向")] [EnumHide("normalizeTypes", 1, false)] [SerializeField] 
        protected Vector3 vSpringNormalize;
        [Tooltip("弹射力量")] [SerializeField]
        protected float fSpringForce = 100f;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public ColliderShapeTypes GetColliderShape{ get { return eColliderShape; } }
        public Vector3 Center { get { return vCenter; } set { vCenter = value; } }

        public Vector3 Size { get { return vSize; } set { vSize = value; } }
        public float Radius { get { return fRadius; } set { fRadius = value; } }

        public Vector3 SpringNormalize { get { return vSpringNormalize; } set { vSpringNormalize = value.normalized; } }
        public float SpringForce { get { return fSpringForce; } set { fSpringForce = value; } }

        public Vector3 GetSpringNormalized() { return vSpringNormalize.normalized; }


        protected virtual void OnValidate()
        {
            vSpringNormalize = GetNormalized(normalizeTypes);
        }

        protected Vector3 GetNormalized(InputNormalizeTypes _normalizeTypes)
        {
            switch (_normalizeTypes)
            {
                case InputNormalizeTypes.DefaultNormal:
                default:
                    return transform.up.normalized;

                case InputNormalizeTypes.Vector:
                    return vSpringNormalize;

                case InputNormalizeTypes.Trans:
                    if (tSpringNormalizeTrans)
                        return tSpringNormalizeTrans.up.normalized;
                    else
                        return transform.up.normalized;

                case InputNormalizeTypes.GameObj:
                    if (oSpringNormalizeObj)
                        return oSpringNormalizeObj.transform.up.normalized;
                    else
                        return transform.up.normalized;
            }
        }

        private void Awake()
        {
            Init();
        }

        public virtual void Init()
        {
            vSpringNormalize.Normalize();
            AddListener();
        }

        private void OnDestroy()
        {
            RemoveListener();
        }


        public virtual void FixedUpatedSpring()
        {
            if (IsCheckPhysic())
            {
                ToShootOff();
            }
        }

        public virtual void ToShootOff()
        {
            Collider[] colliders = OverlapColliders();
            if (colliders == null || colliders.Length <= 0) return;

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] == null || MyMathf.IsInLayerMask(colliders[i].attachedRigidbody.gameObject, colliderLayer) == false) continue;
                ToShootOff(colliders[i].attachedRigidbody);
                break;
            }
        }
        public void ToShootOff(Rigidbody _rig)
        {
            if (_rig == null) return;
            _rig.velocity = Vector3.zero;
            _rig.velocity += GetSpringNormalized() * SpringForce  * Time.fixedDeltaTime;
        }


        protected bool IsCheckBox()
        {
           return Physics.CheckBox(vCenter + transform.position, (vSize - Vector3.one) + transform.lossyScale, transform.rotation, colliderLayer, QueryTriggerInteraction.Ignore);
        }
        protected bool IsCheckSphere()
        {
            return Physics.CheckSphere(vCenter + transform.position, fRadius, colliderLayer, QueryTriggerInteraction.Ignore);
        }
        protected bool IsCheckPhysic()
        {
            switch (eColliderShape)
            {
                case ColliderShapeTypes.Box:
                default:
                    return IsCheckBox();
                case ColliderShapeTypes.Sphere:
                    return IsCheckSphere();
            }
        }


        protected Collider[] OverlapBox()
        {
            return Physics.OverlapBox(vCenter + transform.position, (vSize - Vector3.one) + transform.lossyScale, transform.rotation, colliderLayer);
        }
        protected Collider[] OverlapSphere()
        {
            return Physics.OverlapSphere(vCenter + transform.position, fRadius, colliderLayer);
        }
        protected Collider[] OverlapColliders()
        {
            switch (eColliderShape)
            {
                case ColliderShapeTypes.Box:
                default:
                    return OverlapBox();
                case ColliderShapeTypes.Sphere:
                    return OverlapSphere();
            }
        }


        public virtual void AddListener()
        {
            MonoMgr.GetInstance().AddFixedUpdateListener(FixedUpatedSpring);
        }
        public virtual void RemoveListener()
        {
            MonoMgr.GetInstance().RemoveFixedUpdateListener(FixedUpatedSpring);
        }

        #region DrawGizemos
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if(vSpringNormalize != GetNormalized(normalizeTypes))
                vSpringNormalize = GetNormalized(normalizeTypes);
            Gizmos.color = Color.red;
            Gizmos.matrix = transform.localToWorldMatrix;
            switch (eColliderShape)
            {
                case ColliderShapeTypes.Box:
                default:
                    DrawBox(vCenter, vSize);
                    break;
                case ColliderShapeTypes.Sphere:
                    DrawSphere(vCenter, fRadius);
                    break;
            }
        }
        private void DrawBox(Vector3 _center, Vector3 _size)
        {           
            Gizmos.DrawWireCube(_center,  (_size - Vector3.one) + transform.lossyScale);
        }
        private void DrawSphere(Vector3 _center, float _size)
        {
            Gizmos.DrawWireSphere(_center, _size);
        }
#endif
        #endregion

    }

}
