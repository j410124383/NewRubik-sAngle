using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace YProjectBase
{

    /// <summary>
    /// 对象池模块
    /// 1.Dictonary list
    /// 2.GameObject Resouces
    /// </summary>
    public class PoolMgr : BaseManager<PoolMgr>
    {
        public Dictionary<string, PoolDate> poolDic = new Dictionary<string, PoolDate>();

        //父对象
        private GameObject poolObj;

        /// <summary>
        /// 获得对象池中的闲置对象
        /// </summary>
        /// <param name="name">地址</param>
        /// <returns></returns>
        public void GetObj(string name, UnityAction<GameObject> callback)
        {
            //GameObject obj = null;
            //判断缓存池中是否有缓存空间和闲置对象
            if (poolDic.ContainsKey(name) && poolDic[name].poolList.Count > 0)
            {
                //obj = poolDic[name].GetObj();
                callback?.Invoke(poolDic[name].GetObj());
            }
            else
            {
                ResMgr.GetInstance().loadAsync<GameObject>(name, (o) =>
                {
                    o.name = name;
                    callback?.Invoke(o);
                });
                //obj = GameObject.Instantiate(Resources.Load<GameObject>(name));
            }

        }

        /// <summary>
        /// 闲置对象放入对象池中
        /// </summary>
        /// <param name="name">地址</param>
        /// <param name="obj">对象</param>
        public void PushObj(string name, GameObject obj)
        {

            if (poolObj == null)
                poolObj = new GameObject("Pool");

            //存在缓存空间
            if (poolDic.ContainsKey(name))
                poolDic[name].PushObj(obj);
            //不存在缓存空间
            else
                poolDic.Add(name, new PoolDate(obj, poolObj));
        }

        /// <summary>
        /// 场景切换使用
        /// </summary>
        public void ClearPool()
        {
            poolDic.Clear();
            poolObj = null;
        }

    }

    /// <summary>
    /// 对象池数据类
    /// </summary>
    public class PoolDate
    {
        //父对象
        public GameObject parentObj;
        //对象清单
        public List<GameObject> poolList;

        public PoolDate(GameObject obj, GameObject poolObj)
        {
            parentObj = new GameObject(obj.name);
            parentObj.transform.parent = poolObj.transform;
            poolList = new List<GameObject>() { };
            obj.transform.SetParent(parentObj.transform);
            PushObj(obj);
        }

        /// <summary>
        /// 获得缓存池中的闲置对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public GameObject GetObj()
        {
            GameObject obj = null;
            //获得缓存池中的闲置对象
            obj = poolList[0];
            poolList.RemoveAt(0);
            obj.SetActive(true);
            obj.transform.SetParent(null);

            return obj;
        }

        /// <summary>
        /// 把对象放入对象池中
        /// </summary>
        /// <param name="obj"></param>
        public void PushObj(GameObject obj)
        {
            obj.SetActive(false);
            //设置添加到对象池
            poolList.Add(obj);
            //设置父对象
            obj.transform.SetParent(parentObj.transform);
        }
    }

}
/*调用方法
1.异步加载
例子 
PoolMgr.GetInstance().GetObj("文件地址",(o)=>{
        o.transform.localScale = Vector3.one*2;
});

PoolMgr.GetInstance().PushObj("文件地址",对象);
PoolMgr.GetInstance().PushObj(this.gameObject.name,this.gameObject.name);
*/
