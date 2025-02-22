using System.Collections.Generic;
using Base;
using UnityEngine;
using UnityEngine.Events;

namespace EventCenter
{
    public abstract class EventInfoBase{}

    public class EventInfo<T> : EventInfoBase
    {
        public UnityAction<T> Actions;

        public EventInfo(UnityAction<T> action)
        {
            Actions += action;
        }
    }
    
    public class EventInfo: EventInfoBase
    {
        public UnityAction Actions;
     
        public EventInfo(UnityAction action)
        {
            Actions += action;
        }
    }
    public class EventCenter : WithoutMono<EventCenter>
    {
        //用字典记录函数（委托）
        private Dictionary<string,EventInfoBase> _eventDic = new Dictionary<string, EventInfoBase>();
        //私有构造函数，避免在外部被实例化
        private EventCenter()
        {
            
        } 
        //将事件存入字典的方法
        public void DOAddListener(string eventName, UnityAction func)
        {
            if(_eventDic.ContainsKey(eventName))
                (_eventDic[eventName] as EventInfo).Actions += func;
            else
            {
                _eventDic.Add(eventName, new EventInfo(func));
            }
        }

        public void DOAddListener<T>(string eventName, UnityAction<T> func)
        {
            if (_eventDic.ContainsKey(eventName))
            {
                (_eventDic[eventName] as EventInfo<T>).Actions += func;
            }
            else
            {
                _eventDic.Add(eventName,new EventInfo<T>(func));
            }
        }
        //使用字典里的事件
        public void DOEventTrigger(string eventName)
        {
            if (_eventDic.ContainsKey(eventName))
            {
                (_eventDic[eventName] as EventInfo).Actions?.Invoke();
            }
        }
        
        public void DOEventTrigger<T>(string eventName,T func)
        {
            if (_eventDic.ContainsKey(eventName))
            {
                (_eventDic[eventName] as EventInfo<T>).Actions?.Invoke(func);
            }
        }
        //移除字典里事件的方法
        public void DORemoveListener(string eventName, UnityAction func)
        {
            if (_eventDic.ContainsKey(eventName))
            {
                (_eventDic[eventName] as EventInfo).Actions -= func;
            }
        }
        
        public void DORemoveListener<T>(string eventName, UnityAction<T> func)
        {
            if (_eventDic.ContainsKey(eventName))
            {
                (_eventDic[eventName] as EventInfo<T>).Actions -= func;
            }
        }
        //移除字典里所有事件
        public void DOClear()
        {
            _eventDic.Clear();
        }
        //移除字典里指定名字的事件
        public void DOClear(string eventName)
        {
            if (_eventDic.ContainsKey(eventName))
            {
                _eventDic.Remove(eventName);
            }
        }
    }
}
