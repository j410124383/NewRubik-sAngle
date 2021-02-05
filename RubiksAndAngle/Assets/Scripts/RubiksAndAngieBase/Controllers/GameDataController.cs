using UnityEngine;
using YProjectBase;

/// <summary>
///  继承于Mono的单例对象 需要自行确保唯一性
/// </summary>
public class GameDataController : SingletonMonoBehaviour<GameDataController>
{
    //public GameData gameData;
    public MusicData musicData;

    public ListUIData sceneData;
    public PartUIData partData;

    private int partID = 0;
    public int GetPartID
    {
        get
        {
            return partID;
        }
      
    }

    public void SetPartID(int _partID)
    {
        if (_partID >= 0 && _partID < partData.buttonDatas.Length)
            partID = _partID;
    }

    private void Awake()
    {
        if (musicData == null)
            musicData = Resources.Load<MusicData>("MusicData/MusicData");
        //ResMgr.GetInstance().loadAsync<MusicData>("ListData/ListUIData", (m)=> { musicData = m; }); 

        if (sceneData == null)
            sceneData = Resources.Load<ListUIData>("ListData/ListUIData");

        if (partData == null)
            partData = Resources.Load<PartUIData>("PartData/PartData");
    }

    private void OnEnable()
    {
        GameLoad();
    }

    private void OnDestroy()
    {
        GameSave();
    }

    public void GameLoad()
    {
        GameSaveManager<MusicData>.GetInstance().LoadGame(musicData, (m) => { if (m != null) musicData.isPlaying = m.isPlaying; });
        GameSaveManager<ListUIData>.GetInstance().LoadGame(sceneData, (s) => { if (s != null) sceneData.LoadData(s); });
       // GameSaveManager<PartUIData>.GetInstance().LoadGame(partData, (p) => { if (p != null) partData = p; });
    }

    public void GameSave()
    {
        GameSaveManager<MusicData>.GetInstance().SaveGame(musicData);
        GameSaveManager<ListUIData>.GetInstance().SaveGame(sceneData);
        //GameSaveManager<PartUIData>.GetInstance().SaveGame(partData);
    }

    public bool IsPartClearance(string partName)
    {
        if (sceneData == null || partData == false) return false;

        if (partData.buttonDatas != null && sceneData.listButtonRoomDatas != null)
        {
            for (int i = 0; i < partData.buttonDatas.Length; i++)
            {
                if (partData.buttonDatas[i] != null && partData.buttonDatas[i].partName.Contains(partName))
                {
                    if(sceneData.listButtonRoomDatas[i] != null)
                    {
                        return sceneData.listButtonRoomDatas[i].SceneClearance();
                    }             
                }
            }
        }

        return false;
    }

}