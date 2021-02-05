using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace YProjectBase
{
    public class MusicMgr : BaseManager<MusicMgr>
    {

        private Dictionary<string, AudioClip> audioClipsDic = new Dictionary<string, AudioClip>();          //AudioClip字典
        private Dictionary<string, MusicPoolData> poolDic = new Dictionary<string, MusicPoolData>();   //音效对象池

        private AudioSource bgMusic = null;
        private AudioSource bgTargetMusic = null; //过渡目标音乐
        private float bgValue = 0.5f;
        private bool isPause;
        bool isTransition;

        private float soundValue = 0.3f;

        private GameObject soundObj = null;
        private List<AudioSource> soundList = new List<AudioSource>();

        private GameObject musicMgrObj;

        public AudioSource GetBGMusic
        {
            get
            {
                return bgMusic;
            }
        }

        public bool BGMusicIsPlaing
        {
            get
            {
                if (bgMusic)
                    return bgMusic.isPlaying;
                else return false;
            }
        }


        public void SetAudioClipDictionary(Dictionary<string, AudioClip> _audioClipsDic)
        {
            if (audioClipsDic == null || audioClipsDic.Count <= 0) return;
            this.audioClipsDic = _audioClipsDic;
        }


        public MusicMgr()
        {
            if(musicMgrObj == null)
            {
                musicMgrObj = new GameObject("MusicMgr");
                GameObject.DontDestroyOnLoad(musicMgrObj);
            }
            MonoMgr.GetInstance().AddUpdateListener(UpdateSound);
        }

        private void UpdateSound()
        {
            for (int i = soundList.Count - 1; i >= 0; --i)
            {
                if (!soundList[i].isPlaying && soundList[i] != null)
                {
                    PushSource(soundList[i]);
                    soundList.RemoveAt(i);
                }
            }
        }


        /// <summary>
        /// 播放背景音乐
        /// </summary>
        /// <param name="name">音乐名字</param>
        public void LoadPlayBGMusic(string name, UnityAction<AudioSource> callback = null)
        {
            if (bgMusic == null)
            {
                //Debug.Log("Plyer");
                GameObject obj = new GameObject();
                obj.name = "BGMusic";
                bgMusic = obj.AddComponent<AudioSource>();
                bgTargetMusic = obj.AddComponent<AudioSource>();
                //PushSource(bgTargetMusic);
                bgMusic.transform.SetParent(musicMgrObj.transform);
            }
            ResMgr.GetInstance().loadAsync<AudioClip>("Music/BG/" + name, (clip) =>
            {
                if (clip != null)
                {
                    if (!audioClipsDic.ContainsValue(clip))
                    {
                        if (!audioClipsDic.ContainsKey(clip.name))
                        {
                            audioClipsDic.Add(clip.name, clip);
                        }
                    }

                    bgMusic.clip = clip;
                }
                bgMusic.loop = true;
                bgMusic.volume = bgValue;
                bgMusic.playOnAwake = false;
                // bgMusic.i
                bgMusic.Play();
            });
        }

        /// <summary>
        /// 播放背景音乐
        /// </summary>
        /// <param name="name">音乐名字</param>
        public void LoadPlayBGMusic(AudioClip clip, UnityAction<AudioSource> callback = null)
        {
            if (bgMusic == null)
            {
                //Debug.Log("Plyer");
                GameObject obj = new GameObject();
                obj.name = "BGMusic";
                bgMusic = obj.AddComponent<AudioSource>();
                bgTargetMusic = obj.AddComponent<AudioSource>();
                //PushSource(bgTargetMusic);
                bgMusic.transform.SetParent(musicMgrObj.transform);

                if (clip != null)
                {
                    if (!audioClipsDic.ContainsValue(clip))
                    {
                        if (!audioClipsDic.ContainsKey(clip.name))
                        {
                            audioClipsDic.Add(clip.name, clip);
                        }
                    }

                    bgMusic.clip = clip;
                }
                bgMusic.loop = true;
                bgMusic.volume = bgValue;
                bgMusic.playOnAwake = false;
                bgMusic.Play();
                callback?.Invoke(bgMusic);
            }
            else
            {
                if (clip != null)
                {
                    if (!audioClipsDic.ContainsValue(clip))
                    {
                        if (!audioClipsDic.ContainsKey(clip.name))
                        {
                            audioClipsDic.Add(clip.name, clip);
                        }
                    }

                    if (bgMusic.clip != clip)
                    {
                        bgMusic.clip = clip;
                    }
                }

                bgMusic.Play();
                callback?.Invoke(bgMusic);
            }
        }

        /// <summary>
        /// 加载背景音乐
        /// </summary>
        /// <param name="name">音乐名字</param>
        public void LoadBGMusic(string name, UnityAction<AudioSource> callback = null)
        {
            if (bgMusic == null)
            {
                //Debug.Log("Plyer");
                GameObject obj = new GameObject();
                obj.name = "BGMusic";
                bgMusic = obj.AddComponent<AudioSource>();
                bgTargetMusic = obj.AddComponent<AudioSource>();
               // PushSource(bgTargetMusic);
                bgMusic.transform.SetParent(musicMgrObj.transform);
            }

            ResMgr.GetInstance().loadAsync<AudioClip>("Music/BG/" + name, (clip) =>
            {

                if (clip != null)
                {
                    if (!audioClipsDic.ContainsValue(clip))
                    {
                        if (!audioClipsDic.ContainsKey(clip.name))
                        {
                            audioClipsDic.Add(clip.name, clip);
                        }
                    }

                    bgMusic.clip = clip;
                }
                bgMusic.loop = true;
                bgMusic.volume = bgValue;
                bgMusic.playOnAwake = false;
                bgMusic.Stop();
                isPause = false;
            });
        }

        /// <summary>
        /// 加载背景音乐
        /// </summary>
        /// <param name="name">音乐clip</param>
        public void LoadBGMusic(AudioClip clip, UnityAction<AudioSource> callback = null)
        {
            if (bgMusic == null)
            {
                GameObject obj = new GameObject();
                obj.name = "BGMusic";
                bgMusic = obj.AddComponent<AudioSource>();
                bgTargetMusic = obj.AddComponent<AudioSource>();
              //  PushSource(bgTargetMusic);
                bgMusic.transform.SetParent(musicMgrObj.transform);

                if (clip != null)
                {
                    if (!audioClipsDic.ContainsValue(clip))
                    {
                        if (!audioClipsDic.ContainsKey(clip.name))
                        {
                            audioClipsDic.Add(clip.name, clip);
                        }
                    }

                    bgMusic.clip = clip;
                }
                bgMusic.loop = true;
                bgMusic.volume = bgValue;
                bgMusic.playOnAwake = false;
                bgMusic.Stop();
                callback?.Invoke(bgMusic);
            }
            else
            {

                if (clip != null)
                {
                    if (!audioClipsDic.ContainsValue(clip))
                    {
                        if (!audioClipsDic.ContainsKey(clip.name))
                        {
                            audioClipsDic.Add(clip.name, clip);
                        }
                    }
                    bgMusic.clip = clip;
                }
                bgMusic.loop = true;
                bgMusic.volume = bgValue;
                bgMusic.playOnAwake = false;
                bgMusic.Stop();
                callback?.Invoke(bgMusic);
                isPause = false;
            }
        }


        /// <summary>
        /// 从一个背景音乐过渡到另一个背景音乐
        /// </summary>
        /// <param name="_from"></param>
        /// <param name="_to"></param>
        /// <param name="_time"></param>
        public void PlayBGMMusicTransition(AudioClip _to, float _time)
        {
            if (bgMusic != null)
            {
                isTransition = false;
                MonoMgr.GetInstance().StartCoroutine(BGMMusicTransition(bgMusic, _to, _time));
            }
        }

        IEnumerator BGMMusicTransition(AudioSource _from, AudioClip _to, float _time)
        {
            bgTargetMusic.clip = _to;
            bgTargetMusic.volume = 0;
            bgTargetMusic.loop = true;
            bgTargetMusic.playOnAwake = false;
            bgTargetMusic.Play();
            isTransition = true;

            float speed = _from.volume / _time;
            yield return null;
            while (isTransition)
            {
                if (_from.volume > 0)
                {
                    _from.volume -= speed * Time.unscaledDeltaTime ;
                }
                else
                {
                    _from.volume = 0;
                }

                if (bgTargetMusic.volume < soundValue)
                {
                    bgTargetMusic.volume += speed * Time.unscaledDeltaTime;
                }
                else
                {
                    bgTargetMusic.volume = soundValue;
                    isTransition = false;
                    break;
                }
                yield return null;
            }

            AudioSource temp = bgTargetMusic;
            bgTargetMusic = _from;
            bgMusic = temp;
            bgTargetMusic.clip = null;
            temp = null;
        }



        /// <summary>
        /// 改变背景音乐大小
        /// </summary>
        /// <param name="v">背景音乐大小</param>
        public void ChangeBGValue(float v)
        {
            bgValue = v >= 0 ? v : 0;
            if (bgMusic == null)
                return;
            bgMusic.volume = bgValue;
        }

        /// <summary>
        /// 播放背景音乐
        /// </summary>
        public void PlayBGMusic()
        {
            if (bgMusic == null)
                return;
            bgMusic.Play();
        }

        /// <summary>
        /// 暂停背景音乐
        /// </summary>
        public void PauseBGMusic()
        {
            if (bgMusic == null)
                return;
            bgMusic.Pause();
            isPause = true;
        }

        /// <summary>
        /// 停止背景音乐
        /// </summary>
        public void StopBGMusic()
        {
            if (bgMusic == null)
                return;
            bgMusic.Stop();
        }

        /// <summary>
        ///  取消暂停背景音乐
        /// </summary>
        public void UnPauseBGMusic()
        {
            if (bgMusic == null)
                return;
            if (isPause)
                bgMusic.UnPause();
            else
                bgMusic.Play();
        }

        /// <summary>
        /// 通过 Resource 加载音效
        /// </summary>
        /// <param name="_clipName"></param>
        /// <param name="isLoop"></param>
        /// <param name="callback"></param>
        public void PlaySound(string _clipName, bool isLoop, UnityAction<AudioSource> callback = null)
        {
            if (soundObj == null)
            {
                soundObj = new GameObject();
                soundObj.name = "Sound";
                soundObj.transform.SetParent(musicMgrObj.transform);
            }

            
            ResMgr.GetInstance().loadAsync<AudioClip>("Music/Sound/" + _clipName, (clip) =>
            {
                if (clip == null) return;

                AudioSource source = soundObj.AddComponent<AudioSource>();

                source.clip = clip;
                source.loop = isLoop;
                source.volume = soundValue;
                source.spatialBlend = 0f;
                source.Play();

                soundList.Add(source);

                if (callback != null)
                    callback?.Invoke(source);
            });

        }

        /// <summary>
        /// 通过 Clip 加载音效
        /// </summary>
        /// <param name="_clip"></param>
        /// <param name="isLoop"></param>
        /// <param name="callback"></param>
        public void PlaySound(string _clipName, AudioClip _clip, bool isLoop = false, UnityAction<AudioSource> callback = null)
        {
            if (soundObj == null)
            {
                soundObj = new GameObject();
                soundObj.name = "Sound";
                soundObj.transform.SetParent(musicMgrObj.transform);
            }

            if (_clip == null) return;

            for (int i = soundList.Count - 1; i >= 0; --i)
            {
                if (soundList[i] != null && soundList[i].clip == _clip)
                {
                    if (soundList[i].isPlaying) return;
                }
            }

            GetAudioSource(_clipName, (source) => {

                source.clip = _clip;
                source.loop = isLoop;
                source.volume = soundValue;
                source.spatialBlend = 0f;
                source.Play();

                soundList.Add(source);

                if (callback != null)
                    callback?.Invoke(source);

            });
        }

        /// <summary>
        /// 通过 Clip 加载音效
        /// </summary>
        /// <param name="_clip"></param>
        /// <param name="isLoop"></param>
        /// <param name="callback"></param>
        public void PlaySound(AudioClip _clip, bool isLoop, UnityAction<AudioSource> callback = null)
        {
            if (soundObj == null)
            {
                soundObj = new GameObject();
                soundObj.name = "Sound";
                soundObj.transform.SetParent(musicMgrObj.transform);
            }

            if (_clip == null) return;

            for (int i = soundList.Count - 1; i >= 0; --i)
            {
                if (soundList[i] != null && soundList[i].clip == _clip)
                {
                    if (soundList[i].isPlaying) return;
                }
            }

            GetAudioSource(_clip.name, (source) => {

                source.clip = _clip;
                source.loop = isLoop;
                source.volume = soundValue;
                source.spatialBlend = 0f;
                if (callback != null)
                    callback?.Invoke(source);

                source.Play();
                soundList.Add(source);
            });
        }
     
        /// <summary>
        ///  通过 Clip 加载音效
        /// </summary>
        /// <param name="_clip"></param>
        /// <param name="isLoop"></param>
        /// <param name="isStop"></param>
        /// <param name="callback"></param>
        public void PlaySound(AudioClip _clip, bool isLoop = false, float _soundValue = 0.3f,  bool isStop = true, UnityAction<AudioSource> callback = null)
        {
            if (soundObj == null)
            {
                soundObj = new GameObject();
                soundObj.name = "Sound";
                soundObj.transform.SetParent(musicMgrObj.transform);
            }

            if (_clip == null) return;

            if (isStop)
            {
                for (int i = soundList.Count - 1; i >= 0; --i)
                {
                    if (soundList[i] != null && soundList[i].clip == _clip)
                    {
                        if (soundList[i].isPlaying) return;
                    }
                }
            }

            GetAudioSource(_clip.name, (source) => {

                source.clip = _clip;
                source.loop = isLoop;
                source.volume = soundValue;
                source.spatialBlend = 0f;
                if (callback != null)
                    callback?.Invoke(source);

                source.Play();
                soundList.Add(source);
            });
        }


        /// <summary>
        /// 暂停音效大小
        /// </summary>
        /// <param name="v">音效大小</param>
        public void ChangeSoundValue(float _value)
        {
            soundValue = _value;
            for (int i = 0; i < soundList.Count; i++)
            {
                soundList[i].volume = soundValue;
            }
        }

        /// <summary>
        /// 停止音效大小
        /// </summary>
        public void StopSound(AudioSource _source)
        {
            if (soundList.Contains(_source))
            {
                soundList.Remove(_source);
                _source.Stop();
                PushSource(_source);
            }
        }

        /// <summary>
        /// 停止音效大小
        /// </summary>
        public void StopSound(AudioClip _clip)
        {
            for (int i = soundList.Count - 1; i >= 0; --i)
            {
                if (soundList[i] != null && soundList[i].clip == _clip)
                {
                    soundList[i].Stop();
                    PushSource(soundList[i]);
                    soundList.Remove(soundList[i]);
                }
            }
        }


        /// <summary>
        /// 获得对象池中的闲置对象
        /// </summary>
        /// <param name="_name">地址</param>
        /// <returns></returns>
        private void GetAudioSource(string _name = "Sound", UnityAction<AudioSource> callback = null)
        {
            //判断缓存池中是否有缓存空间和闲置对象
            if (poolDic.ContainsKey(_name) && poolDic[_name].poolList.Count > 0)
            {
                callback?.Invoke(poolDic[_name].GetAudioSource());
            }
            else
            {
                if (soundObj == null)
                {
                    soundObj = new GameObject();
                    soundObj.name = "Sound";
                    soundObj.transform.SetParent(musicMgrObj.transform);
                }

                callback?.Invoke(soundObj.AddComponent<AudioSource>());
            }
        }


        /// <summary>
        /// 把对象放入对象池中
        /// </summary>
        /// <param name="_audioSource"></param>
        private void PushSource(AudioSource _audioSource)
        {
            if (_audioSource == null) return;

            if (soundObj == null)
            {
                soundObj = new GameObject();
                soundObj.name = "Sound";
                soundObj.transform.SetParent(musicMgrObj.transform);
            }

            _audioSource.Stop();
         
            //设置添加到对象池
            //存在缓存空间
            if (poolDic.ContainsKey(_audioSource.clip != null ? _audioSource.clip.name : "Sound"))
                poolDic[_audioSource.clip != null ? _audioSource.clip.name : "Sound"].PushSource(_audioSource);
            //不存在缓存空间
            else
                poolDic.Add(_audioSource.clip != null ? _audioSource.clip.name : "Sound", new MusicPoolData(_audioSource, soundObj));

        }
        /// <summary>
        /// 把对象放入对象池中
        /// </summary>
        /// <param name="_audioSource"></param>
        private void PushSource(AudioClip clip, AudioSource _audioSource)
        {
            if (soundObj == null)
            {
                soundObj = new GameObject();
                soundObj.name = "Sound";
                soundObj.transform.SetParent(musicMgrObj.transform);
            }

            _audioSource.Stop();

            //设置添加到对象池
            //存在缓存空间
            if (poolDic.ContainsKey(clip.name))
                poolDic[clip.name].PushSource(_audioSource);
            //不存在缓存空间
            else
                poolDic.Add(clip.name, new MusicPoolData(_audioSource, soundObj));

        }



    }

    public class MusicPoolData
    {
        //父对象
        public GameObject parentObj;
        //对象清单
        public List<AudioSource> poolList;

        public MusicPoolData(AudioSource _audioSource, GameObject _obj)
        {
            parentObj = _obj;
            poolList = new List<AudioSource>() { };
            PushSource(_audioSource);
        }

        /// <summary>
        /// 获得缓存池中的闲置对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public AudioSource GetAudioSource()
        {
            AudioSource audioS = null;
            //获得缓存池中的闲置对象
            audioS = poolList[0];
            poolList.RemoveAt(0);
            audioS.Stop();

            return audioS;
        }

        /// <summary>
        /// 把对象放入对象池中
        /// </summary>
        /// <param name="_audioSource"></param>
        public void PushSource(AudioSource _audioSource)
        {
            _audioSource.Stop();
            //设置添加到对象池
            poolList.Add(_audioSource);
        }

    }

}




/* 调用方法
    private float x, y;//背景音量

    private float sx, sy;//音效音量

    AudioSource s = null;//控制的音效

    private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 100), "播放背景音乐"))
            // MusicMgr.GetInstance().PauseBGMusic("填写在Music/BG/下的音乐名字");
            MusicMgr.GetInstance().PlayBGMusic("inunison");

        if (GUI.Button(new Rect(100, 0, 100, 100), "暂停背景音乐"))
            MusicMgr.GetInstance().PauseBGMusic();

        if (GUI.Button(new Rect(200, 0, 100, 100), "停止背景音乐"))          
            MusicMgr.GetInstance().StopBGMusic();

        x = GUI.HorizontalSlider(new Rect(300, 10, 100, 10), x, 0, 1);
        GUI.Label(new Rect(330, 10, 100, 50), "x=" + x);
        //y = GUI.VerticalSlider(new Rect(300, 30, 10, 100), y, 1, 0);
        //GUI.Label(new Rect(320, 80, 100, 50), "y=" + y);
        MusicMgr.GetInstance().ChangeBGValue(x);

        if (GUI.Button(new Rect(0, 100, 100, 100), "播放音效"))
            // MusicMgr.GetInstance().PauseBGMusic("填写在Music/sound/下的音乐名字");
            MusicMgr.GetInstance().PlaySound("BB", false,(clip)=>
            {
                s = clip;
            });

        if (GUI.Button(new Rect(0, 200, 100, 100), "停止音效"))
        {  
            MusicMgr.GetInstance().StopSound(s);
            s = null;
        }

        if (sx < x)
        {
            sx = GUI.HorizontalSlider(new Rect(0, 300, 100, 10), sx, 0, 1);
        }
        else
        {
            sx = x;
            sx = GUI.HorizontalSlider(new Rect(0, 300, 100, 10), sx, 0, 1);
        }
        GUI.Label(new Rect(10, 330, 100, 50), "x=" + sx);
        MusicMgr.GetInstance().ChangeSoundValue(sx);
    }
*/
