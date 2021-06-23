using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UICore;

public class LevelEnty : MonoBehaviour
{
    private int levelId = 0;

    public int LevelId { get => levelId; set => levelId = value; }

    void Start()
    {
        //监听自己身上的按钮
        this.GetComponent<UIButton>().onClick.Add(new EventDelegate(()=>{


            #region 测试的
            //DataControl.Instance.LoadAllCfg();
            //string str = DataControl.Instance.ReadData("SpriteName", 2, DataControl.Instance.dicPack);
            //Debug.Log("id 为 2 的 图片名称是：" + str);


            #endregion


            string levelName = DataControl.Instance.ReadData("LevelSceneName", LevelId, DataControl.Instance.diclevel);

            //被点击的关卡的id为
            Debug.Log("被点击的关卡的id为" + levelId+"该关卡的名字为："+levelName);

            //显示加载面板
            UIManager.GetInstance().ShowUI(EUiId.LoadingUI);

            //加载场景。。。
            //先打开加载场景的开关。。。
            GameTool.OpenLoadSceneHelper();
            //然后加载场景。。。
            LoadSceneHelper.Instance.LoadScene(levelName, () => {
                UIManager.GetInstance().HideUI(EUiId.LoadingUI, null);
                //显示
                UIManager.GetInstance().ShowUI(EUiId.PlayerUI);

            });
        }));
    }

}
