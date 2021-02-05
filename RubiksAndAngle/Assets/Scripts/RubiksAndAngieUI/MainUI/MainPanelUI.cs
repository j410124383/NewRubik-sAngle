using UnityEngine;
using UnityEngine.UI;
using YProjectBase;

public class MainPanelUI : BasePanel,IGameStart,IListener
{

    [SerializeField] string listButtonName = "ListButton";
    [SerializeField] string audioButtonName = "AudioButton";
    [SerializeField] string helpButtonName = "HelpButton";
    [SerializeField] string studioButtonName = "StudioButton"; 
    [SerializeField] string homeButtonName = "HomeButton";

    public MusicData musicData;
    [Space(20)]
    public IGameEvent mainUnAcitveEvent;
    public IGameEvent mainAcitveEvent;

    private bool isButton;
    private bool isPlayingMusic = true;


    private Button helpBtn;
    private Button listBtn;
    private Button audioBtn;
    private Button studioBtn;
    private Button homeBtn;

    private Image audioOpenImg;
    private Image audioCloseImg;



    public override void Init()
    {

        if (MusicMgr.GetInstance().GetBGMusic == null)
        {
            if (musicData)
                MusicMgr.GetInstance().LoadBGMusic(musicData.GetBGMClip(0));
            else
                MusicMgr.GetInstance().LoadBGMusic("BGM");
        }

        EventCenter.GetInstance().RemoveEventListener("PartUIToMainUI", ShowUI);
        base.Init();

        helpBtn = GetControl<Button>(helpButtonName);
        listBtn = GetControl<Button>(listButtonName);
        audioBtn = GetControl<Button>(audioButtonName);
        studioBtn = GetControl<Button>(studioButtonName);
        homeBtn = GetControl<Button>(homeButtonName);

        audioOpenImg = GetControl<Image>("AudioImageOpen");
        audioCloseImg = GetControl<Image>("AudioImageClose");

        if (helpBtn) helpBtn.onClick.RemoveAllListeners();
        if (listBtn) listBtn.onClick.RemoveAllListeners();
        if (audioBtn) audioBtn.onClick.RemoveAllListeners();
        if (studioBtn) studioBtn.onClick.RemoveAllListeners();
        if (homeBtn) homeBtn.onClick.RemoveAllListeners();

        if(musicData != null)
        {
            isPlayingMusic = musicData.isPlaying;
        }

        if (audioOpenImg != null && audioCloseImg != null)
        {
            if (isPlayingMusic)
            {
                if(!audioOpenImg.gameObject.activeSelf)
                    audioOpenImg.gameObject.SetActive(isPlayingMusic);

                if (audioCloseImg.gameObject.activeSelf)
                    audioCloseImg.gameObject.SetActive(!isPlayingMusic);
            }
            else
            {
                if (audioOpenImg.gameObject.activeSelf)
                    audioOpenImg.gameObject.SetActive(!isPlayingMusic);

                if (!audioCloseImg.gameObject.activeSelf)
                    audioCloseImg.gameObject.SetActive(isPlayingMusic);
            }
        }


        isButton = true;
        EventCenter.GetInstance().AddEventListener("PartUIToMainUI", ShowUI);
    }

    public override void ShowUI()
    {
        isButton = false;
        if(mainAcitveEvent != null)
        {
            mainAcitveEvent?.Invoke(0);
           // Debug.Log("ShowUI");
        }
    }

    public override void HideUI()
    {
        if (mainUnAcitveEvent != null)
        {
            mainUnAcitveEvent?.Invoke(0);
        }

        isButton = true;
    }


    private void OnDestroy()
    {
        RemoveListener();
    }


    public void HelpButtonEvent()
    {
        if (isButton) return;
        if (musicData != null)
        {
            ButtonMusicEvent(musicData.GetSEClip(0), false);
        }

        isButton = true;
        EventCenter.GetInstance().EventTrigger(EventData.ShowHelpUI);
        HideUI();
    }

    public void StudioButtonEvent()
    {
        if (isButton) return;
        if (musicData != null)
        {
            ButtonMusicEvent(musicData.GetSEClip(0), false);
        }

        isButton = true;
        EventCenter.GetInstance().EventTrigger(EventData.ShowStudioUI);
        HideUI();
    }

    public void ListButtonEvent()
    {
        if (isButton) return;
        if (musicData != null)
            ButtonMusicEvent(musicData.GetSEClip(0), false);

        isButton = true;
        EventCenter.GetInstance().EventTrigger("MainUIToPartUI");
        HideUI();
    }

    public void AudioButtonEvent()
    {
        if (musicData != null)
            ButtonMusicEvent(musicData.GetSEClip(0));

        if (isButton) return;

        isButton = true;
      
        if (isPlayingMusic)
        {

            if (audioOpenImg != null && audioCloseImg != null)
            {
                if (audioOpenImg.gameObject.activeSelf)
                    audioOpenImg.gameObject.SetActive(false);

                if (!audioCloseImg.gameObject.activeSelf)
                    audioCloseImg.gameObject.SetActive(true);
            }

            MusicMgr.GetInstance().PauseBGMusic();

            isPlayingMusic = false;
            isButton = false;
        }
        else
        {

            if (audioOpenImg != null && audioCloseImg != null)
            {
                if (!audioOpenImg.gameObject.activeSelf)
                    audioOpenImg.gameObject.SetActive(true);

                if (audioCloseImg.gameObject.activeSelf)
                    audioCloseImg.gameObject.SetActive(false);
            }

            if (MusicMgr.GetInstance().GetBGMusic != null)
            {
                if (MusicMgr.GetInstance().GetBGMusic.isPlaying == false)
                    MusicMgr.GetInstance().UnPauseBGMusic();
            }
            else
            {
                if(musicData)
                    MusicMgr.GetInstance().LoadPlayBGMusic(musicData.GetBGMClip(0));
                else
                    MusicMgr.GetInstance().LoadPlayBGMusic("BGM");
            }
            isPlayingMusic = true;
            isButton = false;
        }

        if (musicData != null)
            musicData.isPlaying = isPlayingMusic;
    }

    public void HomeButtonEvent()
    {
        if (isButton) return;
        if (musicData != null)
            ButtonMusicEvent(musicData.GetSEClip(0), false);

        if (InputController.GetInstance() != null && InputController.GetInstance().IsInputTwoDown)
        {
#if UNITY_EDITOR
            Debug.Log("HomeButtonEvent   : : :   Quit");
            UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log("HomeButtonEvent   : : :  need IsInputTwoDown");
#else
               
#endif
        }
    }


    public void ButtonMusicEvent(AudioClip _audioClip, bool isLoop = false)
    {
        if(MusicMgr.GetInstance() != null)
          MusicMgr.GetInstance().PlaySound(_audioClip, isLoop, 0.3f, true);       
    }


    public void GameStart()
    {
        isButton = false;

        EventCenter.GetInstance().RemoveEventListener("PartUIToMainUI", ShowUI);
        EventCenter.GetInstance().RemoveEventListener(EventData.HideHelpUI, ShowUI);

        if (GameDataController.GetInstance() != null && GameDataController.GetInstance().musicData != null)
            isPlayingMusic = GameDataController.GetInstance().musicData.isPlaying;

        if (isPlayingMusic)
        {
            if (MusicMgr.GetInstance().GetBGMusic == null)
            {
                if (musicData)
                    MusicMgr.GetInstance().LoadPlayBGMusic(musicData.GetBGMClip(0));
                else
                    MusicMgr.GetInstance().LoadPlayBGMusic("BGM");
            }
            else if (MusicMgr.GetInstance().BGMusicIsPlaing == false)
            {
                    MusicMgr.GetInstance().PlayBGMusic();
            }           
        }
        else
        {
            if (MusicMgr.GetInstance().GetBGMusic == null)
            {
                if (musicData)
                    MusicMgr.GetInstance().LoadBGMusic(musicData.GetBGMClip(0));
                else
                    MusicMgr.GetInstance().LoadBGMusic("BGM");
            }
        }

        if (audioOpenImg != null && audioCloseImg != null)
        {
             audioOpenImg.gameObject.SetActive(isPlayingMusic);
             audioCloseImg.gameObject.SetActive(!isPlayingMusic);
        }

        //RemoveListener();
        AddListener();
    }

    public void AddListener()
    {
        if (helpBtn) helpBtn.onClick.AddListener(HelpButtonEvent);
        if (listBtn) listBtn.onClick.AddListener(ListButtonEvent);
        if (audioBtn) audioBtn.onClick.AddListener(AudioButtonEvent);
        if (studioBtn) studioBtn.onClick.AddListener(StudioButtonEvent);
        if (homeBtn) homeBtn.onClick.AddListener(HomeButtonEvent);

        EventCenter.GetInstance().AddEventListener("PartUIToMainUI", ShowUI);
        EventCenter.GetInstance().AddEventListener(EventData.HideHelpUI, ShowUI);
        EventCenter.GetInstance().AddEventListener(EventData.HideStudioUI, ShowUI);
    }

    public void RemoveListener()
    {
        if (helpBtn) helpBtn.onClick.RemoveListener(HelpButtonEvent);
        if (listBtn) listBtn.onClick.RemoveListener(ListButtonEvent);
        if (audioBtn) audioBtn.onClick.RemoveListener(AudioButtonEvent);
        if (studioBtn) studioBtn.onClick.RemoveListener(StudioButtonEvent);
        if (homeBtn) homeBtn.onClick.RemoveListener(HomeButtonEvent);

        EventCenter.GetInstance().RemoveEventListener("PartUIToMainUI", ShowUI);
        EventCenter.GetInstance().RemoveEventListener(EventData.HideHelpUI, ShowUI);
        EventCenter.GetInstance().RemoveEventListener(EventData.HideStudioUI, ShowUI);
    }

}
