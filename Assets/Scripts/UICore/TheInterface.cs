using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UICore
{
    interface IUIAnimation
    {
        //进入动画
        void EnterAnimation(EventDelegate.Callback callback);

        //退出动画
        void QuitAnimation(EventDelegate.Callback callback);

        //重新设置动画
        void ResetAnimation();


    }

    //事件监听接口
    interface IEventListener
    {

        //添加监听
        void RegisterEvent();

        //取消监听
        void UnRegisterEvent();

    }

}


