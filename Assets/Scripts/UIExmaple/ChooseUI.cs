using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UICore;

public class ChooseUI:BaseUI
{
    private UIButton btnLevel;

    private UIButton btnPack;

    private UIButton btnQuit;

    //初始化界面元素
    public override void InitUIOnAwake()
    {
        base.InitUIOnAwake();

        btnLevel = GameTool.FindTheChild(this.gameObject, "btnLevel").gameObject.GetComponent<UIButton>();
        btnPack = GameTool.FindTheChild(this.gameObject, "btnPack").gameObject.GetComponent<UIButton>();
        btnQuit = GameTool.FindTheChild(this.gameObject, "btnQuit").gameObject.GetComponent<UIButton>();

        btnQuit.onClick.Add(new EventDelegate(() => {
            print("有没有进来。。。。。。。。。。。。。");
            //隐藏选择面板。。。
            UIManager.GetInstance().HideUI(EUiId.ChooseUI, null);

            //显示玩家面板
            UIManager.GetInstance().ShowUI(EUiId.PlayerUI);



        }));

        //添加事件监听。。。
        UIEventListener.Get(btnLevel.gameObject).onClick = ClickToLevel;
        //添加事件监听。。。
        UIEventListener.Get(btnPack.gameObject).onClick = ClickToPack;

    }

    //初始化数据
    public override void InitUIData()
    {
        base.InitUIData();
        this.UiId = EUiId.ChooseUI;

        this.uiType.showMode = EShowUIMode.NoReturn;//不需要反向切换

        this.uiType.colliderType = EUIColliderType.NoLusensy;


    }



    public void ClickToLevel(GameObject obj)
    {
        print("点击了关卡界面的按钮。。。");
        //显示加载界面
        UIManager.GetInstance().ShowUI(EUiId.LoadingUI);
        //---------------------加载场景---------------------------
        //打开加载场景的开关
        GameTool.OpenLoadSceneHelper();
        //加载MainScene场景
        LoadSceneHelper.Instance.LoadScene("MainScene",()=> {

            UIManager.GetInstance().HideUI(EUiId.LoadingUI, null);

            UIManager.GetInstance().ShowUI(EUiId.LevelUI);

            UIManager.GetInstance().ShowUI(EUiId.InforUI);


        });


    }


    public void ClickToPack(GameObject obj)
    {
        print("点击了背包界面的按钮。。。");

        //显示加载界面
        UIManager.GetInstance().ShowUI(EUiId.LoadingUI);
        //---------------------加载场景---------------------------
        //打开加载场景的开关
        GameTool.OpenLoadSceneHelper();
        //加载MainScene场景
        LoadSceneHelper.Instance.LoadScene("MainScene", () => {

            UIManager.GetInstance().HideUI(EUiId.LoadingUI, null);

            UIManager.GetInstance().ShowUI(EUiId.PackUI);

            UIManager.GetInstance().ShowUI(EUiId.InforUI);


        });

    }

}
