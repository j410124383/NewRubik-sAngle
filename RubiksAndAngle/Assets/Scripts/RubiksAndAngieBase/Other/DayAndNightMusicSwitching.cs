using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YProjectBase;

public class DayAndNightMusicSwitching : MonoBehaviour
{
    public enum DayAndNight
    {
        Day,
        Night,
    }

    [SerializeField] DayAndNight start = DayAndNight.Day;
    [SerializeField] DayAndNight end = DayAndNight.Night;
    [SerializeField] float transitionTime = 5f;

    DayAndNight target = DayAndNight.Day;

    private void OnEnable()
    {
        target = start;
        UpdatedBGMMusic();
    }

    private void OnDisable()
    {
        target = end;
        UpdatedBGMMusic();
    }

    private void OnDestroy()
    {
        target = end;
        UpdatedBGMMusic();
    }

    [ContextMenu("UpdatedBGMMusic")]
    public void UpdatedBGMMusic()
    {
        switch (target)
        {
            case DayAndNight.Day:
            default:
                if (GameDataController.GetInstance() != null && GameDataController.GetInstance().musicData != null)
                    UpdatedBGMMusic(GameDataController.GetInstance().musicData.GetBGMClip(0));
                break;
            case DayAndNight.Night:
                if (GameDataController.GetInstance() != null && GameDataController.GetInstance().musicData != null)
                    UpdatedBGMMusic(GameDataController.GetInstance().musicData.GetBGMClip(1));
                break;
        }
    }

    public void UpdatedBGMMusic(AudioClip _clip)
    {
        if (MusicMgr.GetInstance() != null)
        {
            if(MusicMgr.GetInstance().GetBGMusic != null)
            {
                if(MusicMgr.GetInstance().GetBGMusic.clip != _clip)
                    MusicMgr.GetInstance().PlayBGMMusicTransition(_clip, transitionTime);
            }
            else
            {
                switch (target)
                {
                    case DayAndNight.Day:
                    default:
                        if (GameDataController.GetInstance() != null && GameDataController.GetInstance().musicData != null)
                            MusicMgr.GetInstance().LoadPlayBGMusic(GameDataController.GetInstance().musicData.GetBGMClip(0));
                        break;
                    case DayAndNight.Night:
                        if (GameDataController.GetInstance() != null && GameDataController.GetInstance().musicData != null)
                            MusicMgr.GetInstance().LoadPlayBGMusic(GameDataController.GetInstance().musicData.GetBGMClip(1));
                        break;
                }
            }
        }
    }

}
