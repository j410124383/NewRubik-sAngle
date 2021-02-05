using UnityEngine;
using YProjectBase;

[CreateAssetMenu(fileName = "New GameData", menuName = "GameData/New GameData")]
[System.Serializable]
public class GameData : Scriptablebase
{
    [Header("Init Setting")]
    [Tooltip("op 数量")] public int opNum = 1;
    [Tooltip("临时 op 数量")] [Disabled] public int tempOpNum = 1;
    [Tooltip("过关时长")] [Disabled] public string gameClearanceTime = "00:00";
    [Tooltip("过关日期")] [Disabled] public string gameClearanceDate = "2021/1/24/ 00:00:00";

    [HideInInspector] public System.DateTime startTime;

    private void OnValidate()
    {
        opNum = opNum >= 1 ? opNum : 1;
        dataName = name;
    }
   
}