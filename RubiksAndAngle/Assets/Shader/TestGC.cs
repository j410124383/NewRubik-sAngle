using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YProjectBase;

public class TestGC : MonoBehaviour
{
   
    [ContextMenu("ToGC")]
    public void ToGC()
    {
        //System.GC.Collect();
        ScenesMgr.GetInstance().ToGC();
    }

}
