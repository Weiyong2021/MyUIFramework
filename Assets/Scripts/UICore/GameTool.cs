using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 游戏工具类
/// </summary>
public class GameTool :MonoBehaviour
{
    #region 测试

    //private void Start()
    //{
    //    GameObject obj = FindTheChild(this.gameObject, "name3").gameObject;
    //    print("---------------------" + obj.name);

    //}

    #endregion


    //判断游戏是不是第一次运行。。。
    public static bool isFirstLoad = true;



    //①获取指定范围内的随机数
    public static int GetRandomInt(int num1,int num2)
    {
        if(num1<num2)
        {
            return UnityEngine.Random.Range(num1, num2);

        }
        else
        {
            return UnityEngine.Random.Range(num2,num1);

        }
    }

    //②清理内存的方法    一般是在切换场景的时候调用
    public static void ClearMemory()
    {
        //在切换场景的时候清理一下内存。。。
        GC.Collect();
        //释放掉没有使用的资源
        Resources.UnloadUnusedAssets();
    }

    //③根据分号来分割字符串
    public  static string[] DivisionStr(string str)
    {
        return str.Split(':');

    }

    #region ④封装PlayerPrefs
    public static bool HasKey(string keyName)
    {
        return PlayerPrefs.HasKey(keyName);

    }

    public static int GetInt(string keyName)
    {
        return PlayerPrefs.GetInt(keyName);

    }

    public static void SetInt(string keyName,int valueInt)
    {
        PlayerPrefs.SetInt(keyName, valueInt);

    }


    public static float GetFloat(string keyName)
    {
        return PlayerPrefs.GetFloat(keyName);

    }

    public static void SetFloat(string keyName,float valueFloat)
    {
        PlayerPrefs.SetFloat(keyName, valueFloat);
    }

    public static string GetString(string keyName)
    {
        return PlayerPrefs.GetString(keyName);
    }

    public static void SetString(string keyName,string valueString)
    {
        PlayerPrefs.SetString(keyName, valueString);

    }


    public static void DeleteAllKey()
    {
        PlayerPrefs.DeleteAll();

    }

    public static void DeleteTheKey(string keyName)
    {
        PlayerPrefs.DeleteKey(keyName);

    }

    #endregion


    /// <summary>
    /// //⑤查找子物体的方法。。。采用了递归来实现的。。。
    /// </summary>
    /// <param name="objParent">父物体</param>
    /// <param name="childName">孩子的名字</param>
    /// <returns></returns>
    public static Transform FindTheChild(GameObject objParent,string childName)
    {
        //通过名字来查找子物体
        Transform searchTrans = objParent.transform.Find(childName);
        if(searchTrans==null)//如果没有找到，就一层层的找下去，通过递归来查找的
        {
            foreach (Transform trans in objParent.transform)
            {
                print(objParent.transform);
                searchTrans = FindTheChild(trans.gameObject, childName);
                if(searchTrans!=null)
                {
                    return searchTrans;
                }
            }
            
        }
        //如果不进入上面的if语句里面的内容则找到了   直接返回。。。
        return searchTrans;
       

    }



    //⑥获取子物体的脚本
    public static T GetTheChildComponent<T>(GameObject objParent,string childName) where T:Component
    {
        //先查找到子物体
        Transform searchTrans = FindTheChild(objParent, childName);

        //如果找到
        if(searchTrans!=null)
        {
            return searchTrans.gameObject.GetComponent<T>();
        }
        else
        {
            return null;
        }

    }



    //⑦给子物体添加脚本
    public static T AddTheChildCmoponent<T>(GameObject objParent,string childName) where T:Component
    {
        Transform searchTrans = FindTheChild(objParent, childName);

        if(searchTrans!=null)
        {
            //先把他之前身上的脚本移除
            T[] theComponentsArr = searchTrans.GetComponents<T>();

            for(int i=0;i<theComponentsArr.Length;++i)
            {
                Destroy(theComponentsArr[i]);
            }
            //移除完了之后再添加上去。。。。
            return searchTrans.gameObject.AddComponent<T>();
        }
        return null;
    }



    //⑧添加子物体
    public static void AddChildToParent(Transform transParent,Transform transChild)
    {
        //设置父物体
        transChild.SetParent(transParent,false);
        //设置位置
        transChild.localPosition = Vector3.zero;
        //设置角度
        transChild.localRotation = Quaternion.identity;
        //设置缩放大小
        transChild.localScale = Vector3.one;

        //设置层级
        SetLayer(transParent.gameObject.layer, transChild);


    }



    //⑨设置所有子物体的层级和父物体的层级一样。。。
    public static void SetLayer(int parentLayer,Transform transChild)
    {
        transChild.gameObject.layer = parentLayer;

        //再看看
        for(int i=0;i<transChild.childCount;++i)
        {
            Transform child = transChild.GetChild(i);
            child.gameObject.layer = parentLayer;

            //然后再看子物体下的子物体
            SetLayer(parentLayer, child);//一直递归下去

        }

    }



    //⑩给窗体添加碰撞体//isLusensy  是否是透明的
    public static void AddCollierBg(GameObject objParent,string collerBgName,bool isLusensy, NGUIAtlas atlas)
    {
        Transform bg = FindTheChild(objParent, "ColliderBg");
        if(bg==null)
        {
            UIWidget widget = null;
            if(isLusensy)//如果是透明的
            {
                widget = NGUITools.AddWidget<UIWidget>(objParent);
            }
            else
            {
                widget = NGUITools.AddSprite(objParent, atlas, collerBgName);
            }

            widget.name = "ColliderBg";
            bg = widget.transform;
            //使用这个来填充满我们的整个屏幕。。。
            UIStretch stretch= bg.gameObject.AddComponent<UIStretch>();
            stretch.style = UIStretch.Style.Both;

            //
            widget.depth = -1;
            widget.alpha = 0.5f;

            NGUITools.AddWidgetCollider(bg.gameObject);


        }

    }

    /// <summary>
    /// 十一   加载场景的开关
    /// </summary>
    public static void OpenLoadSceneHelper()
    {
        GameObject uiRoot = GameObject.Find("UI Root").gameObject;
        if(uiRoot!=null)
        {
            GameObject helpObj = FindTheChild(uiRoot, "LoadSceneHelper").gameObject;
            if(helpObj.activeInHierarchy==false)//如果不可见就设置可见
            {
                helpObj.SetActive(true);

            }
        }
    }

}
