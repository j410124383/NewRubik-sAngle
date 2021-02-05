using System.Collections;
using UnityEngine;
using YProjectBase;

public class SceneUIController : MonoBehaviour,IInit,IGameStart,IGameEnd,IListener
{

    [SerializeField] GameObject sceneUIPerfab = null;

    PlayPanelUI playStateUI = null;
    PausePanelUI pauseStateUI = null;
    MarkPanelUI endStateUI = null;
    HelpPanelUI helpStateUI = null;

    Transform whiteMask = null;
    UnityEngine.UI.Image whiteMaskSprite = null;
    bool isWhiting = false;

    Vector2 center = new Vector2(0.5f, 0.5f);
    BasePanel[] sceneUIs = null;

    readonly Color uWhite = new Color(1,1,1,0);

    [SerializeField] string mainSceneName = "MainMenu";
    float endGameWaitTime = 5f;
    bool isScene = false;
    bool isTempTime = false;
    bool isContiue;
    //RectTransform loadingUI;
    AsyncOperation operation;






    public void Init()
    {

        if (sceneUIPerfab != null)
        {
            GameObject sceneUIObj = GameObject.Find(sceneUIPerfab.name);
            if (sceneUIObj)
            {
                GameObject.Destroy(sceneUIObj);
            }
            ResMgr.GetInstance().loadAsync<GameObject>("UI/"+sceneUIPerfab.name, (o) =>
            {
                if(o != null)
                {
                    o.SetActive(true);
                    Canvas cav = o.GetComponent<Canvas>();
                    cav.worldCamera = Camera.main;
                    cav.sortingLayerName = "InputUI";
                    cav.sortingOrder = 10;
                    cav.planeDistance = 1;
                    cav = null;
                    InitUI(o);
                }
            });
        }
        isScene = false;
        isTempTime = false;
        AddListener();
    }

    private void OnDisable()
    {
        RemoveListener();
    }

    private void OnDestroy()
    {
        RemoveListener();
    }




    private void InitUI(GameObject _UIObj)
    {
        whiteMask = _UIObj.transform.GetChild(0).Find("WhiteMask");

        sceneUIs = GetSceneUI(_UIObj.transform,true);

        if(pauseStateUI != null)
        {
            if (pauseStateUI.gameObject.activeSelf == true)
            {
                pauseStateUI.gameObject.SetActive(false);
            }
        }

        if (playStateUI != null)
        {
            if(playStateUI.gameObject.activeSelf == false)
            {
                playStateUI.gameObject.SetActive(true);
            }

            playStateUI.ShowUI();
        }

        if (helpStateUI != null)
        {
            if (helpStateUI.gameObject.activeSelf == true)
            {
                helpStateUI.gameObject.SetActive(false);
            }
        }

        if (whiteMask != null)
        {
            whiteMaskSprite = whiteMask.GetComponent<UnityEngine.UI.Image>();

            if (whiteMask.gameObject.activeSelf == false)
            {
                whiteMask.gameObject.SetActive(true);
            }
            if(whiteMaskSprite != null)
            {
                whiteMaskSprite.color = uWhite;
                whiteMaskSprite.raycastTarget = false;
            }
           //whiteMaskSprite.
        }

    }

    public BasePanel[] GetSceneUI(Transform _UITrans, bool includeUnActive = false)
    {
        BasePanel[] panels = _UITrans.GetComponentsInChildren<BasePanel>(includeUnActive);

        foreach (var panel in panels)
        {
            if(panel != null)
            {
                panel.Init();

                if (panel is PausePanelUI && pauseStateUI != panel)
                {
                    pauseStateUI = panel as PausePanelUI;
                    continue;
                }
                else if (panel is PlayPanelUI && playStateUI != panel)
                {
                    playStateUI = panel as PlayPanelUI;
                    continue;
                }
                else if (panel is MarkPanelUI && endStateUI != panel)
                {
                    endStateUI = panel as MarkPanelUI;
                    continue;
                }
                else if (panel is HelpPanelUI && helpStateUI != panel)
                {
                    helpStateUI = panel as HelpPanelUI;
                    continue;
                }

            }
        }

        return panels;

    }



    public void GameContinue()
    {
        if (playStateUI != null && pauseStateUI != null)
        {
            UIController.FromUIToUI(pauseStateUI, playStateUI,UIState.BackShow);          
        }
    }

    public void GamePause()
    {
        if (playStateUI != null && pauseStateUI != null)
        {
            //pauseStateUI.ShowUI();
            UIController.FromUIToUI(playStateUI, pauseStateUI, UIState.FowardShow);
            //Debug.Log("FromUIToUI   GamePause");
        }
    }

    /// <summary>
    /// 白色幕布
    /// 重置游戏触发
    /// </summary>
    public void WhiteMaskEvent()
    {
        if(whiteMaskSprite != null)
        {
            isWhiting = false;
            StartCoroutine(WMaskEvent(0.15f));
            Debug.Log("WhiteMaskEvent");
        }
    }

    IEnumerator WMaskEvent(float _time)
    {
        isWhiting = true;
        if (whiteMaskSprite != null)
        {
            whiteMaskSprite.color = Color.white;
            whiteMaskSprite.raycastTarget = true;
        }
        float tempTime = 0f;
        yield return null;

        while (isWhiting)
        {
            tempTime += Time.unscaledDeltaTime;
            if(tempTime > _time)
            {
                isWhiting = false;
                break;
            }
            yield return null;
        }

        if (whiteMaskSprite != null)
        {
            whiteMaskSprite.color = uWhite;
            whiteMaskSprite.raycastTarget = false;
        }
    }


    public void GameStart()
    {
        if (playStateUI != null)
        {
            playStateUI.ShowUI();
        }
    }


    public void GameEnd(Transform _overPoint)
    {
        if (endStateUI != null)
        {
            Vector3 campoint = InputController.GetInstance().GetMainCam.WorldToScreenPoint(_overPoint.position);
            center = new Vector2(campoint.x / Screen.width, campoint.y / Screen.height);
            GameEnd();
        }
    }

    public void GameEnd(Vector2 _overPoint)
    {
        if (endStateUI != null)
        {
            center = _overPoint;
            GameEnd();
        }
    }

    public void GameEnd()
    {
        if (endStateUI != null)
        {
            endStateUI.Speed = 2;
            endStateUI.ShowUI(center,1,0);
        }

        LoadMainScene(mainSceneName);
    }




    /// <summary>
    /// 加载继续主界面场景
    /// </summary>
    /// <param name="_sceneName"></param>
    public void LoadMainScene(string _sceneName)
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
    public void GetProgress(float _progress)
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




    public void AddListener()
    {
        EventCenter.GetInstance().AddEventListener<float>("Loading", GetProgress);
        EventCenter.GetInstance().AddEventListener<Vector2>("GameEndVector2", GameEnd);
        MonoMgr.GetInstance().AddLateUpdateListener(EndGameTimer);
        EventCenter.GetInstance().AddEventListener(EventData.gameReset, WhiteMaskEvent);
    }

    public void RemoveListener()
    {
        EventCenter.GetInstance().RemoveEventListener<float>("Loading", GetProgress);
        EventCenter.GetInstance().RemoveEventListener<Vector2>("GameEndVector2", GameEnd);
        MonoMgr.GetInstance().RemoveLateUpdateListener(EndGameTimer);
        EventCenter.GetInstance().RemoveEventListener(EventData.gameReset, WhiteMaskEvent);
    }

}
