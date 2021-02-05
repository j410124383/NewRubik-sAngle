using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace YProjectBase
{

    /// <summary>
    /// mono管理
    /// 1.声明周期函数
    /// 2.事件
    /// </summary>
    public class MonoController : MonoBehaviour
    {
        private event UnityAction updateEvent;
        private event UnityAction fixedUpdateEvent;
        private event UnityAction lateUpdateEvent;

        private List<UnityAction> updateEventList = new List<UnityAction>();
        private List<UnityAction> fixedUpdateEventList = new List<UnityAction>();
        private List<UnityAction> lateUpdateEventList = new List<UnityAction>();

        private void Start()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        private void Update()
        {
            if (updateEvent != null)
                updateEvent();
        }

        private void FixedUpdate()
        {
            if (fixedUpdateEvent != null)
                fixedUpdateEvent();
        }

        private void LateUpdate()
        {
            if (lateUpdateEvent != null)
                lateUpdateEvent();
        }


        /// <summary>
        /// 添加需要帧更新的方法
        /// </summary>
        /// <param name="functon">方法</param>
        public void AddUpdateListener(UnityAction functon)
        {
            if (!updateEventList.Contains(functon))
            {
                 updateEvent += functon;
                 updateEventList.Add(functon);           
            }
        }

        /// <summary>
        /// 移除需要帧更新的方法
        /// </summary>
        /// <param name="functon">方法</param>
        public void RemoveUpdateListener(UnityAction functon)
        {
            if (updateEventList.Contains(functon))
            {
                updateEvent -= functon;
                updateEventList.Remove(functon);
            }
        }

        /// <summary>
        /// 添加需要物理帧更新的方法
        /// </summary>
        /// <param name="functon">方法</param>
        public void AddFixedUpdateListener(UnityAction functon)
        {
            if (!fixedUpdateEventList.Contains(functon))
            {
                fixedUpdateEvent += functon;
                fixedUpdateEventList.Add(functon);             
            }
        }

        /// <summary>
        /// 移除需要物理帧更新的方法
        /// </summary>
        /// <param name="functon">方法</param>
        public void RemoveFixedUpdateListener(UnityAction functon)
        {
            if (fixedUpdateEventList.Contains(functon))
            {
                fixedUpdateEvent -= functon;
                fixedUpdateEventList.Remove(functon);
            }
        }

        /// <summary>
        /// 添加需要最后一帧帧更新的方法
        /// </summary>
        /// <param name="functon">方法</param>
        public void AddLateUpdateListener(UnityAction functon)
        {
            if (!lateUpdateEventList.Contains(functon))
            {
                lateUpdateEvent += functon;
                lateUpdateEventList.Add(functon);
            }
        }

        /// <summary>
        /// 移除需要最后一帧更新的方法
        /// </summary>
        /// <param name="functon">方法</param>
        public void RemoveLateUpdateListener(UnityAction functon)
        {
            if (lateUpdateEventList.Contains(functon))
            {
                lateUpdateEvent -= functon;
                lateUpdateEventList.Remove(functon);
            }
        }



        /// <summary>
        /// 移除所有帧更新事件
        /// </summary>
        public void ClearUpdateEvent()
        {
            if(updateEventList != null)
            {
                for (int i = 0; i < updateEventList.Count; i++)
                {
                    if (updateEventList[i] == null) continue;
                    updateEvent -= updateEventList[i];
                }
                updateEventList.Clear();
            }
        }

        /// <summary>
        /// 移除所有物理帧更新事件
        /// </summary>
        public void ClearFixedUpdateEvent()
        {
            if (fixedUpdateEventList != null)
            {
                for (int i = 0; i < fixedUpdateEventList.Count; i++)
                {
                    if (fixedUpdateEventList[i] == null) continue;
                    fixedUpdateEvent -= fixedUpdateEventList[i];
                }

                fixedUpdateEventList.Clear();
            }
        }

        /// <summary>
        /// 移除所有最后一帧更新事件
        /// </summary>
        public void ClearLateUpdateEvent()
        {
            if (lateUpdateEventList != null)
            {
                for (int i = 0; i < lateUpdateEventList.Count; i++)
                {
                    if (lateUpdateEventList[i] == null) continue;
                    lateUpdateEvent -= lateUpdateEventList[i];
                }
                lateUpdateEventList.Clear();
            }
        }



        private void OnApplicationQuit()
        {
          
        }

    }

}