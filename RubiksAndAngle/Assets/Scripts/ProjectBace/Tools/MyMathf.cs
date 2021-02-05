
using UnityEngine;

public class MyMathf 
{
    static public bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        // 根据Layer数值进行移位获得用于运算的Mask值
        int objLayerMask = 1 << obj.layer;
        return (layerMask.value & objLayerMask) > 0;
    }
}
