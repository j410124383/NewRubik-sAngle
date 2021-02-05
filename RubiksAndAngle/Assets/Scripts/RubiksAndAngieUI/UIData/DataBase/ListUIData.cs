using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ListUIData", menuName = "UIData/New ListUIData")]
[System.Serializable]
public class ListUIData : Scriptablebase
{
    public ListButtonRoom[] listButtonRoomDatas;

    private List<bool> secneLockList = new List<bool>();
    private List<string> secneNameList = new List<string>();
    private List<ListButtonRoom.ListButtonData> scenesList = new List<ListButtonRoom.ListButtonData>();


    private void OnEnable()
    {
        Init();
    }

    public void Init()
    {
        if (listButtonRoomDatas != null)
        {
            secneLockList.Clear();
            secneNameList.Clear();
            scenesList.Clear();
            for (int i = 0; i < listButtonRoomDatas.Length; i++)
            {
                if (listButtonRoomDatas[i] == null) continue;
                for (int j = 0; j < listButtonRoomDatas[i].listButtonDatas.Length; j++)
                {
                    if (listButtonRoomDatas[i].listButtonDatas[j] == null) continue;
                    secneNameList.Add(listButtonRoomDatas[i].listButtonDatas[j].sceneName);
                    secneLockList.Add(listButtonRoomDatas[i].listButtonDatas[j].isLock);
                    scenesList.Add(listButtonRoomDatas[i].listButtonDatas[j]);
                }
            }
        }
    }

    public override void DefaultData()
    {
        pathName = "MainMuneData/ListUIData";
        dataName = "ListUIData";
    }

    /// <summary>
    /// 获得场景锁的 bool 值
    /// </summary>
    /// <param name="_sceneName">场景名字</param>
    /// <returns></returns>
    public bool GetIsLock(string _sceneName, UnityEngine.Events.UnityAction<int> callback = null)
    {
        if (secneNameList == null || secneLockList == null) return false;

        if (secneNameList.Contains(_sceneName))
        {
            int num = secneNameList.FindIndex(sID => sID.Equals(_sceneName));
           // Debug.Log("GetIsLock " + num);
            callback?.Invoke(num);
           // Debug.Log("GetIsLock " + scenesList[num].isLock);
            return scenesList[num].isLock;
        }
        return false;
    }


    /// <summary>
    /// 获得场景索引值
    /// </summary>
    /// <param name="_sceneName">输入场景名字</param>
    /// <returns></returns>
    public int GetSceneIndex(string _sceneName)
    {
        if (secneNameList == null) return 0;

        if (secneNameList.Contains(_sceneName))
        {
            return secneNameList.IndexOf(_sceneName);
        }
        return 0;
    }

    /// <summary>
    /// 改变场景锁的状态
    /// </summary>
    /// <param name="_sceneName">输入场景名字</param>
    /// <param name="_isLock">输入 场景开关 的 bool 值</param>
    public void ChangeSceneLock(string _sceneName, bool _isLock)
    {
        if (secneNameList == null || secneLockList == null || scenesList == null) return;

        if (secneNameList.Contains(_sceneName))
        {
            int num = secneNameList.FindIndex(sID => sID.Equals(_sceneName));
            secneLockList[num] = _isLock;
            scenesList[num].isLock = _isLock;
        }
    }

    /// <summary>
    /// 改变场景锁的状态
    /// </summary>
    /// <param name="_sceneIndex">输入场景列表中的索引值</param>
    /// <param name="_isLock">输入 场景开关 的 bool 值</param>
    public void ChangeSceneLock(int _sceneIndex, bool _isLock)
    {
        if (secneLockList == null || scenesList == null || _sceneIndex < 0) return;

        if (scenesList.Count > _sceneIndex)
        {
            secneLockList[_sceneIndex] = _isLock;
            scenesList[_sceneIndex].isLock = _isLock;
        }
    }

    /// <summary>
    /// 加载记录
    /// </summary>
    /// <param name="_loadData"></param>
    public void LoadData(ListUIData _loadData)
    {
        if (_loadData == null || listButtonRoomDatas == null) return;
        for (int i = 0; i < listButtonRoomDatas.Length; i++)
        {
            if(listButtonRoomDatas[i] != null)
            {
                listButtonRoomDatas[i].LoadData(_loadData.listButtonRoomDatas[i]);
            }
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (listButtonRoomDatas == null || listButtonRoomDatas.Length <= 0) return;
        for (int i = 0; i < listButtonRoomDatas.Length; i++)
        {
            listButtonRoomDatas[i].ButtonRoomName = i.ToString();
            listButtonRoomDatas[i].Init();
        }
    }

    [ContextMenu("DefultLock")]
    public void DefultLock()
    {
        if (listButtonRoomDatas == null || listButtonRoomDatas.Length <= 0) return;
        for (int i = 0; i < listButtonRoomDatas.Length; i++)
        {
            listButtonRoomDatas[i].InitIsLock();
        }
    }

    [ContextMenu("DefultDontLock")]
    public void DefultDontLock()
    {
        if (listButtonRoomDatas == null || listButtonRoomDatas.Length <= 0) return;
        for (int i = 0; i < listButtonRoomDatas.Length; i++)
        {
            listButtonRoomDatas[i].DontLock();
        }
    }

#endif



    [System.Serializable]
    public class ListButtonRoom
    {
        [HideInInspector] [SerializeField] string buttonRoomName;

        public ListButtonData[] listButtonDatas;

        public string ButtonRoomName { get { return buttonRoomName; } set { buttonRoomName = value; } }

        public void Init()
        {
            if (listButtonDatas == null || listButtonDatas.Length <= 0) return;
            for (int i = 0; i < listButtonDatas.Length; i++)
            {
                listButtonDatas[i].listID = i;
                if (listButtonDatas[i].listName != string.Empty)               
                    listButtonDatas[i].ButtonName = listButtonDatas[i].listName;
            }
        }

        public void InitIsLock()
        {
            if (listButtonDatas == null || listButtonDatas.Length <= 0) return;
            for (int i = 0; i < listButtonDatas.Length; i++)
            {
                listButtonDatas[i].InitIsLock();
            }
        }

        public void DontLock()
        {
            if (listButtonDatas == null || listButtonDatas.Length <= 0) return;
            for (int i = 0; i < listButtonDatas.Length; i++)
            {
                listButtonDatas[i].DontLock();
            }
        }

        public bool SceneClearance()
        {
            if(listButtonDatas == null) return false;

            for (int i = 0; i < listButtonDatas.Length; i++)
            {
                if (listButtonDatas[i] == null) continue;
                if (listButtonDatas[i].isLock == false) continue;
                return false;
            }

            return true;
        }

        /// <summary>
        /// 加载记录
        /// </summary>
        /// <param name="_loadData"></param>
        public void LoadData(ListButtonRoom _loadData)
        {
            if (_loadData == null || listButtonDatas == null) return;

            for (int i = 0; i < listButtonDatas.Length; i++)
            {
                if (listButtonDatas[i] == null || _loadData.listButtonDatas[i] == null) continue;
                listButtonDatas[i].LoadData(_loadData.listButtonDatas[i]);
            }
        }


        [System.Serializable]
        public class ListButtonData
        {

            [HideInInspector] [SerializeField] string buttonName;
            public string listName;
            public int listID;
            public Sprite buttonImage;
            public string sceneName;
            public bool isLock;

            public string ButtonName { get { return buttonName; } set { buttonName = value; } }

            public void InitIsLock()
            {
                this.isLock = true;
            }

            public void DontLock()
            {
                this.isLock = false;
            }

            /// <summary>
            /// 加载记录
            /// </summary>
            /// <param name="_loadData"></param>
            public void LoadData(ListButtonData _loadData)
            {
                if (_loadData == null) return;
                isLock = _loadData.isLock;
            }

        }


    }

}


