using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public enum UIState
{
    FowardShow,
    BackShow,
    FowardHide,
    BackHide,
    None,
}

public interface IUIState
{
    void FowardShowUI();
    void BackShowUI();
    void FowardHideUI();
    void BackHideUI();
}

namespace YProjectBase
{

    /// <summary>
    /// 面板基类
    /// 找到所有自己的控件对象
    /// 显示或隐藏
    /// </summary>
    public abstract class BasePanel : MonoBehaviour, IUIState,IInit
    {
        private Dictionary<string, List<UIBehaviour>> controlDic = new Dictionary<string, List<UIBehaviour>>();

        protected UIState baseCurrentUiState = UIState.None;

        public UIState GetCurrentUiState { get { return baseCurrentUiState; } }

        protected virtual void Awake()
        {
            Init();
        }


        /// <summary>
        /// 初始化
        /// </summary>
        public virtual void Init()
        {
            FindChildrenControl<Button>();
            FindChildrenControl<Image>();
            FindChildrenControl<Text>();
            FindChildrenControl<Toggle>();
            FindChildrenControl<Slider>();
            FindChildrenControl<ScrollRect>();
            FindChildrenControl<InputField>();
        }


        /// <summary>
        /// 显示UI
        /// </summary>
        public virtual void ShowUI()
        {

        }

        /// <summary>
        /// 隐藏UI
        /// </summary>
        public virtual void HideUI()
        {

        }



        /// <summary>
        /// 按钮事件
        /// </summary>
        /// <param name="buttonName"></param>
        protected virtual void OnClick(string buttonName)
        {

        }

        /// <summary>
        /// 单多框事件
        /// </summary>
        /// <param name="toggleName"></param>
        /// <param name="value"></param>
        protected virtual void OnValueChanged(string toggleName, bool value = false)
        {

        }




        /// <summary>
        /// 获得对应名字的对应控件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="controlName"></param>
        /// <returns></returns>
        protected T GetControl<T>(string controlName) where T : UIBehaviour
        {
            if (controlDic.ContainsKey(controlName))
            {
                for (int i = 0; i < controlDic[controlName].Count; i++)
                {
                    if (controlDic[controlName][i] is T)
                        return controlDic[controlName][i] as T;
                }
            }

            return null;
        }

        /// <summary>
        /// 找到子对象的对应控件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private void FindChildrenControl<T>() where T : UIBehaviour
        {

            T[] controls = this.GetComponentsInChildren<T>();

            for (int i = 0; i < controls.Length; i++)
            {
                string objName = controls[i].gameObject.name;
                if (controlDic.ContainsKey(objName))
                    controlDic[objName].Add(controls[i]);
                else
                    controlDic.Add(objName, new List<UIBehaviour>() { controls[i] });

                //如果是按钮
                if (controls[i] is Button)
                {
                    (controls[i] as Button).onClick.AddListener(() =>
                    {
                        OnClick(objName);

                    });
                }
                //如果是单选框还是多选框
                else if (controls[i] is Toggle)
                {
                    (controls[i] as Toggle).onValueChanged.AddListener((value) =>
                    {
                        OnValueChanged(objName, value);

                    });
                }

            }

        }

        /// <summary>
        /// 更新显示状态
        /// </summary>
        /// <param name="_uiState"></param>
        public void ToUpdatedUIState(UIState _uiState)
        {
            switch (_uiState)
            {
                case UIState.BackHide:
                    BackHideUI();
                    break;
                case UIState.BackShow:
                    BackShowUI();
                    break;
                case UIState.FowardHide:
                    FowardHideUI();
                    break;
                case UIState.FowardShow:
                    FowardShowUI();
                    break;
            }
        }


        /// <summary>
        /// 前进显示 UI 事件
        /// </summary>
        public virtual void FowardShowUI(){ ShowUI(); baseCurrentUiState = UIState.FowardShow; }
        /// <summary>
        /// 后退显示 UI 事件
        /// </summary>
        public virtual void BackShowUI(){ ShowUI(); baseCurrentUiState = UIState.BackShow; }
        /// <summary>
        /// 前进隐藏 UI 事件
        /// </summary>
        public virtual void FowardHideUI() { HideUI(); baseCurrentUiState = UIState.FowardHide; }
        /// <summary>
        /// 后退隐藏 UI 事件
        /// </summary>
        public virtual void BackHideUI() { HideUI(); baseCurrentUiState = UIState.BackHide; }

 
    }

}

/*调用方法
/* protected override void Awake()
{
  //不能少，执行父类事件
  base.Awake();
    //在后面添加自己的
}*/
