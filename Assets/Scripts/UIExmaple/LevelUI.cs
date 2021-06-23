using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UICore;

public class LevelUI : BaseUI
{
    private UIButton btnReturn;

    private GameObject levelAll;


    public override void InitUIOnAwake()
    {
        base.InitUIOnAwake();

        btnReturn = GameTool.FindTheChild(this.gameObject, "btnReturn").gameObject.GetComponent<UIButton>();
        btnReturn.onClick.Add(new EventDelegate(()=> {
            UIManager.GetInstance().ClickReturn();
        
        }));

        levelAll = GameTool.FindTheChild(this.gameObject, "levelAll").gameObject;

        for(int i=0;i<levelAll.transform.childCount;++i)
        {
            levelAll.transform.GetChild(i).gameObject.AddComponent<LevelEnty>();
            levelAll.transform.GetChild(i).gameObject.GetComponent<LevelEnty>().LevelId = i + 1;

        }
    }


    public override void InitUIData()
    {
        base.InitUIData();

        this.UiId = EUiId.LevelUI;

        this.uiType.colliderType = EUIColliderType.Lusensy;

        this.uiType.showMode = EShowUIMode.HideOther;

        this.beforeUiId = EUiId.MainUI;

    }
}
