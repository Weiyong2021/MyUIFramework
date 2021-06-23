using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UICore;
/// <summary>
/// 玩家面板
/// </summary>
public class PackUI:BaseUI
{
    //返回按钮
    private UIButton btnReturn;


    private UIScrollView sv;

    private GameObject itemPrefab;



    /// <summary>
    /// 初始化元素界面(控件)
    /// </summary>
    public override void InitUIOnAwake()
    {
        base.InitUIOnAwake();

        btnReturn = GameTool.FindTheChild(this.gameObject, "btnReturn").gameObject.GetComponent<UIButton>();

        btnReturn.onClick.Add(new EventDelegate(() => {
            //显示主面板。。。
            UIManager.GetInstance().ShowUI(EUiId.MainUI);
           
        }));

        //
        sv = GameTool.FindTheChild(this.gameObject, "sv").gameObject.GetComponent<UIScrollView>();

        itemPrefab = Resources.Load<GameObject>("UI/Item");

        //展示物品的函数
        this.ShowItem();

    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    public override void InitUIData()
    {
        base.InitUIData();
        //初始化id
        this.UiId = EUiId.PackUI;
        //需要透明的碰撞器
        this.uiType.colliderType = EUIColliderType.Lusensy;
        //显示模式
        this.uiType.showMode = EShowUIMode.HideOther;
        //上一个面板的id是主面板的id
        this.beforeUiId = EUiId.MainUI;

    }

    //2021-5-19  0:07写到了这里，明天再看看。。。。。。。。。。。。。。。。。。。。。。
    /// <summary>
    /// 展示物品函数
    /// </summary>
    public void ShowItem()
    {
        //得到物品表里面有多少个物品
        int count = DataControl.Instance.GetInforCount(DataControl.Instance.dicItem);


        for(int i=0;i<count;++i)
        {
            //读取物品图片名字
            string spriteName = DataControl.Instance.ReadData("IconName",i+1, DataControl.Instance.dicItem);
            //生成物品
            GameObject itemObj = Instantiate<GameObject>(itemPrefab);
            //加载图集
            NGUIAtlas nGUIAtlas = Resources.Load<NGUIAtlas>("Atlas/ItemAtlas");
            //得到物品的精灵组件
            UISprite sprite= GameTool.FindTheChild(itemObj, "item").transform.GetComponent<UISprite>();
            //给图片换图集
            sprite.atlas = nGUIAtlas;
            //根据名字设置图片的显示
            sprite.spriteName = spriteName;
            //设置父物体
            itemObj.transform.SetParent(sv.transform, false);
            //设置位置
            itemObj.transform.localPosition = new Vector3(-309f+160f*(i%4), 270f-155f*(i/4), 0);
            //设置缩放大小
            itemObj.transform.localScale = Vector3.one;
            //设置旋转
            itemObj.transform.localRotation = Quaternion.identity;

        }

        //更新滑动条
        sv.transform.gameObject.GetComponent<UIScrollView>().UpdateScrollbars();


    }
}
