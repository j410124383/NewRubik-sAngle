using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class MyClearConsole : Editor
{
    [MenuItem("我的工具/Clear %#&C")]
    static public void ClearContrl()
    {
       Type type = Type.GetType("UnityEditor.LogEntries,UnityEditor.dll");

        if(type == null)
        {
            Debug.Log("<color=red>type is null</color>");
            return;
        }

        if (type.GetMethod("Clear") != null)
        {
            type.GetMethod("Clear").Invoke(null, null);
        }
    }
}
