using UnityEngine;
using YProjectBase;

public class MainUIController : MonoBehaviour, IInit, IGameStart, IListener
{
    [SerializeField] GameObject sceneUIPerfab = null;

    MarkPanelUI maskUI = null;

    string sendSceneName = "MainMenu";
    float endGameWaitTime = 5f;
    bool isScene = false;
    bool isTempTime = false;
    bool isContiue;
    //RectTransform loadingUI;
    AsyncOperation operation;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {

        if (sceneUIPerfab != null)
        {
            GameObject sceneUIObj = GameObject.Find(sceneUIPerfab.name);
            if (sceneUIObj)
            {
                GameObject.Destroy(sceneUIObj);
            }
            ResMgr.GetInstance().loadAsync<GameObject>("UI/" + sceneUIPerfab.name, (o) =>
            {
                if (o != null)
                {
                    o.SetActive(true);
                    Canvas cav = o.GetComponent<Canvas>();
                    cav.worldCamera = Camera.main;
                    cav.sortingLayerName = "InputUI";
                    cav.sortingOrder = 10;
                    cav.planeDistance = 1;
                    cav = null;
                    maskUI = o.transform.GetComponentInChildren<MarkPanelUI>(true);

                    if (maskUI != null)
                    {
                        maskUI.Center = new Vector2(0.47f, 0.5f);
                        maskUI.Raidus = 1;
                        maskUI.TargetRaidus = 0;
                        if (maskUI.gameObject.activeSelf == true)
                        {
                            maskUI.gameObject.SetActive(false);
                        }
                    }

                }
            });
        }


        isScene = false;
        isTempTime = false;
        isContiue = false;
        operation = null; 
    }



    public void AddListener()
    {
        EventCenter.GetInstance().AddEventListener<string>("MainMuneToGameScene", SendToGameScene);
        EventCenter.GetInstance().AddEventListener<float>("Loading", GetProgress);
        MonoMgr.GetInstance().AddUpdateListener(EndGameTimer);
    }

    public void RemoveListener()
    {
        EventCenter.GetInstance().RemoveEventListener<string>("MainMuneToGameScene", SendToGameScene);
        EventCenter.GetInstance().RemoveEventListener<float>("Loading", GetProgress);
        MonoMgr.GetInstance().RemoveUpdateListener(EndGameTimer);
    }


    public void SendToGameScene(string sceneName)
    {

#if UNITY_EDITOR
        Debug.Log("SendToGameScene  :  " + InputController.GetInstance().GetFirstScreenPoint);
#endif

        if (maskUI)
        {
            maskUI.Center = InputController.GetInstance().GetFirstScreenPoint;
            maskUI.ShowUI();
        }
       LoadGameScene(sceneName);
    }


    /// <summary>
    /// 加载继续主界面场景
    /// </summary>
    /// <param name="_sceneName"></param>
    public void LoadGameScene(string _sceneName)
    {
        if (isScene == true) return;

        //Debug.Log("_startPanel.data  " + _startPanel.data.sceneName);

        if (ScenesMgr.GetInstance().IsValidCanLoadScene(_sceneName) == false) return;

        ScenesMgr.GetInstance().LoadSceneAsync(_sceneName, (s) => { operation = s; });
        ScenesMgr.GetInstance().ToGC();

        isScene = true;
        isContiue = false;
        isTempTime = true;
    }

    /// <summary>
    /// 场景加载进度
    /// </summary>
    /// <param name="_progress"></param>
    private void GetProgress(float _progress)
    {
        if (!isScene == true) return;

        if (_progress >= 0.9f)
        {
            if (endGameWaitTime <= 0 && !isContiue && !isTempTime)
            {
                isContiue = true;
                operation.allowSceneActivation = true;
                RemoveListener();
            }
        }
    }

    /// <summary>
    /// 游戏加载基本时长
    /// </summary>
    private void EndGameTimer()
    {
        if (!isScene == true) return;

        if (isTempTime)
        {
            //Debug.Log(Time.time);

            if (endGameWaitTime <= 0)
            {
                endGameWaitTime = 0;
                isTempTime = false;
                return;
            }
            endGameWaitTime -= Time.unscaledDeltaTime;
        }
    }


    public void GameStart()
    {
        AddListener();
    }

}
