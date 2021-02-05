using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YProjectBase;

public class PartPanelUI : BasePanel, IListener
{
    [SerializeField] string backButtonName = "B_back";
    [SerializeField] string animName = "MainMenuStrart";
    [SerializeField] PartUIData partUIDatas;
    [SerializeField] List<Transform> partTrans = new List<Transform>();


   // [Space(20)]//public IGameEvent backButtonEvent;
  //  public IGameEvent listButtonEvent;

    private bool isButton;


    private Button backBtn;
    private Button[] partBtn;
    private Button ABtn;

    private int[] btnPartID;


    Animator sampleUI;
    int showAnimID;


    public override void Init()
    {
        EventCenter.GetInstance().RemoveEventListener("MainUIToPartUI", ShowUI);

        btnPartID = new int[partTrans.Count];
        partBtn = new Button[partTrans.Count];
        Image[] partImage = new Image[partTrans.Count];
        Text[] textA = new Text[partTrans.Count];
        Text[] textB = new Text[partTrans.Count];

        Transform[] childTextA = new Transform[partTrans.Count];
        Transform[] childTextB= new Transform[partTrans.Count];
        Transform[] childButton = new Transform[partTrans.Count];

        if (partTrans != null && partTrans.Count > 0 && partUIDatas != null &&  partTrans.Count <= partUIDatas.buttonDatas.Length)
        {
            for (int i = 0; i < partTrans.Count; i++)
            {
                if (partTrans[i] == null) continue;
                childTextA[i] = partTrans[i].GetChild(0);
                childTextB[i] = partTrans[i].GetChild(1);
                childButton[i] = partTrans[i].GetChild(2);

                childTextA[i].name = i.ToString();
                childTextB[i].name = partUIDatas.buttonDatas[i].partIDName != string.Empty ? partUIDatas.buttonDatas[i].partIDName : ((char)(65 + i)).ToString(); 
                childButton[i].name = partUIDatas.buttonDatas[i].partName != string.Empty ? partUIDatas.buttonDatas[i].partName : partTrans[i].gameObject.name;
                btnPartID[i] = i;
            }
        }


        base.Init();

        sampleUI = GetComponent<Animator>();
        showAnimID = AnimationMgr.GetInstance().GetAnimationID(animName);

        backBtn = GetControl<Button>(backButtonName);


        if (partUIDatas != null && partUIDatas.buttonDatas != null && GameDataController.GetInstance() != null)
            {
                for (int j = 0; j < partUIDatas.buttonDatas.Length; j++)
                {
                    if (partUIDatas.buttonDatas[j] != null)
                    {
                        if (j == 0)
                        {
                            partUIDatas.buttonDatas[j].isLock = false;
                            continue;
                        }

                        if (GameDataController.GetInstance().IsPartClearance(partUIDatas.buttonDatas[j-1].partName))
                        {
                            partUIDatas.buttonDatas[j].isLock = false;
                            // Debug.Log("islock");
                        }
                        else
                        {
                            partUIDatas.buttonDatas[j].isLock = true;
                        }
                    }
                }
            }      


        if (partTrans != null && partUIDatas.buttonDatas != null)
        {
            for (int i = 0; i < partTrans.Count; i++)
            {
                btnPartID[i] = i;
                partBtn[i] = GetControl<Button>(childButton[i].name);
                partImage[i] = GetControl<Image>(childButton[i].name);

                if(partImage[i] && partUIDatas.buttonDatas[i] != null)
                    partImage[i].sprite = partUIDatas.buttonDatas[i].buttonImage;              

                if (partBtn[i] != null && partUIDatas.buttonDatas[i] != null) partBtn[i].interactable = !partUIDatas.buttonDatas[i].isLock;

                textA[i] = GetControl<Text>(childTextA[i].name);
                textB[i] = GetControl<Text>(childTextB[i].name);

                if (textA[i] && partUIDatas.buttonDatas[i] != null) textA[i].text = partUIDatas.buttonDatas[i].partName;
                if (textB[i]) textB[i].text = childTextB[i].name;
            }

        }

        ABtn = GetControl<Button>("ANAXIMANDER");

        partImage = null;
        textA = null;
        textB = null;
        childTextA = null;
        childTextB = null;
        childButton = null;

        isButton = true;
        EventCenter.GetInstance().AddEventListener("MainUIToPartUI", ShowUI);

    }

    private void OnEnable()
    {

        if (partTrans != null && partUIDatas.buttonDatas != null)
        {

            if (partUIDatas != null && partUIDatas.buttonDatas != null && GameDataController.GetInstance() != null)
            {
                for (int j = 0; j < partUIDatas.buttonDatas.Length; j++)
                {
                    if (partUIDatas.buttonDatas[j] != null)
                    {
                        if (j == 0)
                        {
                            partUIDatas.buttonDatas[j].isLock = false;
                            continue;
                        }

                        if (GameDataController.GetInstance().IsPartClearance(partUIDatas.buttonDatas[j-1].partName))
                        {
                            partUIDatas.buttonDatas[j].isLock = false;
                        }
                        else
                        {
                            partUIDatas.buttonDatas[j].isLock = true;
                        }
                    }
                }
            }

            for (int i = 0; i < partTrans.Count; i++)
            {
                btnPartID[i] = i;
                if (partBtn[i] != null && partUIDatas.buttonDatas[i] != null) partBtn[i].interactable = !partUIDatas.buttonDatas[i].isLock;
            }
        }

    }

    private void OnDestroy()
    {
        EventCenter.GetInstance().RemoveEventListener("MainUIToPartUI", ShowUI);
        RemoveListener();
    }


    public override void ShowUI()
    {
        RemoveListener();
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }

        //RemoveListener();

        if (sampleUI != null)
            AnimationMgr.GetInstance().AnimPlay(sampleUI, showAnimID);

        AddListener();

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


    public void BackButtonEvent()
    {
        if (GameDataController.GetInstance() != null && GameDataController.GetInstance().musicData != null)
            ButtonMusicEvent(GameDataController.GetInstance().musicData.GetSEClip(0), false);

        if (isButton) return;

        EventCenter.GetInstance().EventTrigger("PartUIToMainUI");
        isButton = true;
        HideUI();
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
    }


    private void ListButtonEvent(int _num)
    {
        if (isButton) return;
       // Debug.Log(_num);


        EventCenter.GetInstance().EventTrigger<int>("PartUIToListUI", _num);
        if(GameDataController.GetInstance() != null)
        {
            GameDataController.GetInstance().SetPartID(_num);
            if (GameDataController.GetInstance().musicData != null)
                ButtonMusicEvent(GameDataController.GetInstance().musicData.GetSEClip(0), false);
        }
        // HideUI();
        // if (gameObject.activeSelf)
        //     gameObject.SetActive(false);
        isButton = true;
        RemoveListener();
    }

    private void ListButtonEvent(ButtonInfo _buttonInfo)
    {
        ListButtonEvent(_buttonInfo.buttonID);
    }


    public void ButtonMusicEvent(AudioClip _audioClip, bool isLoop = false)
    {
        if (MusicMgr.GetInstance() != null)
            MusicMgr.GetInstance().PlaySound(_audioClip, isLoop, 0.3f, true);
    }


    public void AddListener()
    {
        if (backBtn) backBtn.onClick.AddListener(BackButtonEvent);
       // if (ABtn) ABtn.onClick.AddListener(()=> { ListButtonEvent(0); });
        if (partBtn != null && partBtn.Length > 0)
        {
            for (int i = 0; i < partBtn.Length; i++)
            {
                if (partBtn[i] == null) continue;
                ButtonInfo info = new ButtonInfo();
                info.buttonID = i;
                partBtn[i].onClick.AddListener( () => ListButtonEvent(info));
            }
        }

    }
    public void RemoveListener()
    {
        if (backBtn) backBtn.onClick.RemoveListener(BackButtonEvent);

        if (partBtn != null && partBtn.Length > 0)
        {
            for (int i = 0; i < partBtn.Length; i++)
            {
                if (partBtn[i] == null) continue;
                partBtn[i].onClick.RemoveAllListeners();
            }
        }

        // if (ABtn) ABtn.onClick.RemoveListener(() => { ListButtonEvent(0); });
    }

}

public class ButtonInfo
{
    public int buttonID;
    public string buttonName;
}