using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UICore
{
    //委托
    public delegate void DelAfterHideUI();

   

    public enum EUIRootType
    {
        KeepAbove,
        Normal

    }

    //
    public enum EUIColliderType
    {
        NoCollier,
        Lusensy,//透明的
        NoLusensy//不透明的碰撞器
    }


    public enum EEventType
    {
        BuyProp,
        SellProp

    }


    /// <summary>
    /// 窗体的id的枚举
    /// </summary>
    public enum EUiId
    {
        NullUI = 0,//默认的面板
        PackUI = 1,//背包窗体 (面板)
        LevelUI = 2,//关卡窗体
        MainUI=3,//主面板
        InforUI,
        LoadingUI,
        PlayerUI,
        ChooseUI

    }

    //窗体的打开模式。。。
    public enum EShowUIMode
    {
        DoNothing,//什么都不做  一般用于主窗体和保持在最前方的菜单栏的窗体还有淡出来的小窗口。。。
        HideOther,//显示的时候隐藏其他（支持反向切换，隐藏所有的窗体，不包括保持在最前方的窗体)
        NoReturn//不需要反向切换(隐藏所有的窗体，包括保持在最前方的窗体)


    }

    public class GameDefine
    {
       



        // 窗体预设的路径
        public static Dictionary<EUiId, string> dicPath = new Dictionary<EUiId, string>()
        {
            {EUiId.MainUI,"UI/MainUI" },
            {EUiId.PackUI,"UI/PackUI" },
            {EUiId.LevelUI,"UI/LevelUI" },
            {EUiId.InforUI,"UI/InforUI" },
            {EUiId.LoadingUI,"UI/LoadingUI"},
            {EUiId.PlayerUI,"UI/PlayerUI"},
            {EUiId.ChooseUI,"UI/ChooseUI"}

        };

    }

}



