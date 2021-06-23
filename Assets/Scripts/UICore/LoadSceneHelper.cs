using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneHelper : MonoBehaviour
{
    private static LoadSceneHelper instance;
    public  static LoadSceneHelper Instance => instance;

    //异步的对象
    AsyncOperation async;

    private UISlider iSlider;

    private int barProgress;

    private Action action2;



    private void Awake()
    {
        instance = this;
        this.gameObject.SetActive(false);
        DontDestroyOnLoad(this.gameObject);

    }

    /// <summary>
    /// 异步加载场景。。。
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(string sceneName,Action action)
    {
        if(iSlider==null)
        {
            iSlider = GameObject.Find("progressBar").GetComponent<UISlider>();

        }


        action2 = action;
        barProgress = 0;


        StartCoroutine(LoadSceneReally(sceneName));

    }

    IEnumerator LoadSceneReally(string sceneName)
    {
        async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;
        yield return async;
    }

    int theProgress = 0;
    void Update()
    {
        if (async == null)
            return;

        if(async.progress<0.9f)//到0.9的时候已经加载完成了
        {
            //说明还在加载中
            theProgress = (int)async.progress * 100;

        }
        else
        {
            theProgress = 100;

        }

        if(barProgress<theProgress)
        {
            barProgress++;

        }
        iSlider.value = barProgress / 100f;

        if(barProgress==100)
        {
            //显示场景。。。
            async.allowSceneActivation = true;
           
        }
        if(async.isDone)
        {
            if (action2 != null)
            {
                action2();

            }
            async = null;

            iSlider.value = 0;

            //清理内存
            GameTool.ClearMemory();
            this.gameObject.SetActive(false);
        }

       
    }
}
