using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBody 
{

    /// <summary>
    /// 更新旋转数据
    /// </summary>
    /// <param name="_aixY">存放Y轴数据的Trans</param>
    /// <param name="_aixX">存放X轴数据的Trans</param>
    /// <param name="_aixZ">存放Z轴数据的Trans</param>
    void UpdatedAixData();

    /// <summary>
    /// 重置旋转数据
    /// </summary>
    /// <param name="resRotate">源初旋转数据</param>
    /// <param name="_aixY">存放Y轴数据的Trans</param>
    /// <param name="_aixX">存放X轴数据的Trans</param>
    /// <param name="_aixZ">存放Z轴数据的Trans</param>
    void ResetAixData();

}
