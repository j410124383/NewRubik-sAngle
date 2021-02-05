using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace YProjectBase
{

    /// <summary>
    /// 事件中心模块
    /// 1.Dictionary 
    /// 2.委托
    /// 3.观察者模式
    /// </summary>
    public class EventCenter : BaseManager<EventCenter>
    {
        //key---------事件名字
        //value------监听的事件
        private Dictionary<string, IEventInfo> eventDic = new Dictionary<string, IEventInfo>();

        /// <summary>
        /// 添加事件监听(带参数)
        /// </summary>
        /// <param name="name">事件的名字</param>
        /// <param name="action">准备用来处理事件的函数</param>
        public void AddEventListener<T>(string name, UnityAction<T> action)
        {
            //判断是否存在事件
            if (eventDic.ContainsKey(name))
                (eventDic[name] as EventInfo<T>).actions += action;
            else
                eventDic.Add(name, new EventInfo<T>(action));
        }

        /// <summary>
        /// 添加事件监听(不带参数)
        /// </summary>
        /// <param name="name">事件的名字</param>
        /// <param name="action">准备用来处理事件的函数</param>
        public void AddEventListener(string name, UnityAction action)
        {
            //判断是否存在事件
            if (eventDic.ContainsKey(name))
                (eventDic[name] as EventInfo).actions += action;
            else
                eventDic.Add(name, new EventInfo(action));
        }

        /// <summary>
        /// 删除事件监听(带参数)
        /// </summary>
        /// <param name="name">事件的名字</param>
        /// <param name="action">准备用来处理事件的函数</param>
        public void RemoveEventListener<T>(string name, UnityAction<T> action)
        {
            //判断是否存在事件
            if (eventDic.ContainsKey(name))
                (eventDic[name] as EventInfo<T>).actions -= action;
        }

        /// <summary>
        /// 删除事件监听(不带参数)
        /// </summary>
        /// <param name="name">事件的名字</param>
        /// <param name="action">准备用来处理事件的函数</param>
        public void RemoveEventListener(string name, UnityAction action)
        {
            //判断是否存在事件
            if (eventDic.ContainsKey(name))
                (eventDic[name] as EventInfo).actions -= action;
        }

        /// <summary>
        /// 事件触发(带参数)
        /// </summary>
        /// <param name="name">事件的名字</param>
        /// <param info="info">事件的参数</param>
        public void EventTrigger<T>(string name, T info)
        {
            //判断是否存在事件
            if (eventDic.ContainsKey(name))
                (eventDic[name] as EventInfo<T>).actions?.Invoke(info);
        }

        /// <summary>
        /// 事件触发(不带参数)
        /// </summary>
        /// <param name="name">事件的名字</param>
        /// <param info="info">事件的参数</param>
        public void EventTrigger(string name)
        {
            //判断是否存在事件
            if (eventDic.ContainsKey(name))
                (eventDic[name] as EventInfo).actions?.Invoke();
        }

        /// <summary>
        /// 清空事件中心
        /// </summary>
        public void ClearEvent()
        {
            eventDic.Clear();
        }

    }

    /// <summary>
    /// 空接口
    /// </summary>
    public interface IEventInfo
    {

    }
    /// <summary>
    /// 带参数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EventInfo<T> : IEventInfo
    {
        public UnityAction<T> actions;

        public EventInfo(UnityAction<T> action)
        {
            actions += action;
        }
    }
    /// <summary>
    /// 不带参数
    /// </summary>
    public class EventInfo : IEventInfo
    {
        public UnityAction actions;

        public EventInfo(UnityAction action)
        {
            actions += action;
        }
    }
}