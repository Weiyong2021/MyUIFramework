using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SystemDebuger;

namespace UICore
{
    /// <summary>
    /// 反向切换的信息数据
    /// </summary>
    public class UIReturnInfor
    {
        //将要被显示出来的窗体。。。
        public BaseUI willShowUI;
        //存放反向切换窗体ID的列表。。。
        public List<EUiId> listReturn;
    }

    
    /// <summary>
    /// UI管理器
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        //存储所有的窗体
        private Dictionary<EUiId, BaseUI> dicAllUI;

        //存储已经打开过的窗体(正在显示的窗体）
        private Dictionary<EUiId, BaseUI> dicShowUI;

        //存储当前窗体
        private BaseUI currentUI = null;
        //存储跳转到该当前窗体的上一个窗体
        private BaseUI beforeUI = null;

        //存储NGUI的UIRoot
        public  Transform uiRootTrans;

        //保持在最前方的窗体的父物体
        private Transform keepAboveUIRoot;

        //普通的弹出窗口的父物体
        private Transform normalUIRoot;


        //NGUI的图集    注意在这里翻车了一个小时。。。
        // public UIAtlas theAtlas;//不是这个。。。
        public NGUIAtlas theAtlas;


        //窗体隐藏之前默认执行委托。。。
       // public bool isBeforeHideDoCallBack = true;


        //设置显示在最前面的UI的父物体的深度
        private int keepAboveUIRooDepth = 300;
        //设置普通父物体的根节点的深度
        private int normalUIRootDepth = 100;

        //静态私有成员变量
        private static UIManager instance;

        //栈
        private Stack<UIReturnInfor> stackReturnInfor;

        
        public static UIManager GetInstance()
        { 
            return instance;

        }

        private void Awake()
        {
            ////如果根物体为空，则查找
            //if(uiRootTrans==null)
            //{
            //    uiRootTrans = GameObject.Find("UI Root").transform;

            //}

            if(dicAllUI==null)
            {
                dicAllUI = new Dictionary<EUiId, BaseUI>();

            }

            if(dicShowUI==null)
            {
                dicShowUI = new Dictionary<EUiId, BaseUI>();
            }

            if(stackReturnInfor==null)
            {
                stackReturnInfor = new Stack<UIReturnInfor>();

            }

            instance = this;

            //初始化数据
            InitUIManager();

            //可以在外面拖，不用在这里加载。。。
            ////加载图集。。。
            //theAtlas = Resources.Load<NGUIAtlas>("Atlas/MyAtlas");
            //if (theAtlas != null)
            //    print("theAtlas is not null");

        }


        /// <summary>
        /// 初始化数据
        /// </summary>
        public void InitUIManager()
        {
            if(dicAllUI.Count>0)
            {
                dicAllUI.Clear();
            }
            if(dicShowUI.Count>0)
            {
                dicShowUI.Clear();
            }

            //过场景不移除
            DontDestroyOnLoad(uiRootTrans);

            //如果父物体为空的话
            if(keepAboveUIRoot==null)
            {
                keepAboveUIRoot = new GameObject("keepAboveUIRoot").transform;
                //然后把他放到UIRoot下面去
                GameTool.AddChildToParent(uiRootTrans, keepAboveUIRoot);
                //然后设置层级
                GameTool.SetLayer(uiRootTrans.gameObject.layer, keepAboveUIRoot);

            }

            //如果父物体为空的话
            if (normalUIRoot==null)
            {
                normalUIRoot = new GameObject("normalUIRoot").transform;
                //然后把他放到UIRoot下面去
                GameTool.AddChildToParent(uiRootTrans, normalUIRoot);
                //然后设置层级
                GameTool.SetLayer(uiRootTrans.gameObject.layer, normalUIRoot);

            }

            //显示我们的UI   一开始显示我们的主面板 
            ShowUI(EUiId.MainUI);

            //
            ShowUI(EUiId.InforUI);

            
            // GameTool.isFirstLoad = false;//第一次加载过了之后，就设置他是flase



        }


        /// <summary>
        /// 根据窗体的id来显示窗体
        /// </summary>
        /// <param name="uiId">窗体的id</param>
        public void ShowUI(EUiId uiId)
        {
            BaseUI baseUI = JudgeShowUI(uiId);
            if(baseUI==null)
            {
                print("baseUI is null");
            }

            if(baseUI!=null)
            {
                baseUI.ShowUI();
                //记录下已经显示的窗体。。。
                dicShowUI[uiId] = baseUI;
                //在窗体显示的时候判断是否需要反向切换。。。
                if(baseUI.uiType.isResetReturnUIInfor)
                {
                    //重新设置反向切换的信息（当现实的是MainUI的时候，就需要重新设置)
                    this.ClearStackReturnInfor();

                }



                if (baseUI.uiType.rootType == EUIRootType.Normal)
                {
                    //注意这两个顺序不能乱。。。
                    beforeUI = currentUI;

                    currentUI = baseUI;

                }
            }

        }


        //清空栈的信息。。。
        public  void ClearStackReturnInfor()
        {
            if(stackReturnInfor!=null)
            {
                stackReturnInfor.Clear();

            }

        }

        /// <summary>
        /// 把窗体添加到stackReturnInfor(更新栈)
        /// </summary>
        public void UpdateStack(BaseUI baseUI)
        {
            if(baseUI.IsNeedUpdateStack)
            {
                //将要移除的窗体的id列表
                List<EUiId> removeKey = null;

                //存放需要添加进栈的窗体的列表。。。
                List<BaseUI> maxToMinUiId = new List<BaseUI>();

                //得到排序好的窗体的id
                List<EUiId> newList = new List<EUiId>();

                UIReturnInfor uIReturnInfor = new UIReturnInfor();


                if(dicShowUI.Count>0)
                {
                    foreach(KeyValuePair<EUiId,BaseUI> itemUI in dicShowUI)
                    {
                        if(itemUI.Value.uiType.rootType!=EUIRootType.KeepAbove)
                        {
                            itemUI.Value.HideUI(null);
                            if(removeKey==null)
                            {
                                removeKey = new List<EUiId>();

                            }

                            removeKey.Add(itemUI.Key);

                        }

                        if(itemUI.Value.IsNeedUpdateStack)
                        {
                            maxToMinUiId.Add(itemUI.Value);

                        }


                    }


                    if(removeKey!=null)
                    {
                        for(int i=0;i<removeKey.Count;++i)
                        {
                            dicShowUI.Remove(removeKey[i]);
                        }
                    }
                    //按照窗体的depth值从大到小排序

                    if(maxToMinUiId.Count>0)
                    {
                        maxToMinUiId.Sort((a,b)=> {
                            return a.CurrentDepth.CompareTo(b.CurrentDepth);
                        });

                        for(int i=0;i<maxToMinUiId.Count;++i)
                        {
                            newList.Add(maxToMinUiId[i].GetUiId);

                        }

                        //
                        uIReturnInfor.willShowUI = baseUI;

                        uIReturnInfor.listReturn = newList;

                        stackReturnInfor.Push(uIReturnInfor);


                    }


                }

            }
            else
            {
                if(baseUI.uiType.showMode==EShowUIMode.NoReturn)
                {
                    //隐藏所有的窗体
                    HideAllUI(true);

                }
            }

            this.CheckStack(baseUI);

        }


        //检测栈的顺序是否被打乱。。。如果打乱了就清空栈
        public void CheckStack(BaseUI baseUI)
        {
            if(baseUI.IsNeedUpdateStack)
            {
                if(stackReturnInfor.Count>0)
                {
                    //取出栈顶元素
                    UIReturnInfor uIReturnInfor = stackReturnInfor.Peek();
                    if (uIReturnInfor.willShowUI != baseUI)
                    {
                        //需要清空
                        stackReturnInfor.Clear();

                    }
                }
               
            }

        }


        //当点击返回按钮返回的时候，调用一下这个函数
        public void ClickReturn()
        {
            //说明没有反向切换的信息。。。
            if(stackReturnInfor.Count==0)
            {
                EUiId beforeUiId = currentUI.GetBeforeUiId;
              
                if (currentUI == null)
                    return;
                if(beforeUiId != EUiId.NullUI)
                {
                    HideUI(currentUI.GetUiId, () => {
                        this.ShowUI(beforeUiId);
                    });

                }

            }
            else//说明有反向切换信息。。。
            {
                //1.先隐藏当前窗体
                UIReturnInfor uIReturnInfor = stackReturnInfor.Peek();
               
                if(uIReturnInfor!=null)
                {
                    //或得当前窗体的id
                    EUiId theId=uIReturnInfor.willShowUI.GetUiId;
                    if(uIReturnInfor.willShowUI != null&&dicShowUI.ContainsKey(theId))
                    {
                        this.HideUI(theId, () => { 
                            //
                            if(!dicShowUI.ContainsKey(uIReturnInfor.listReturn[0]))
                            {
                                BaseUI baseUI = GetBaseUI(uIReturnInfor.listReturn[0]);
                               
                                baseUI.ShowUI();
                                dicShowUI.Add(uIReturnInfor.listReturn[0], baseUI);

                                this.beforeUI = currentUI;
                                currentUI = baseUI;

                                //把栈顶元素删除掉
                                stackReturnInfor.Pop();

                            }
                        
                        });


                    }
                }

            }

        }


        //判断该窗体是不是正在显示，或者说已经显示出来了
        public BaseUI JudgeShowUI(EUiId uiId)
        {
            //根据窗体id得到窗体
            BaseUI baseUI = GetBaseUI(uiId);
            //先判断该窗体是不是正在显示，或者说已经显示出来了
            if (dicShowUI.ContainsKey(uiId))//已经显示出来了
            {
                //表示已经在属性面板中，但是被隐藏了，失活了
                if (baseUI.gameObject.activeInHierarchy == false)
                {
                    return baseUI;//返回让他第二次启用
                }
                else//表示已经在场景中了，并且是正在使用的状态。。。
                {
                    return null;//已经显示了，我们就不需要显示了

                }

            }

            if(baseUI==null)//如果在dicAllUI字典里面没有找到
            {
                //说明还没有加载过，所以我们需要加载。。。
                //开始加载
                if(GameDefine.dicPath.ContainsKey(uiId))
                {
                    string path = GameDefine.dicPath[uiId];
                    GameObject theUI = Resources.Load<GameObject>(path);
                    if(theUI!=null)
                    {
                        //实例化出来
                        GameObject willShowUI = Instantiate<GameObject>(theUI);
                        //显示出来
                        NGUITools.SetActive(willShowUI, true);
                        //把他放到对应的父物体下面去
                        baseUI = willShowUI.GetComponent<BaseUI>();
                        Transform theRoot=GetTheUIRoot(baseUI);
                        GameTool.AddChildToParent(theRoot, willShowUI.transform);
                        theUI = null;
                        //放到字典中
                        dicAllUI.Add(uiId, baseUI);
                        

                    }
                    else//如果没有加载资源出来，就报错。。。
                    {
                        
                        Debuger.LogError("Load UI is Null");
                    }
                }

            }
            //显示出来之后，还要给他加一个碰撞器
            //添加背景碰撞器
            JudgeCollider(baseUI);

            //设置窗体的层级。。。。。。。。（2021-5-13 晚上1:08  明天再看。。。）
            //2021-5-13 下午七点
            SetUIDepth(baseUI);

            //在这里调用一下。。。
            this.UpdateStack(baseUI);

            return baseUI;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseUI"></param>
        public void SetUIDepth(BaseUI baseUI)
        {

            int resultDepth = 0;

            //
            EUIRootType rootType = baseUI.uiType.rootType;
            if(rootType==EUIRootType.KeepAbove)
            {
                //1.先获取这个UIRoot下面的depth值最大
                int maxDepth = GetMaxDepth(keepAboveUIRoot.gameObject, false);


                //2.设置我们当前要显示的窗体的depth值等于我们或得的最大的depth值+1
                resultDepth = Mathf.Clamp(maxDepth + 1, keepAboveUIRooDepth, int.MaxValue);

            }
            if(rootType==EUIRootType.Normal)
            {
                //1.先获取这个UIRoot下面的depth值最大
                int maxDepth = GetMaxDepth(normalUIRoot.gameObject, false);


                //2.设置我们当前要显示的窗体的depth值等于我们或得的最大的depth值+1
                resultDepth = Mathf.Clamp(maxDepth + 1, normalUIRootDepth, int.MaxValue);

                
            }

            if(baseUI.CurrentDepth!=resultDepth)
            {
                SetDepth(baseUI.gameObject, resultDepth);

            }

            baseUI.CurrentDepth = resultDepth;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uiObj"></param>
        /// <param name="depth"></param>
        public void SetDepth(GameObject uiObj,int depth)
        {
            List<UIPanel> listPanels = GetPanelMinToMax(uiObj, true);
            if(listPanels!=null)
            {
                for(int i=0;i<listPanels.Count;++i)
                {
                    listPanels[i].depth = depth + i;//一层一层的递增
                }
            }

        }

        //或得最大的depth值
        public int GetMaxDepth(GameObject uiRootObj,bool includeHide=false)
        {
            int theDepth = 0;
            List<UIPanel> listPanel = GetPanelMinToMax(uiRootObj, includeHide);
            if(listPanel!=null)
            {
                return listPanel[listPanel.Count - 1].depth;

            }
            return theDepth;


        }

        //listPanel里面的窗体根据Depth从小到大排序

        public List<UIPanel> GetPanelMinToMax(GameObject parentObj, bool includeHide = false)
        {
            UIPanel[] panels = parentObj.transform.GetComponentsInChildren<UIPanel>();
            if(panels.Length>0)
            {
                List<UIPanel> listPanel = new List<UIPanel>(panels);
                //排序
                listPanel.Sort((a,b)=> { 
                    return a.depth.CompareTo(b.depth); 
                });

                return listPanel;
            }
            return null;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uiId"></param>
        /// <returns></returns>
        public BaseUI GetBaseUI(EUiId uiId)
        {
            if(dicAllUI.ContainsKey(uiId))
            {
                return dicAllUI[uiId];
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseUI"></param>
        /// <returns></returns>
        public Transform GetTheUIRoot(BaseUI baseUI)
        {
            if(baseUI.uiType.rootType==EUIRootType.Normal)
            {
                return normalUIRoot;
            }
            else if(baseUI.uiType.rootType == EUIRootType.KeepAbove)
            {
                return keepAboveUIRoot;
            }
            else
            {
                return uiRootTrans;//返回的是NGUI的Root
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseUI"></param>
        public void JudgeCollider(BaseUI baseUI)
        {
            EUIColliderType colliderType = baseUI.uiType.colliderType;
            if(colliderType==EUIColliderType.NoCollier)
            {
                //不需要碰撞器
                return;
            }
            else if(colliderType == EUIColliderType.NoLusensy)
            {
                //需要一个不透明的碰撞器                  //在这里NoLusencyBg表示不透明的背景
                GameTool.AddCollierBg(baseUI.gameObject, "NoLusencyBg", false, theAtlas);

            }
            else if(colliderType == EUIColliderType.Lusensy)
            {
                //需要一个透明的碰撞器
                GameTool.AddCollierBg(baseUI.gameObject, "NoLusencyBg", true, theAtlas);


            }

        }


        #region 隐藏窗体

        //隐藏窗体
        public void HideUI(EUiId uiId, DelAfterHideUI callBack)
        {
            this.HideTheUI(uiId, callBack);

        }

        //直接隐藏该窗体
        public void HideTheUI(EUiId uiId, DelAfterHideUI callBack)
        {
            //如果该窗体没有显示出来。。。则直接返回，不用隐藏了
            if (!dicShowUI.ContainsKey(uiId))
            {
                return;
            }
            ////窗体隐藏之前执行的委托
            //if (isBeforeHideDoCallBack)
            //{
            //    if (callBack != null)
            //    {
            //        callBack();
            //    }
            //    dicShowUI[uiId].HideUI(null);//隐藏窗体

            //    dicShowUI.Remove(uiId);
            //    return;

            //}

            //窗体隐藏之后执行的委托。。。
            if (callBack != null)
            {
                callBack += () => { dicShowUI.Remove(uiId); };
                //callBack += delegate { dicShowUI.Remove(uiId); };
                dicShowUI[uiId].HideUI(callBack);

            }
            else
            {
                dicShowUI[uiId].HideUI(null);
                dicShowUI.Remove(uiId);

            }

        }


        //隐藏所有的窗体
        public void HideAllUI(bool isHideAboveUI)
        {
            List<EUiId> listRemove = null;
            if (isHideAboveUI)//是否要隐藏最前端的UI
            {
                foreach (KeyValuePair<EUiId, BaseUI> uiItem in dicShowUI)
                {
                    uiItem.Value.HideUI(null);

                }
            }
            else
            {
                foreach (KeyValuePair<EUiId, BaseUI> uiItem in dicShowUI)
                {
                    if (uiItem.Value.uiType.rootType == EUIRootType.KeepAbove)
                    {
                        continue;
                    }
                    else
                    {
                        if (listRemove == null)
                        {
                            listRemove = new List<EUiId>();
                            listRemove.Add(uiItem.Key);
                            uiItem.Value.HideUI(null);
                        }
                    }

                }

            }
            if (listRemove != null)
            {
                for (int i = 0; i < listRemove.Count; ++i)
                {
                    listRemove.Remove(listRemove[i]);

                }
            }


        }

        #endregion

    }

}


