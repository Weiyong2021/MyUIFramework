using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UICore;

public class MainUI :BaseUI
{
    private UIButton btnPack;

    private UIButton btnLevel;

    public override void InitUIOnAwake()
    {
        base.InitUIOnAwake();
        //
        btnPack = GameTool.FindTheChild(this.gameObject, "btnPack").gameObject.GetComponent<UIButton>();

        btnLevel= GameTool.FindTheChild(this.gameObject, "btnLevel").gameObject.GetComponent<UIButton>();

        btnPack.onClick.Add(new EventDelegate(() => {
            //显示背包面板。。。
            print("显示背包面板。。。。。。。。。。。。。。。。");
            UIManager.GetInstance().ShowUI(EUiId.PackUI);

        
        }));

        btnLevel.onClick.Add(new EventDelegate(() => {
            //显示关卡面板。。。
            print("显示关卡面板。。。。。。。。。。。。。。。。");

            UIManager.GetInstance().ShowUI(EUiId.LevelUI);



        }));


    }

    //初始化数据
    public override void InitUIData()
    {
        base.InitUIData();
        //初始化自己的id
        this.UiId = EUiId.MainUI;
        //
        this.uiType.isResetReturnUIInfor = true;


    }
}
