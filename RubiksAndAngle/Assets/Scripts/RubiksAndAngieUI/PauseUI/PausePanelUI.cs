using UnityEngine;
using UnityEngine.UI;
using YProjectBase;

public class PausePanelUI : BasePanel, IListener
{

    [SerializeField] string playButtonName = "B_play";
    [SerializeField] string backPartButtonName = "B_backPart";
    [SerializeField] string musicButtonName = "B_music";
    [SerializeField] string helpButtonName = "B_help";

    [SerializeField] string animShowName = "MainMenuStrart";
    [SerializeField] string animHideName = "MainMenuStrart";

    SceneUIController sceneUIController;

    private bool isButton;
    private Animator sampleUI;
    private int showAnimID;
    private int hideAnimID;

    private Button playBtn;
    private Button backPartBtn;
    private Button musicBtn;
    private Button helpBtn;

    private Image musicImag;

    private Sprite musicOn;
    private Sprite musicOff;
    private bool isMusic;

    public override void Init()
    {
        base.Init();

        sampleUI = GetComponent<Animator>();
        showAnimID = AnimationMgr.GetInstance().GetAnimationID(animShowName);
        hideAnimID = AnimationMgr.GetInstance().GetAnimationID(animHideName);

        playBtn = GetControl<Button>(playButtonName);
        backPartBtn = GetControl<Button>(backPartButtonName);
        musicBtn = GetControl<Button>(musicButtonName);
        helpBtn = GetControl<Button>(helpButtonName);

        musicImag = GetControl<Image>(musicButtonName);

        if (musicBtn)
        {
            musicOn = musicBtn.spriteState.selectedSprite;
            musicOff = musicBtn.spriteState.disabledSprite;
        }

        isButton = true;
    }

    
    public override void ShowUI()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }

        if (sampleUI != null)
            AnimationMgr.GetInstance().AnimPlay(sampleUI, showAnimID);

        AddListener();

        isButton = false;
    }
    public override void HideUI()
    {
        if (sampleUI != null)
            AnimationMgr.GetInstance().AnimPlay(sampleUI, hideAnimID);

        RemoveListener();
        isButton = true;
    }

    public void UnActiveUI()
    {
        gameObject.SetActive(false);
    }

    public override void BackShowUI()
    {
        ShowUI();
    }
    public override void FowardShowUI()
    {
        ShowUI();
    }

    private void BackShow()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }

        if (sampleUI != null)
            AnimationMgr.GetInstance().AnimPlay(sampleUI, showAnimID);

        isButton = false;
    }

    private void PlayButtonEvent()
    {
        if (!isButton)
        {
            EventCenter.GetInstance().EventTrigger(EventData.gameContinue);

            if (GameDataController.GetInstance() != null && GameDataController.GetInstance().musicData != null)
                ButtonMusicEvent(GameDataController.GetInstance().musicData.GetSEClip(0), false);

            HideUI();
        }
    }
    private void BackPartButtonEvent()
    {
        if (!isButton)
        {
            EventCenter.GetInstance().EventTrigger<Vector2>("GameEndVector2", new Vector2(0.47f, 0.5f));
            if (GameDataController.GetInstance() != null && GameDataController.GetInstance().musicData != null)
                ButtonMusicEvent(GameDataController.GetInstance().musicData.GetSEClip(0), false);
            isButton = true;
        }
    }
    private void MusicButtonEvent()
    {
        if (!isButton)
        {
            isButton = true;

            if (musicImag != null && musicOn != null && musicOff != null)
            {
              
                if (MusicMgr.GetInstance() != null)
                {
                    if (MusicMgr.GetInstance().BGMusicIsPlaing)
                    {
                        MusicMgr.GetInstance().PauseBGMusic();
                        musicImag.sprite = musicOff;
                        isMusic = false;
                    }
                    else
                    {
                        //Debug.Log("MusicButtonEvent");
                        MusicMgr.GetInstance().UnPauseBGMusic();
                        musicImag.sprite = musicOn;
                        isMusic = true;
                    }

                    if (GameDataController.GetInstance() != null && GameDataController.GetInstance().musicData != null)
                    {
                        ButtonMusicEvent(GameDataController.GetInstance().musicData.GetSEClip(0), false);
                        GameDataController.GetInstance().musicData.isPlaying = isMusic;
                    }
                }
            }


            isButton = false;
        }
    }
    private void HelpButtonEvent()
    {
        if (!isButton)
        {
            EventCenter.GetInstance().EventTrigger(EventData.ShowHelpUI);

            if (GameDataController.GetInstance() != null && GameDataController.GetInstance().musicData != null)
                ButtonMusicEvent(GameDataController.GetInstance().musicData.GetSEClip(0), false);

            isButton = true;
        }
    }

    public void ButtonMusicEvent(AudioClip _audioClip, bool isLoop = false)
    {
        if (MusicMgr.GetInstance() != null)
            MusicMgr.GetInstance().PlaySound(_audioClip, isLoop, 0.3f, true);
    }

    public void AddListener()
    {
        if (playBtn) playBtn.onClick.AddListener(PlayButtonEvent);
        if (backPartBtn) backPartBtn.onClick.AddListener(BackPartButtonEvent);
        if (musicBtn) musicBtn.onClick.AddListener(MusicButtonEvent);
        if (helpBtn) helpBtn.onClick.AddListener(HelpButtonEvent);

        EventCenter.GetInstance().AddEventListener(EventData.HideHelpUI, BackShow);

    }
    public void RemoveListener()
    {
        if (playBtn) playBtn.onClick.RemoveListener(PlayButtonEvent);
        if (backPartBtn) backPartBtn.onClick.RemoveListener(BackPartButtonEvent);
        if (musicBtn) musicBtn.onClick.RemoveListener(MusicButtonEvent);
        if (helpBtn) helpBtn.onClick.RemoveListener(HelpButtonEvent);

        EventCenter.GetInstance().RemoveEventListener(EventData.HideHelpUI, BackShow);
    }

}
