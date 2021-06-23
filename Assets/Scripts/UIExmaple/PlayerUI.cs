using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UICore;

/// <summary>
/// 玩家面板
/// </summary>
public class PlayerUI:BaseUI
{
    //跳出按钮
    private UIButton btnBreak;

    /// <summary>
    /// 初始化面板元素（控件)
    /// </summary>
    public override void InitUIOnAwake()
    {
        base.InitUIOnAwake();

        btnBreak = GameTool.FindTheChild(this.gameObject, "btnBreak").gameObject.GetComponent<UIButton>();

        btnBreak.onClick.Add(new EventDelegate(() => {

            print("跳出窗体。。。");
            //淡出选择界面
            UIManager.GetInstance().ShowUI(EUiId.ChooseUI);



        }));
    }
    /// <summary>
    /// 初始化数据
    /// </summary>
    public override void InitUIData()
    {
        base.InitUIData();

        this.UiId = EUiId.PlayerUI;
        this.uiType.colliderType = EUIColliderType.Lusensy;

        this.uiType.showMode = EShowUIMode.NoReturn;
    }

}
