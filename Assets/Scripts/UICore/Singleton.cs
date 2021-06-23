using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//抽象类
public abstract class Singleton<T> where T:new()
{
    //如果要实现不继承于Mono的单利的模式，就可以直接继承这个Singleton
    private static T instance;

    static object _lock=new object();

    public static T Instance
    {
        get
        {
            if(instance==null)
            {
                //加锁   是为了控制某一个时间内只有一个线程访问
                lock(_lock)
                {
                    if(instance==null)
                    {
                        instance = new T();
                    }
                }
            }

            return instance;
        }
    
    }
}
