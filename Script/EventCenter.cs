using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventCenter : MonoBehaviour
{
    public interface IEventInfo { }
    // 实现两个个参数
    public class EventInfo<T,V> : IEventInfo
    {
        public UnityAction<T,V> actions;
        public EventInfo(UnityAction<T,V> action)
        {
            actions += action;
        }
    }
    // 实现一个参数
    public class EventInfo<T> : IEventInfo
    {
        public UnityAction<T> actions;
        public EventInfo(UnityAction<T> action)
        {
            actions += action;
        }
    }
    // 实现无参
    public class EventInfo : IEventInfo
    {
        public UnityAction actions;
        public EventInfo(UnityAction action)
        {
            actions += action;
        }
    }
    
    // key对应事件的名字，value对应的是监听这个事件对应的委托函数
    private Dictionary<string, IEventInfo> eventDic = new Dictionary<string, IEventInfo>();
    public static EventCenter instance;

    private void Awake()
    {
        instance = this;
    }

    // 添加事件监听，两个参数的
    public void AddEventListener<T,V>(string name, UnityAction<T,V> action)
    {
        if (eventDic.ContainsKey(name))
            (eventDic[name] as EventInfo<T,V>).actions += action;
        else
            eventDic.Add(name, new EventInfo<T,V>(action));
    }
    
    // 添加事件监听，一个参数的
    public void AddEventListener<T>(string name, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(name))
            (eventDic[name] as EventInfo<T>).actions += action;
        else
            eventDic.Add(name, new EventInfo<T>(action));
    }

    // 添加事件监听，无参数的
    public void AddEventListener(string name, UnityAction action)
    {
        if (eventDic.ContainsKey(name))
            (eventDic[name] as EventInfo).actions += action;
        else
            eventDic.Add(name, new EventInfo(action));
    }

    // 事件触发，无参的
    public void EventTrigger(string name)
    {
        if (eventDic.ContainsKey(name))
            (eventDic[name] as EventInfo).actions?.Invoke();
    }

    //事件触发，一个参数的
    public void EventTrigger<T>(string name, T info)
    {
        if (eventDic.ContainsKey(name))
            (eventDic[name] as EventInfo<T>).actions?.Invoke(info);
    }
    //事件触发，两个个参数的
    public void EventTrigger<T,V>(string name, T info ,V info2)
    {
        if (eventDic.ContainsKey(name))
            (eventDic[name] as EventInfo<T,V>).actions?.Invoke(info ,info2);
    }

    //移除监听，无参的
    public void RemoveEventListener(string name, UnityAction action)
    {
        if (eventDic.ContainsKey(name))
            (eventDic[name] as EventInfo).actions -= action;
    }

    //移除监听，一个参数的
    public void RemoveEventListener<T>(string name, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(name))
            (eventDic[name] as EventInfo<T>).actions -= action;
    }
    
    //移除监听，两个个参数的
    public void RemoveEventListener<T,V>(string name, UnityAction<T,V> action)
    {
        if (eventDic.ContainsKey(name))
            (eventDic[name] as EventInfo<T,V>).actions -= action;
    }

    public void Clear()
    {
        eventDic.Clear();
    }
}
