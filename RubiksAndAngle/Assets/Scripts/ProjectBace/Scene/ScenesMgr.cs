using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace YProjectBase
{

    /// <summary>
    /// 场景切换模块
    /// 异步加载 协成 委托
    /// </summary>
    public class ScenesMgr : BaseManager<ScenesMgr>
    {

        /// <summary>
        /// 切换场景 同步加载
        /// </summary>
        /// <param name="name">场景名字</param>
        public void LoadScene(string name, UnityAction fuction)
        {
            if (IsValidCanLoadScene(name))
            {
                SceneManager.LoadScene(name);

                fuction();
            }
        }

        /// <summary>
        /// 切换场景 异步加载
        /// </summary>
        /// <param name="name">场景名字</param>
        /// <param name="fuction"></param>
        public void LoadSceneAsync(string name, UnityAction<AsyncOperation> fuction)
        {
            if (IsValidCanLoadScene(name))
            {
                MonoMgr.GetInstance().StartCoroutine(ReallyLoadSceneAsync(name, fuction));
            }
        }

        IEnumerator ReallyLoadSceneAsync(string name, UnityAction<AsyncOperation> fuction)
        {
            yield return null;

            AsyncOperation AO = SceneManager.LoadSceneAsync(name);

            AO.allowSceneActivation = false;
            fuction(AO);

            while (!AO.isDone)
            {
                //事件中心向外分发场景进度
                EventCenter.GetInstance().EventTrigger<float>("Loading", AO.progress);

                yield return AO.progress;
            }

            yield return AO;
        }

        /// <summary>
        /// 强制GC
        /// </summary>
        public void ToGC()
        {
            System.GC.Collect();
        }


        /// <summary>
        /// 验证场景是否存在
        /// </summary>
        /// <param name="scenename"></param>
        /// <returns></returns>
        public bool IsValidCanLoadScene(string scenename)
        {
            if (Application.CanStreamedLevelBeLoaded(scenename))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }

}