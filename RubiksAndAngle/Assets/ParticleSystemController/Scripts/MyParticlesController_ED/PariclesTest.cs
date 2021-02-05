using MyParticlesController_ED;

public class PariclesTest : PariclesBase
{

    int tempTriggerNum;         //储存触发次数
    bool canPlay;                     //在 Update() 中是否可以触发 UpdateEvent();
    DelayAction delayAction; //UpdateEvent() 中委托的方法;



    private void Update()
    {
        UpdateEvent(ref canPlay, ref triggerNum, delayAction);
    }


    #region base方法

    /// <summary>
    /// 重写初始化
    /// </summary>
    protected override void Init()
    {
        base.Init();
        tempTriggerNum = triggerNum + 1;
        InitAction();
    }

    /// <summary>
    /// 初始化 delayAction 方法
    /// </summary>
    void InitAction()
    {
        //方法存储
        delayAction = ReallyPlay;
    }

    /// <summary>
    /// 播放触发事件
    /// </summary>
    public void ReallyPlay()
    {
        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);

            if (partials != null && partials.Length <= 0) return;

            for (int i = 0; i < partials.Length; i++)
            {
                if (partials[i].particleSystem == null) continue;

                if (partials[i].particleSystem.gameObject.activeSelf == false)
                {
                    partials[i].particleSystem.gameObject.SetActive(true);
                }

                partials[i].particleSystem.Stop();
                partials[i].particleSystem.Play();
            }

        }
        else
        {
            if (partials != null && partials.Length <= 0) return;

            for (int i = 0; i < partials.Length; i++)
            {
                if (partials[i].particleSystem == null) continue;

                if (partials[i].particleSystem.gameObject.activeSelf == false)
                {
                    partials[i].particleSystem.gameObject.SetActive(true);
                }

                partials[i].particleSystem.Stop();
                partials[i].particleSystem.Play();
            }
        }
    }

    #endregion


    #region 具体实现接口

    public override void None()
    {
        if (partials == null || partials.Length <= 0) return;

        for (int i = 0; i < partials.Length; i++)
        {
            if (partials[i] == null || partials[i].particleSystem == null) continue;
            partials[i].particleSystem.Stop();
        }

        triggerNum = tempTriggerNum;
        ResetDelayTempTime();
        canPlay = false;


        if (gameObject.activeSelf == true)
        {
            gameObject.SetActive(false);
        }

    }

    public override void Play()
    {
        if (partials == null || partials.Length <= 0) return;

        for (int i = 0; i < partials.Length; i++)
        {
            if (partials[i] == null || partials[i].particleSystem == null) continue;
            partials[i].particleSystem.Stop();
        }

        triggerNum = tempTriggerNum;
#if UNITY_EDITOR
        UnityEngine.Debug.Log(triggerNum);
#endif
        ResetDelayTempTime();
        canPlay = true;
        isDelay = true;

        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }

    }

    public override void Stop()
    {
        if (partials == null || partials.Length <= 0) return;

        for (int i = 0; i < partials.Length; i++)
        {
            if (partials[i] == null || partials[i].particleSystem == null) continue;
            partials[i].particleSystem.Stop();
        }

        triggerNum = tempTriggerNum;
        ResetDelayTempTime();
        canPlay = false;

        if (gameObject.activeSelf == true)
        {
            gameObject.SetActive(false);
        }

    }

    #endregion

}
