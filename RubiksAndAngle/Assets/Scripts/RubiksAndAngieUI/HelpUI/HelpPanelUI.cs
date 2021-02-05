using UnityEngine;
using UnityEngine.UI;
using YProjectBase;

public class HelpPanelUI : BasePanel, IListener
{

    [SerializeField] string backButtonName = "B_back";
    [SerializeField] string animName = "fade_In";

    [Space(20)] public IGameEvent backButtonEvent;

    private bool isButton;
    private Button backBtn;

    Animator sampleUI;
    int showAnimID;

    // 初始化 必须保留 base.Init();
    public override void Init()
    {
        EventCenter.GetInstance().RemoveEventListener(EventData.ShowHelpUI, ShowUI);

        base.Init();

        sampleUI = GetComponent<Animator>();
        showAnimID = AnimationMgr.GetInstance().GetAnimationID(animName);

        backBtn = GetControl<Button>(backButtonName);

        isButton = true;

        EventCenter.GetInstance().AddEventListener(EventData.ShowHelpUI, ShowUI);
    }

    private void OnDestroy()
    {
        RemoveListener();
        EventCenter.GetInstance().RemoveEventListener(EventData.ShowHelpUI, ShowUI);
    }


    /// <summary>
    /// 显示UI
    /// </summary>
    public override void ShowUI()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }

        //RemoveListener();
        AddListener();

        if (sampleUI != null)
            AnimationMgr.GetInstance().AnimPlay(sampleUI, showAnimID);

        isButton = false;

    }

    /// <summary>
    /// 隐藏UI
    /// </summary>
    public override void HideUI()
    {
        EventCenter.GetInstance().EventTrigger(EventData.HideHelpUI);
        RemoveListener();
        isButton = true;
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }


    public void BackButtonEvent()
    {
        if (GameDataController.GetInstance() != null && GameDataController.GetInstance().musicData != null)
            ButtonMusicEvent(GameDataController.GetInstance().musicData.GetSEClip(0), false);

        if (isButton) return;

        if (backButtonEvent != null)
        {
            backButtonEvent.Invoke(0);
        }

        isButton = true;

        HideUI();
    }


    public void ButtonMusicEvent(AudioClip _audioClip, bool isLoop = false)
    {
        if (MusicMgr.GetInstance() != null)
            MusicMgr.GetInstance().PlaySound(_audioClip, isLoop, 0.3f);
    }


    public void AddListener()
    {
        if (backBtn) backBtn.onClick.AddListener(BackButtonEvent);
    }

    public void RemoveListener()
    {
        if (backBtn) backBtn.onClick.RemoveListener(BackButtonEvent);
    }


    ///// <summary>
    ///// 前进显示 UI 事件
    ///// </summary>
    //public override void FowardShowUI() 
    //{  
    //	base.FowardShowUI();  
    //	
    //}

    ///// <summary>
    ///// 后退显示 UI 事件
    ///// </summary>
    //public override void BackShowUI() 
    //{
    //	base.BackShowUI(); 
    //	
    //}

    ///// <summary>
    ///// 前进隐藏 UI 事件
    ///// </summary>
    //public override void FowardHideUI()  
    //{
    //	base.FowardHideUI(); 
    //	
    //}

    ///// <summary>
    ///// 后退隐藏 UI 事件
    ///// </summary>
    //public override void BackHideUI() 
    //{ 
    //	base.BackHideUI(); 
    //	
    //}


}