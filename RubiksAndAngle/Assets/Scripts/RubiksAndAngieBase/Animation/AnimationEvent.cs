using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YProjectBase;

public class AnimationEvent : MonoBehaviour
{
    public IGameEvent gameStartEvent;

    Animation anim;

    bool isPlaying;
    bool isEvent;

    public bool IsPlaying
    {
        get
        {
            return isPlaying;
        }
    }

    public bool IsEvent
    {
        get
        {
            return isEvent;
        }
        set
        {
            isEvent = value;
        }
    }

    private void Awake()
    {
        anim = GetComponent<Animation>();
        isPlaying = true;
    }

    private void OnDestroy()
    {
        MonoMgr.GetInstance().RemoveLateUpdateListener(UpdatedIsPlaying);
        MonoMgr.GetInstance().RemoveLateUpdateListener(AnimEndEvent);
    }

    public void UpdatedIsPlaying()
    {
        if (anim == null) return;
        isPlaying = anim.isPlaying;
    }

    void AnimEndEvent()
    {
        if (isEvent) return;

        //if (anim == null) return;
        if (!isPlaying)
        {
            if (gameStartEvent != null)
            {
                isEvent = true;
                //Debug.Log("GameStart");
                gameStartEvent?.Invoke(null);
                MonoMgr.GetInstance().RemoveLateUpdateListener(UpdatedIsPlaying);
                MonoMgr.GetInstance().RemoveLateUpdateListener(AnimEndEvent);
            }
        }
    }

    public void PlayAnim()
    {
        if (anim == null) return;
        anim.Play();
        MonoMgr.GetInstance().AddLateUpdateListener(UpdatedIsPlaying);
        MonoMgr.GetInstance().AddLateUpdateListener(AnimEndEvent);
    }

}
