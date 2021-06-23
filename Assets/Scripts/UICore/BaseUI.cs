using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UICore
{
   

    public class UIType
    {

        //是否需要重新设置窗体反向切换的信息
        public bool isResetReturnUIInfor = false;

        //窗体显示模式。。。
        public EShowUIMode showMode = EShowUIMode.DoNothing;



        public EUIRootType rootType = EUIRootType.Normal;

        public EUIColliderType colliderType = EUIColliderType.NoCollier;//默认为不加碰撞器的


    }




    public class BaseUI : MonoBehaviour
    {
        //
        public UIType uiType=new UIType();


        private int currentDepth = 1;
        public int CurrentDepth { get => currentDepth; set => currentDepth = value; }






        protected Transform thisTrans;//这个也是多余的，可以不要

        ////是否需要碰撞器
        //private bool isNeedCollider = false;

        // public bool IsNeedCollider { get => isNeedCollider; set => isNeedCollider = value; }

        //窗体(面板)的id   
        protected EUiId UiId = EUiId.NullUI;


        //记录上一个窗体的id
        protected EUiId beforeUiId = EUiId.NullUI;

        public EUiId GetUiId
        {
            get
            {
                return UiId;
            }

        }

        public EUiId GetBeforeUiId
        {
            get
            {
                return beforeUiId;
            }
        }

       
        protected virtual void Awake()
        {
            thisTrans = this.transform;//这一行可以不要，多余的

            //初始化界面元素
            InitUIOnAwake();

            // 初始化数据
            InitUIData();


        }

        /// <summary>
        /// 是否需要添加到反向切换的信息里面   如果需要就更新 stackReturnInfor
        /// </summary>
        public bool IsNeedUpdateStack
        {

            get
            {
                //显示在最前面的窗体是不需要反向切换的
                if(this.uiType.rootType==EUIRootType.KeepAbove)
                {
                    return false;
                }
                if(this.uiType.showMode==EShowUIMode.NoReturn)
                {
                    return false;
                }

                return true;

            }
        
        
        }





        /// <summary>
        /// 初始化界面元素
        /// </summary>
        public virtual void InitUIOnAwake()
        {

        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        public virtual void InitUIData()
        {
            if(uiType==null)
            {
                uiType = new UIType();

            }
        }

        //重置
        public virtual void Reset()
        {

        }



        public virtual void ShowUI()
        {
            NGUITools.SetActive(this.gameObject, true);

        }

        public virtual void HideUI(DelAfterHideUI callBack)
        {
            //this.gameObject.SetActive(false);
            NGUITools.SetActive(this.gameObject, false);
            //隐藏面板起来之后可能要做很多的事情，在这里可以使用委托

            if (callBack != null)
            {
                // callBack?.Invoke();   可以这样写
                callBack();

            }

        }


        public virtual void DestroyUI()
        {
            BeforeDestory();
            Destroy(this.gameObject);

        }

        //在销毁之前可以做一些事情
        protected virtual void BeforeDestory()
        {

        }



        //当我们关闭面板或者打开面板的时候有可能 播放音效
        protected virtual void PlayAudio()
        {

        }

    }

}

