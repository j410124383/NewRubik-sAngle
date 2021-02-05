using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YProjectBase;

public class AnimationMgr : BaseManager<AnimationMgr>
{

    private Dictionary<string, int> animDataDic = new Dictionary<string, int>();

    /// <summary>
    /// 得到动画ID
    /// </summary>
    /// <param name="name">动画动画名字</param>
    /// <returns></returns>
    public int GetAnimationID(string name)
    {
        //判断是否存在事件
        if (animDataDic.ContainsKey(name))
        {
            return animDataDic[name];
        }
        else
        {
            animDataDic.Add(name,Animator.StringToHash(name));
            return animDataDic[name];
        }
    }

    /// <summary>
    /// 播放对应 Animator 中对应动画
    /// </summary>
    /// <param name="_anim">对像 Animator </param>
    /// <param name="_animID">对应动画 ID </param>
    /// <param name="_animLayer">对应动画层</param>
    public void AnimPlay(Animator _anim, int _animID, int _animLayer = 0)
    {
        if (_anim != null)
        {
            if (_anim.HasState(_animLayer, _animID))
            {
                _anim.Play(_animID);
            }
        }
    }

    /// <summary>
    /// 播放对应 Animator 中对应动画
    /// </summary>
    /// <param name="_anim">对像 Animator </param>
    /// <param name="_animName">对应动画名字</param>
    /// <param name="_animLayer">对应动画层</param>
    public void AnimPlay(Animator _anim, string _animName, int _animLayer = 0)
    {
        int _animID = GetAnimationID(_animName);

        if (_anim != null)
        {
            if (_anim.HasState(_animLayer, _animID))
            {
                _anim.Play(_animID);
            }
        }
    }


}
