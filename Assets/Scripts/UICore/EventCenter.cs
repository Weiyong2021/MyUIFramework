using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UICore;

public class EventCenter
{
    private static EventCenter instance = new EventCenter();
    private EventCenter() { }

    public static EventCenter Instance => instance;


    //key是要监听的事件类型   value是监听到事件后执行的委托们
    public Dictionary<EEventType, Action> dicEventType = new Dictionary<EEventType, Action>();

    //添加监听的方法
    public void AddListener(EEventType eventType, Action action)
    {
        if (!dicEventType.ContainsKey(eventType))
        {
            dicEventType.Add(eventType, null);

        }

        dicEventType[eventType] += action;
    }

    //取消指定的监听的方法
    public void RemoveListener(EEventType eventType, Action action)
    {
        if (dicEventType.ContainsKey(eventType))
        {
            dicEventType[eventType] -= action;
        }
    }

    //取消所有的监听方法
    public void RemoveAllListener()
    {
        dicEventType.Clear();//清空字典
    }


    //广播消息（派发事件）当触发这个事件的时候，就执行委托们
    public void Broadcast(EEventType eventType)
    {
        Action action;
        if (dicEventType.TryGetValue(eventType, out action))
        {
            if (action != null)
            {
                //action?.Invoke();   这样也可以。。。

                action();
            }
        }
    }



}
