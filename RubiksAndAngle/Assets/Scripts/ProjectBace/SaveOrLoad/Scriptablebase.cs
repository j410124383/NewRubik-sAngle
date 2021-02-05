using UnityEngine;

[System.Serializable]
public class Scriptablebase : ScriptableObject
{
    [SerializeField]protected string pathName = "GameData/";
    [SerializeField]protected string dataName = "";

    public string PathName { get { return pathName; } }
    public string DataName { get { return dataName; }  set { dataName = value; } }

    public virtual void DefaultData()
    {

    }

}
