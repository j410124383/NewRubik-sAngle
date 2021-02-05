using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MusicData", menuName = "GameData/New MusicData")]
[System.Serializable]
public class MusicData : Scriptablebase
{
    public bool isPlaying = true;

    [SerializeField] private AudioClip[] bgmClips;
    [SerializeField] private AudioClip[] seClips;

    private Dictionary<string, AudioClip> audioClipDic = new Dictionary<string, AudioClip>();


    public AudioClip[] GetBGMClips {  get { return bgmClips; } }
    public AudioClip[] GetSEClips { get { return seClips; } }


    private void Awake()
    {
        if (audioClipDic.Count != (bgmClips.Length + seClips.Length))
        {
            audioClipDic.Clear();

            if (bgmClips != null && bgmClips.Length > 0)
            {
                for (int i = 0; i < bgmClips.Length; i++)
                {
                    if (bgmClips[i] == null) continue;
                    audioClipDic.Add(bgmClips[i].name, bgmClips[i]);
                }
            }

            if (seClips != null && seClips.Length > 0)
            {
                for (int i = 0; i < seClips.Length; i++)
                {
                    if (seClips[i] == null) continue;
                    audioClipDic.Add(seClips[i].name, seClips[i]);
                }
            }
        }
        
    }

    public void Init()
    {
        if (audioClipDic.Count != (bgmClips.Length + seClips.Length))
        {
            audioClipDic.Clear();

            if (bgmClips != null && bgmClips.Length > 0)
            {
                for (int i = 0; i < bgmClips.Length; i++)
                {
                    if (bgmClips[i] == null) continue;
                    audioClipDic.Add(bgmClips[i].name, bgmClips[i]);
                }
            }

            if (seClips != null && seClips.Length > 0)
            {
                for (int i = 0; i < seClips.Length; i++)
                {
                    if (seClips[i] == null) continue;
                    audioClipDic.Add(seClips[i].name, seClips[i]);
                }
            }
        }
    }


    /// <summary>
    /// 通过名字获得 AudioClip
    /// </summary>
    /// <param name="_clipName"></param>
    /// <returns></returns>
    public AudioClip GetAudioClip(string _clipName)
    {
        if (audioClipDic == null) return null;
        if (!audioClipDic.ContainsKey(_clipName)) return null;

        return audioClipDic[_clipName];
    }

    /// <summary>
    /// 通过索引获得 BGMClip
    /// </summary>
    /// <param name="_clipNum"></param>
    /// <returns></returns>
    public AudioClip GetBGMClip(int _clipNum)
    {
        if (bgmClips == null || bgmClips.Length <= 0) return null;
        if(_clipNum > bgmClips.Length - 1 || _clipNum < 0) return null;
        return bgmClips[_clipNum];
    }
    /// <summary>
    /// 通过名字获得 BGMClip
    /// </summary>
    /// <param name="_clipName"></param>
    /// <returns></returns>
    public AudioClip GetBGMClip(string _clipName)
    {
        if (bgmClips == null || bgmClips.Length <= 0) return null;

        for (int i = 0; i < bgmClips.Length; i++)
        {
            if (bgmClips[i] == null) continue;

            if (bgmClips[i].name.Contains(_clipName))
            {
                return bgmClips[i];
            }
        }

        return null;
    }

    /// <summary>
    /// 通过索引获得 SEClip
    /// </summary>
    /// <param name="_clipNum"></param>
    /// <returns></returns>
    public AudioClip GetSEClip(int _clipNum)
    {
        if (seClips == null || seClips.Length <= 0) return null;
        if (_clipNum > seClips.Length - 1 || _clipNum < 0) return null;
        return seClips[_clipNum];
    }
    /// <summary>
    /// 通过名字获得 GetSEClip
    /// </summary>
    /// <param name="_clipName"></param>
    /// <returns></returns>
    public AudioClip GetSEClip(string _clipName)
    {
        if (seClips == null || seClips.Length <= 0) return null;

        for (int i = 0; i < seClips.Length; i++)
        {
            if (seClips[i] == null) continue;

            if (seClips[i].name.Contains(_clipName))
            {
                return seClips[i];
            }
        }

        return null;
    }



}
