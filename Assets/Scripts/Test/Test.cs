using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SystemDebuger;
using UICore;

public class Test : MonoBehaviour
{
    
    void Start()
    {
        //开启开关
        Debuger.debugerEnable = true;

        //Debuger.Log("测试1");
        //Debuger.LogError("测试2");
        //Debuger.LogWarning("测试三");

        EventCenter.Instance.AddListener(EEventType.BuyProp, () => {
            Debuger.Log("购买金币成功。。。。。。。。。。。。。");
        
        
        });
        

    }

    
    void Update()
    {
        
    }
}
