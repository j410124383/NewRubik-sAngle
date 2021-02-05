using UnityEngine;
using YProjectBase;

[RequireComponent(typeof(LineRenderer))]
[DisallowMultipleComponent]
public class MainMusicUI : MonoBehaviour,IInit,IListener,IGameStart
{

    [SerializeField] private bool isAwakeStart = false;
    //[SerializeField] private string bgmName = "BGM";
    [Range(0.0005f, 0.5f)] [SerializeField] private float delay = 0.0166f;
    [SerializeField] private float multiplyerLenth = 107.0f;
    [SerializeField] private float multiplyerHeight = 107.0f;

    private AudioSource bgMusic;
    private LineRenderer linerender;

    private float[] spectrum;
    private float[] spactrumDataDelay;
    private int numSamples = 1024;
    private readonly int LINERENDER_POINT_CNT = 68;


    public float Delay { get { return delay; } set { delay = Mathf.Clamp(value, 0.0005f, 0.5f); } }
    public float MultiplyerLenth {get{ return multiplyerLenth; } set { multiplyerLenth = value >= 0 ? value : 0; } }
    public float MultiplyerHeight { get { return multiplyerHeight; } set { multiplyerHeight = value >= 0 ? value : 0; } }

    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        if (isAwakeStart)
        {
            RemoveListener();
            AddListener();
        }
    }

    private void OnDisable()
    {
        RemoveListener();
    }

    private void OnDestroy()
    {
        RemoveListener();
    }


    public void Init()
    {
        RemoveListener();
        //if (MusicMgr.GetInstance().GetBGMusic == null)
        //{
        //    MusicMgr.GetInstance().LoadBGMusic(bgmName);
        //}
        bgMusic = MusicMgr.GetInstance().GetBGMusic;
       // MusicMgr.GetInstance().StopBGMusic();

        spectrum = new float[numSamples];
        spactrumDataDelay = new float[numSamples];

        if (linerender == null)
        {
            linerender = GetComponent<LineRenderer>();
            linerender.positionCount = LINERENDER_POINT_CNT;
            linerender.startWidth = 0.02f;
            linerender.endWidth = 0.02f;
        }
    }

    public void UpdatedMusicUI()
    {
        if (bgMusic == null)
            bgMusic = MusicMgr.GetInstance().GetBGMusic;

        if (bgMusic == null || linerender == null) return;
      
        //音乐采样
        bgMusic.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);


        int j = 1;
        while (j < numSamples + 1)
        {
            float newData = (spectrum[j - 1] * 1.0f * 1);
            //对更新数据进行延迟处理
            if (newData > spactrumDataDelay[j - 1])
            {
                spactrumDataDelay[j - 1] += (delay * Time.deltaTime);
                if (spactrumDataDelay[j - 1] > newData)
                {
                    spactrumDataDelay[j - 1] = newData;
                }
            }
            else
            {
                spactrumDataDelay[j - 1] -= (delay * Time.deltaTime);
                if (spactrumDataDelay[j - 1] < 0f)
                {
                    spactrumDataDelay[j - 1] = 0f;
                }
            }
            j++;
        }


        for (int i = 0, cnt = LINERENDER_POINT_CNT; i < cnt; ++i)
        {
            var v = spactrumDataDelay[i];
            var tempX = ((i - LINERENDER_POINT_CNT / 2) * 0.2f) * multiplyerLenth;

            tempX -= ((0) - LINERENDER_POINT_CNT / 2) * 0.2f * multiplyerLenth;

            linerender.SetPosition(i, new Vector3(tempX, i==0 ? 0 : v * 20 * multiplyerHeight * 10, 0) );
        }

    }


    public void GameStart()
    {
      
        AddListener();
        //if (MusicMgr.GetInstance().GetBGMusic == null || (MusicMgr.GetInstance().GetBGMusic != null && MusicMgr.GetInstance().BGMusicIsPlaing == false))
        //{
        //    MusicMgr.GetInstance().LoadBGMusic(bgmName);
        //}
    }


    public void AddListener()
    {
        MonoMgr.GetInstance().AddUpdateListener(UpdatedMusicUI);
    }

    public void RemoveListener()
    {
        MonoMgr.GetInstance().RemoveUpdateListener(UpdatedMusicUI);
    }

}
