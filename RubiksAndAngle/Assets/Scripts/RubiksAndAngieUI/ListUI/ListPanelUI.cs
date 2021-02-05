using UnityEngine;
using UnityEngine.UI;
using YProjectBase;

public class ListPanelUI : BasePanel, IListener
{
    [SerializeField] string backButtonName = "B_back";
    [SerializeField] string animName = "MainMenuStrart";

    public ListUIData listUIData;

    public Transform[] roomArray;

    [Space(20)]public IGameEvent backButtonEvent;
    private bool isButton;


    private Button backBtn;
    private Button aBtn;
    private Button bBtn;
    private Button cBtn;
    private Button dBtn;


    Image[] listImages;
    Button[] listBtns;
    Text[] listTexts;


    Animator sampleUI;
    int showAnimID;

    public override void Init()
    {
        EventCenter.GetInstance().RemoveEventListener<int>("PartUIToListUI", ShowUI);

        base.Init();

        sampleUI = GetComponent<Animator>();
        showAnimID = AnimationMgr.GetInstance().GetAnimationID(animName);

        backBtn = GetControl<Button>(backButtonName);


        aBtn = GetControl<Button>("gridA");
        bBtn = GetControl<Button>("gridB");
        cBtn = GetControl<Button>("gridC");
        dBtn = GetControl<Button>("gridD");

        isButton = true;


        EventCenter.GetInstance().AddEventListener<int>("PartUIToListUI", ShowUI);
        
    }

    public void InitRoom()
    {
        listImages = new Image[roomArray.Length];
        listBtns = new Button[roomArray.Length];
        listTexts = new Text[roomArray.Length];

        if (roomArray != null && roomArray.Length > 0)
        {
            for (int i = 0; i < roomArray.Length; i++)
            {
                listTexts[i] = roomArray[i].GetChild(0).GetComponent<Text>();
                listBtns[i] = roomArray[i].GetChild(1).GetComponent<Button>();
                listImages[i] = roomArray[i].GetChild(1).GetComponent<Image>();
            }
        }

    }


    private void OnDestroy()
    {
        EventCenter.GetInstance().RemoveEventListener<int>("PartUIToListUI", ShowUI);
    }


    public void StartShowUI()
    {       
        UpdateRoomData(GameDataController.GetInstance().GetPartID);
        ShowUI();
    }

    public void ShowUI(int PartID)
    {
#if UNITY_EDITOR
        Debug.Log("ListPanelUI  :  " +PartID);
#endif
        UpdateRoomData(PartID);

        ShowUI();
    }

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

    public override void HideUI()
    {
        RemoveListener();
        isButton = true;
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }


    private void UpdateRoomData(int _roomNum)
    {
        if (listUIData == null) return;

        for (int i = 0; i < listBtns.Length; i++)
        {
            if (listBtns[i] == null) continue;
            listBtns[i].onClick.RemoveAllListeners();
        }

        if (listUIData.listButtonRoomDatas.Length <= _roomNum && listUIData.listButtonRoomDatas[_roomNum].listButtonDatas.Length <= 0) return;
        ListUIData.ListButtonRoom room = listUIData.listButtonRoomDatas[_roomNum];
        for (int i = 0; i < room.listButtonDatas.Length; i++)
        {
            if (listImages[i] != null && room.listButtonDatas[i].buttonImage != null)
                listImages[i].sprite = room.listButtonDatas[i].buttonImage;

            if (listTexts[i] != null && room.listButtonDatas[i].listName != null)
                listTexts[i].text = room.listButtonDatas[i].listName;


            if (listBtns != null)
            {
                if (listBtns[i] == null) continue;

                ButtonInfo info = new ButtonInfo();
                info.buttonName = room.listButtonDatas[i].sceneName;
                listBtns[i].onClick.AddListener(() => ListButtonEvent(info));
                if(i > 0)
                    listBtns[i].interactable = !room.listButtonDatas[i - 1].isLock;
#if UNITY_EDITOR
                if (i > 0)
                    Debug.Log("ListPanelUI interactable :  " + !room.listButtonDatas[i - 1].isLock);
                Debug.Log("ListPanelUI  :  " + info.buttonName);
#endif
            }

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

    public void ListButtonEvent(ButtonInfo _sceneID)
    {
        ListButtonEvent(_sceneID.buttonName);
    }

    public void ListButtonEvent(string _sceneName)
    {
        //Debug.Log(_sceneName);
        /* ScenesMgr.GetInstance()*/
        //if (isButton) return;
        // isButton = true;
        if (GameDataController.GetInstance() != null && GameDataController.GetInstance().musicData != null)
            ButtonMusicEvent(GameDataController.GetInstance().musicData.GetSEClip(0), false);

        EventCenter.GetInstance().EventTrigger<string>("MainMuneToGameScene", _sceneName);
        isButton = true;
#if UNITY_IOS || UNITY_ANDROID
        if (isButton) return;
        isButton = true;
        InputController.GetInstance().IphoneShake();
#endif

    }


    public void ButtonMusicEvent(AudioClip _audioClip, bool isLoop = false)
    {
        if (MusicMgr.GetInstance() != null)
            MusicMgr.GetInstance().PlaySound(_audioClip, isLoop, 0.3f, true);
    }


    public void AddListener()
    {
        if (backBtn) backBtn.onClick.AddListener(BackButtonEvent);
    }

    public void RemoveListener()
    {
        if (backBtn) backBtn.onClick.RemoveListener(BackButtonEvent);
    }


}
