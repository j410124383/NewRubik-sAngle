using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YProjectBase
{

    /// <summary>
    /// Input输入基类
    /// </summary>
    public class InputMgr : BaseManager<InputMgr>
    {
        private bool isStartInput = false;

        public InputMgr()
        {
            MonoMgr.GetInstance().AddUpdateListener(InputUpdate);
            //MonoMgr.GetInstance().AddFixedUpdateListener(InputUpdate);
        }

        public void StartOrEndCheck(bool isInput)
        {
            isStartInput = isInput;
        }

        /// <summary>
        /// 输入帧刷新
        /// </summary>
        private void InputUpdate()
        {
            if (!isStartInput)
                return;

            CheckKeyCode(KeyCode.Q);
            CheckKeyCode(KeyCode.E);
            CheckKeyCode(KeyCode.W);
            CheckKeyCode(KeyCode.R);
            CheckKeyCode(KeyCode.A);
            CheckKeyCode(KeyCode.S);
            CheckKeyCode(KeyCode.D);
            CheckKeyCode(KeyCode.F);
            CheckKeyCode(KeyCode.UpArrow);
            CheckKeyCode(KeyCode.LeftArrow);
            CheckKeyCode(KeyCode.RightArrow);
            CheckKeyCode(KeyCode.DownArrow);
            CheckKeyCode(KeyCode.Space);
            CheckKeyCode(KeyCode.Mouse0);
            CheckKeyCode(KeyCode.Mouse1);
            CheckKeyCode(KeyCode.Escape);
            CheckKeyCode(KeyCode.JoystickButton0);
        }

        /// <summary>
        /// 按键检测事件
        /// </summary>
        /// <param name="key">检测的按键</param>
        private void CheckKeyCode(KeyCode key)
        {
            //按下
            if (Input.GetKeyDown(key))
                EventCenter.GetInstance().EventTrigger("KeyDown", key);

            //抬起
            if (Input.GetKeyUp(key))
                EventCenter.GetInstance().EventTrigger("KeyUp", key);

            //按住
            if (Input.GetKey(key))
                EventCenter.GetInstance().EventTrigger("Key", key);
        }

    }

}

/* void Start()
    {
        //设置可以监听按键
        InputMgr.GetInstance().StartOrEndCheck(true);

        //设置可以监听按键事件
        EventCenter.GetInstance().AddEventListener<KeyCode>("按键类型", 按键调用的方法);
        EventCenter.GetInstance().AddEventListener<KeyCode>("KeyDown", CheckDown);
        EventCenter.GetInstance().AddEventListener<KeyCode>("KeyUp", CheckUp);
        EventCenter.GetInstance().AddEventListener<KeyCode>("Key", Check);
    }

    private void CheckDown(KeyCode key)
    {

        switch (key)
        {
            case KeyCode.A:
                Debug.Log("KeyDown-----A");
                break;
            case KeyCode.Space:
                Debug.Log("KeyDown-----Space");
                break;
            case KeyCode.UpArrow:
                Debug.Log("KeyDown-----UpArrow");
                break;
        }
    }

    private void CheckUp(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.A:
                Debug.Log("KeyUp------A");
                break;
            case KeyCode.Space:
                Debug.Log("KeyDown-----Space");
                break;
            case KeyCode.UpArrow:
                Debug.Log("KeyDown-----UpArrow");
                break;
        }
    }

    private void Check(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.A:
                Debug.Log("KeyUp------A");
                break;
            case KeyCode.Space:
                Debug.Log("KeyDown-----Space");
                break;
            case KeyCode.UpArrow:
                Debug.Log("KeyDown-----UpArrow");
                break;
        }
    }
*/
