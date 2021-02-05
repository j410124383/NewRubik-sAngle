using System;
using UnityEngine;


namespace YProjectBase
{
    /// <summary>
    /// 对 UnityEvent 显示不支持
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct, Inherited = true, AllowMultiple = true)]
    public class DisabledAttribute : PropertyAttribute
    {


    }
}