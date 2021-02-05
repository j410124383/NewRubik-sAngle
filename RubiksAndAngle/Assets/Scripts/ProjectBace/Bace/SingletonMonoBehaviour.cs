using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YProjectBase
{
    /// <summary>
    /// 单例 MonoBehaviour 基类
    /// 泛型
    /// 单例模式
    /// 需要保证唯一性
    /// </summary>
    [DisallowMultipleComponent]
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public static T GetInstance()
        {
            if (instance == null)
            {
                GameObject obj = new GameObject(typeof(T).ToString());
                instance = obj.AddComponent<T>();
                //过场景不移除 往往存在于整个生命周期中
                GameObject.DontDestroyOnLoad(obj);
            }
            return instance;
        }



    }
}