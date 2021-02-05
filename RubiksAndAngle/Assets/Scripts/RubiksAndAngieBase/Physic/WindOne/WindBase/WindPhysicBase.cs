using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YProjectBase;

namespace RubiksAndAngie.RPhysic
{

    public abstract class WindPhysicBase : MonoBehaviour, IInit, IListener 
    {

        /// <summary>
        /// 风的模式
        /// </summary>
        public enum WindModeTypes
        {
            Directional,
            Line,
            Box,
            Sphere,
        }

        [Tooltip("风的模式")] [SerializeField]
        protected WindModeTypes mode = WindModeTypes.Directional;

        [Tooltip("风的影响距离")] [EnumHide("mode", 1, true)] [SerializeField]
        protected float distance = 10f;

        [Tooltip("风的影响范围")] [EnumHide("mode", 2, true)]  [SerializeField]
        protected Vector3 size = new Vector3(1,1,1);

        [Tooltip("风的影响半径")] [EnumHide("mode", 3, true)] [SerializeField]
        protected float radius = 1f;

        [Tooltip("风的主要强度")] [SerializeField]
        protected float mainStrength = 1f;


        [HideInInspector] [SerializeField]
        protected List<WindObjectBase> windObjectsList = new List<WindObjectBase>();
        private List<WindObjectBase> tempWindObjectsList = new List<WindObjectBase>();

        public WindModeTypes GetWindMode
        {
            get
            {
                return mode;
            }
        }
        public void SetWindMode(WindModeTypes _mode)
        {
            mode = _mode;
        }

        public float MainStrength
        {
            get
            {
                return mainStrength;
            }

            set
            {
                mainStrength = value >= 0f ? value : 1f;
            }
        }


        protected virtual void Awake()
        {
            Init();
        }
        public virtual void Init()
        {
            windObjectsList = GetAllWindObj();
            AddListener();
        }
        protected virtual void OnDestroy()
        {
            RemoveListener();
        }



        /// <summary>
        /// 为了选择风的计算模式
        /// </summary>
        protected abstract void SelectWindMode();
        /// <summary>
        /// 为了自定义风的计算模式
        /// </summary>
        protected void SelectWindMode(WindModeTypes _mode)
        {
            if(mode != _mode)
                 mode = _mode;
            SelectWindMode();
        }


        /// <summary>
        /// 为了获得全部受风影响的对象
        /// 返回列表
        /// </summary>
        /// <returns></returns>
        protected List<WindObjectBase> GetAllWindObj()
        {
            WindObjectBase[] winds = FindObjectsOfType<WindObjectBase>();
            if (winds == null || winds.Length <= 0) return null;

            windObjectsList.Clear();
            for (int i = 0; i < winds.Length; i++)
            {
                if(winds[i] != null)
                {
                    windObjectsList.Add(winds[i]);
                }
            }
            winds = null;
            return windObjectsList;
        }

        /// <summary>
        /// 更新风数据
        /// </summary>
        protected virtual void UpdatedWindPhysic()
        {
            SelectWindMode();
        }

        /// <summary>
        /// 设置全局风
        /// </summary>
        protected virtual void DirectionWind()
        {
            if (windObjectsList == null || windObjectsList.Count <= 0) return;
            for (int i = 0; i < windObjectsList.Count; i++)
            {
                if (windObjectsList[i] == null) continue;
                    windObjectsList[i].SetWindVelocity(transform, transform.forward * mainStrength);
            }
        }

        protected virtual void LineWind()
        {

        }



        public abstract void AddListener();
        public abstract void RemoveListener();

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            SelectDrawGizmos();
        }


        private void SelectDrawGizmos()
        {
            switch (mode)
            {
                case WindModeTypes.Directional:
                default:
                    DrawWindDirection();
                    break;
                case WindModeTypes.Line:
                    DrawLine();
                    break;
                case WindModeTypes.Box:
                    DrawBox();
                    break;
                case WindModeTypes.Sphere:
                    DrawSphere();
                    break;
            }
        }

        private void DrawWindDirection()
        {
            Vector3 backCenter = transform.position - transform.forward * 1.5f;
            Vector3 forwardCenter = transform.position + transform.forward * 1.5f;

            Gizmos.DrawLine(backCenter + transform.right * 0.5f, backCenter - transform.right * 0.5f);
            Gizmos.DrawLine(backCenter + transform.up * 0.5f, backCenter - transform.up * 0.5f);

            Gizmos.DrawLine(forwardCenter, transform.position + transform.up);
            Gizmos.DrawLine(forwardCenter, transform.position - transform.up);
            Gizmos.DrawLine(forwardCenter, transform.position + transform.right);
            Gizmos.DrawLine(forwardCenter, transform.position - transform.right);

            Gizmos.DrawLine(backCenter + transform.right * 0.5f, transform.position + transform.right * 0.5f);
            Gizmos.DrawLine(backCenter - transform.right * 0.5f, transform.position - transform.right * 0.5f);
            Gizmos.DrawLine(backCenter + transform.up * 0.5f, transform.position + transform.up * 0.5f);
            Gizmos.DrawLine(backCenter - transform.up * 0.5f, transform.position - transform.up * 0.5f);

            Gizmos.DrawLine(transform.position + transform.up, transform.position - transform.up);
            Gizmos.DrawLine(transform.position + transform.right, transform.position - transform.right);
        }
        private void DrawLine()
        {
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * distance);
        }
        private void DrawBox()
        {
            Gizmos.DrawWireCube(transform.position, size);
        }
        private void DrawSphere()
        {
            Gizmos.DrawWireSphere(transform.position, radius);
        }

#endif

    }

}

