using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace YProjectBase
{

    /// <summary>
    /// 资源加载模块
    /// </summary>
    public class ResMgr : BaseManager<ResMgr>
    {

        /// <summary>
        /// 预设列表 储存资源镜像
        /// </summary>
        private Hashtable resCache;
        public ResMgr()
        {
            resCache = new Hashtable();
        }

        /// <summary>
        /// 同步加载资源
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="pathName">资源路径</param>
        /// <returns></returns>
        public T load<T>(string pathName, bool isCache = false) where T : Object
        {
            T res = null;
            if (resCache.ContainsKey(pathName))
                res = resCache[pathName] as T;
            else
            {
                res = Resources.Load<T>(pathName);
                if (isCache)
                    resCache.Add(pathName, res);
            }
            if (res is GameObject)
                return GameObject.Instantiate(res);
            else
                return res;
            //return res;
        }

        /// <summary>
        /// 异步加载接口
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="name">地址</param>
        /// <param name="callback">方法</param>
        public void loadAsync<T>(string name, UnityAction<T> callback) where T : Object
        {
            MonoMgr.GetInstance().StartCoroutine(ReallyloadAsync(name, callback));
        }

        private IEnumerator ReallyloadAsync<T>(string name, UnityAction<T> callBack) where T : Object
        {
            ResourceRequest res = Resources.LoadAsync(name);
            yield return null;

            while (!res.isDone)
            {
                yield return null;
            }

            //资源动态加载完毕之后调用回调
            if (res.asset is GameObject)
                callBack(GameObject.Instantiate(res.asset) as T);
            else
                callBack(res.asset as T);
        }
    }


}
/*调用方法
1.同步加载
例子
GameObject obj = ResMgr.GetInstance().Load<GameObject>("文件地址");
obj.transform.localScale = Vector3.one*2;


2.异步加载
例子
1. 
ResMgr.GetInstance().loadAsync<GameObject>("文件地址",(obj)=>{
        obj.transform.localScale = Vector3.one*2;
});
2. 
ResMgr.GetInstance().loadAsync<GameObject>("文件地址",Text);
void Text(GameObject obj)
{
   obj.transform.localScale = Vector3.one*2;
}
*/
