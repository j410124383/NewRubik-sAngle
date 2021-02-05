using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YProjectBase;

public class PlayPanelUI : BasePanel, IListener
{

    [SerializeField] string pauseButtonName = "B_pause";
    [SerializeField] string replayButtonName = "B_replay";

    SceneUIController sceneUIController;

    private Button pauseBtn;
    private Button replayBtn;

    private bool isButton;



    public override void Init()
    {
        base.Init();

        pauseBtn = GetControl<Button>(pauseButtonName);
        replayBtn = GetControl<Button>(replayButtonName);

        isButton = true;

        if (replayBtn) replayBtn.onClick.RemoveAllListeners();
        if (pauseBtn) pauseBtn.onClick.RemoveAllListeners();
    }


    public override void ShowUI()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }

        AddListener();
        isButton = false;
    }
    public override void HideUI()
    {
        RemoveListener();
        isButton = true;
    }


    public override void BackShowUI()
    {
        ShowUI();
    }
    public override void FowardShowUI()
    {
        ShowUI();
    }


    private void PauseButtonEvent()
    {
        if (!isButton)
        {
            EventCenter.GetInstance().EventTrigger(EventData.gamePause);
            if (GameDataController.GetInstance() != null && GameDataController.GetInstance().musicData != null)
                ButtonMusicEvent(GameDataController.GetInstance().musicData.GetSEClip(0), false);
            HideUI();
        }
    }
    private void ReplayButtonEvent()
    {
        if (!isButton)
        {
            EventCenter.GetInstance().EventTrigger(EventData.gameReset);
            if (GameDataController.GetInstance() != null && GameDataController.GetInstance().musicData != null)
                ButtonMusicEvent(GameDataController.GetInstance().musicData.GetSEClip(0), false);
        }
    }

    public void ButtonMusicEvent(AudioClip _audioClip, bool isLoop = false)
    {
        if (MusicMgr.GetInstance() != null)
            MusicMgr.GetInstance().PlaySound(_audioClip, isLoop, 0.3f, true);
    }


    public void AddListener()
    {
        if (replayBtn) replayBtn.onClick.AddListener(ReplayButtonEvent);
        if (pauseBtn) pauseBtn.onClick.AddListener(PauseButtonEvent);
       // Debug.Log("PlayPanelUI");
    }
    public void RemoveListener()
    {
        if (replayBtn) replayBtn.onClick.RemoveListener(ReplayButtonEvent);
        if (pauseBtn) pauseBtn.onClick.RemoveListener(PauseButtonEvent);
    }

}
