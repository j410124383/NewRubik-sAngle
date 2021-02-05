using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace YProjectBase
{

    public enum UI_layer
    {
        Top,
        Mid,
        Down,
        System
    }


    /// <summary>
    /// UI面板管理器
    /// </summary>
    public class UIManager : BaseManager<UIManager>
    {

        private Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();

        private Transform top;
        private Transform mid;
        private Transform down;
        private Transform system;

        private RectTransform canvas;

        public UIManager()
        {
            GameObject obj = ResMgr.GetInstance().load<GameObject>("UI/Canvas");
            canvas = obj.transform as RectTransform;
            GameObject.DontDestroyOnLoad(obj);

            top = canvas.Find("Top");
            mid = canvas.Find("Mid");
            down = canvas.Find("Down");
            system = canvas.Find("System");

            obj = ResMgr.GetInstance().load<GameObject>("UI/EventSystem");
            GameObject.DontDestroyOnLoad(obj);
        }

        //canvas父对象
        public RectTransform GetCanvas
        {
            get
            {
                return canvas;
            }
        }

        /// <summary>
        /// 获得层级父对象
        /// </summary>
        /// <param name="layer"></param>
        /// <returns></returns>
        public Transform GetLayerFather(UI_layer layer)
        {
            switch (layer)
            {
                case UI_layer.Top:
                    return this.top;
                case UI_layer.Mid:
                    return this.mid;
                case UI_layer.Down:
                    return this.down;
                case UI_layer.System:
                    return this.system;
            }
            return null;
        }



        /// <summary>
        /// 显示面板
        /// </summary>
        /// <typeparam name="T">面板基类类型</typeparam>
        /// <param name="panelName">面板名称</param>
        /// <param name="layer">显示在哪一层</param>
        /// <param name="callback">最终需要执行的函数</param>
        public void ShowPanel<T>(string panelName, UI_layer layer = UI_layer.Mid, UnityAction<T> callback = null) where T : BasePanel
        {

            if (panelDic.ContainsKey(panelName))
            {
                panelDic[panelName].ShowUI();

                if (callback != null)
                    callback(panelDic[panelName] as T);

                return;
            }

            ResMgr.GetInstance().loadAsync<GameObject>("UI/" + panelName, (obj) =>
            {

                //把父对象设置为canvas
                //设置到相应位置
                Transform father = mid;

                Debug.Log(system.gameObject);
                switch (layer)
                {
                    case UI_layer.Top:
                        father = top;
                        break;
                    case UI_layer.Down:
                        father = down;
                        break;
                    case UI_layer.System:
                        father = system;
                        break;
                }
                //设置父对象
                obj.transform.SetParent(father);

                //obj.name = panelName;

                //设置相对位置和大小
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localScale = Vector3.zero;

                (obj.transform as RectTransform).offsetMax = Vector2.zero;
                (obj.transform as RectTransform).offsetMin = Vector2.zero;

                T panel = obj.GetComponent<T>();

                if (callback != null)
                    callback(panel);

                panel.ShowUI();

                panelDic.Add(panelName, panel);
            });
        }

        /// <summary>
        /// 隐藏面板
        /// </summary>
        /// <param name="panelName">面板名称</param>
        public void HidePanel(string panelName)
        {
            if (panelDic.ContainsKey(panelName))
            {
                panelDic[panelName].HideUI();

                GameObject.Destroy(panelDic[panelName].gameObject);
                panelDic.Remove(panelName);
            }
        }

        /// <summary>
        /// 获得面板
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="panelName">面板名称</param>
        /// <returns></returns>
        public T GetPanel<T>(string panelName) where T : BasePanel
        {
            if (panelDic.ContainsKey(panelName))
            {
                return panelDic[panelName] as T;
            }

            return null;
        }

    }

}

//调用方法
//创建显示
//UIManager.GetInstance().ShowPanel<类>("类名字",UI_layer.位置,方法);
//UIManager.GetInstance().ShowPanel<T>("T",UI_layer.Mid,function);

//移除隐藏
//UIManager.GetInstance().HidePanel("类名字");
//UIManager.GetInstance().HidePanel("T");

