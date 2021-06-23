using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UICore;

public class InforUI :BaseUI
{
    public override void InitUIOnAwake()
    {
        base.InitUIOnAwake();


    }
    public override void InitUIData()
    {
        base.InitUIData();
        this.UiId = EUiId.InforUI;
        this.uiType.showMode = EShowUIMode.DoNothing;
        //保持在最前方。。。
        this.uiType.rootType = EUIRootType.KeepAbove;

    }

}
