
using System.ComponentModel;
using UnityEngine;

namespace MyTools
{
    /// <summary>
    /// 继承于Unity的Debug 在Unity环境下使用
    /// </summary>
    public class MyDebug : Debug
    {
        public static void LogRed(object message)
        {
            Log("<color=red>"+message+"</color>");
        }

        public static void LogGreen(object message)
        {
            Log("<color=green>" + message + "</color>");
        }

        public static void LogBlue(object message)
        {
            Log("<color=blue>" + message + "</color>");
        }

        public static void ToDebugLog(object message,Color _color)
        {
            string colorName = "#" + ColorUtility.ToHtmlStringRGB(_color);

            if (colorName == string.Empty) return;

            string color = "<color=" + colorName + "> {0} </color>";

            LogFormat(color, message);
        }

    }
}

