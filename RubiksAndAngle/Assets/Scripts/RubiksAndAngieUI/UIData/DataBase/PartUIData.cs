using UnityEngine;

[CreateAssetMenu(fileName = "New PartUIData", menuName = "UIData/New PartUIData")]
[System.Serializable]
public class PartUIData :Scriptablebase
{
    public PartButtonData[] buttonDatas;

    public override void DefaultData()
    {
        pathName = "MainMuneData/PartUIData";
        dataName = "PartUIData";
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (buttonDatas == null || buttonDatas.Length <= 0) return;
        for (int i = 0; i < buttonDatas.Length; i++)
        {
            buttonDatas[i].partID = i;
            if (buttonDatas[i].partName != string.Empty)
            {
                buttonDatas[i].ButtonName = buttonDatas[i].partName;
            }
        }
    }

    [ContextMenu("DefultLock")]
    public void DefultLock()
    {
        if (buttonDatas == null || buttonDatas.Length <= 0) return;
        for (int i = 0; i < buttonDatas.Length; i++)
        {
            buttonDatas[i].InitIsLock();
        }
    }
#endif

    [System.Serializable]
    public class PartButtonData
    {
        [HideInInspector] [SerializeField] string buttonName;
        public string partName;
        public string partIDName;
        public int partID;
        public Sprite buttonImage;
        public bool isLock;


        public string ButtonName { set { buttonName = value; } get { return buttonName; } }

        public void InitIsLock()
        {
            this.isLock = this.partID >= 1 ? true : false;
        }

    }

}

