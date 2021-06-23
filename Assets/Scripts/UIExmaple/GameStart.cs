using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    private GameObject uiRoot;

    private void Awake()
    {

        if(GameTool.isFirstLoad)
        {
            uiRoot = Resources.Load<GameObject>("UI/UI Root");

            GameObject obj=GameObject.Instantiate<GameObject>(uiRoot);
            obj.name = "UI Root";
            uiRoot = null;

            GameTool.isFirstLoad = false;//第一次加载过了之后，就设置他是flase

            //数据预加载    把数据加载到内存中。。。。
            DataControl.Instance.LoadAllCfg();



        }

    }


}
