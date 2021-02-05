using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

namespace YProjectBase
{

    /// <summary>
    /// mono接口添加外部帧更新
    /// 1.AddUpdateListener
    /// 2.RemoveUpdateListener
    /// mono接口添加外部协成
    /// </summary>
    public class MonoMgr : BaseManager<MonoMgr>
    {
        private MonoController controller;

        public MonoMgr()
        {
            if (controller == null)
            {
                GameObject obj = new GameObject("MonoController");
                controller = obj.AddComponent<MonoController>();
            }
        }



        /// <summary>
        /// 添加需要帧更新的方法
        /// </summary>
        /// <param name="functon">方法</param>
        public void AddUpdateListener(UnityAction functon)
        {
            controller.AddUpdateListener(functon);
        }

        /// <summary>
        /// 移除需要帧更新的方法
        /// </summary>
        /// <param name="functon">方法</param>
        public void RemoveUpdateListener(UnityAction functon)
        {
            controller.RemoveUpdateListener(functon);
        }


        /// <summary>
        /// 添加需要物理帧更新的方法
        /// </summary>
        /// <param name="functon">方法</param>
        public void AddFixedUpdateListener(UnityAction functon)
        {
            controller.AddFixedUpdateListener(functon);
        }

        /// <summary>
        /// 移除需要物理帧更新的方法
        /// </summary>
        /// <param name="functon">方法</param>
        public void RemoveFixedUpdateListener(UnityAction functon)
        {
            controller.RemoveFixedUpdateListener(functon);
        }


        /// <summary>
        /// 添加需要最后一帧帧更新的方法
        /// </summary>
        /// <param name="functon">方法</param>
        public void AddLateUpdateListener(UnityAction functon)
        {
            controller.AddLateUpdateListener(functon);
        }


        /// <summary>
        /// 移除需要最后一帧更新的方法
        /// </summary>
        /// <param name="functon">方法</param>
        public void RemoveLateUpdateListener(UnityAction functon)
        {
            controller.RemoveLateUpdateListener(functon);
        }



        #region 协成接口
        public Coroutine StartCoroutine(string methodName)
        {
            return controller.StartCoroutine(methodName);
        }

        public Coroutine StartCoroutine(IEnumerator routine)
        {
            return controller.StartCoroutine(routine);
        }

        public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value)
        {
            return controller.StartCoroutine(methodName, value);
        }

        public Coroutine StartCoroutine_Auto(IEnumerator routine)
        {
            return controller.StartCoroutine(routine);
        }

        public void StopAllCoroutines()
        {
            controller.StopAllCoroutines();
        }

        public void StopCoroutine(IEnumerator routine)
        {
            controller.StopCoroutine(routine);
        }

        public void StopCoroutine(Coroutine routine)
        {
            controller.StopCoroutine(routine);
        }

        public void StopCoroutine(string methodName)
        {
            controller.StopCoroutine(methodName);
        }
        #endregion
    }

}