using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UICore;

public class LoadingUI :BaseUI
{
    public override void InitUIOnAwake()
    {
        base.InitUIOnAwake();
    }
    public override void InitUIData()
    {
        base.InitUIData();
        //给窗体的id初始化
        this.UiId = EUiId.LoadingUI;
        this.uiType.showMode = EShowUIMode.NoReturn;

        this.uiType.colliderType = EUIColliderType.Lusensy;

    }

}
