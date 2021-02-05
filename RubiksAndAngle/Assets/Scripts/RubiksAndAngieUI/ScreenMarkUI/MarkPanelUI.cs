using UnityEngine;
using UnityEngine.UI;
using YProjectBase;


public class MarkPanelUI : BasePanel,IListener
{

    [SerializeField] Material markMaterial;
    [SerializeField] Vector2 center = new Vector2(0.5f, 0.5f);
    [SerializeField] float speed = 1;
    [Range(0,1)] [SerializeField] float raidus = 1;
    [Range(0, 1)] [SerializeField] float targetRaidus = 0;
    [SerializeField] AnimationCurve maskShowAnim = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
    [SerializeField] AnimationCurve maskHideAnim = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
    [SerializeField] bool isUnscaleTime = false;

    [Space(20)]  [SerializeField] IGameEvent markEndEvent = null;

    [HideInInspector] [SerializeField] private Vector4 cicle;

    [HideInInspector] [SerializeField] private float tempTime = 0;

    [HideInInspector] [SerializeField] private float oriRaidus = 1;
    [HideInInspector] [SerializeField] private int dir = 1;

    [HideInInspector] [SerializeField] bool isForward = true;

    public float Speed { set { speed = Mathf.Clamp(value, 1, 5); } get { return speed; } }
    public float Raidus { set { raidus = Mathf.Clamp(value, 0,1); } get { return raidus; } }
    public float TargetRaidus { set { targetRaidus = Mathf.Clamp(value, 0, 1); } get { return targetRaidus; } }
    public Vector2 Center { set { center = new Vector2(Mathf.Clamp(value.x, 0, 1), Mathf.Clamp(value.y, 0, 1)); } get { return center; } }

#if UNITY_EDITOR

    [HideInInspector] [SerializeField] bool isPlay;
    [SerializeField] bool isTest;
    [HideInInspector] [SerializeField] bool istemp;

    [HideInInspector] [SerializeField] float tempR;
    [HideInInspector] [SerializeField] float tempTR;

    private void OnValidate()
    {
        if (Application.isEditor)
        {
            if (markMaterial != null)
            {
                speed = Mathf.Clamp(speed, 1, 5);
                center = new Vector2(Mathf.Clamp(center.x, 0, 1), Mathf.Clamp(center.y, 0, 1));
                cicle = new Vector4(center.x, center.y, raidus);
                markMaterial.SetVector("_Circle", cicle);
            }

            if (markMaterial == null)
                markMaterial = GetComponent<Image>().material;
        }
    }

    private void OnEnable()
    {
        if (Application.isEditor)
        {
            oriRaidus = raidus;
            isTest = false;
            istemp = false;
        }
    }


    private void Update()
    {
        if(istemp != isTest && isTest)
        {
            tempR = raidus;
            tempTR = targetRaidus;
            cicle.z = raidus;
            if (markMaterial != null && markMaterial.HasProperty("_Circle"))
                markMaterial.SetVector("_Circle", cicle);
            istemp = isTest;
        }
        else if(istemp != isTest && !isTest)
        {
            raidus = tempR;
            targetRaidus = tempTR;
            cicle.z = raidus;
            if (markMaterial != null && markMaterial.HasProperty("_Circle"))
                markMaterial.SetVector("_Circle", cicle);
            istemp = isTest;
        }

        if (isTest)
        {
            TestMaskAnim();
        }
    }




    [ContextMenu("TestForwardPlay")]
    public void TestForwardPlay()
    {
        if (!isTest) return;

        TestStop();

        if (!isPlay)
        {
            raidus = 0;
            targetRaidus = 1;
            cicle.z = raidus;
            dir = (raidus - targetRaidus) >= 0 ? 1 : -1;
            if (markMaterial.HasProperty("_Circle"))
                 markMaterial.SetVector("_Circle", cicle);
            isForward = true;
            tempTime = 0;
            isPlay = true;
        }
    }

    [ContextMenu("TestBackPlay")]
    public void TestBackPlay()
    {
        if (!isTest) return;

        TestStop();

        if (!isPlay)
        {
            raidus = 1;
            targetRaidus = 0;
            cicle.z = raidus;
            // dir = raidus - targetRaidus;
            dir = (raidus - targetRaidus) >= 0 ? 1 : -1;
            if (markMaterial.HasProperty("_Circle"))
                markMaterial.SetVector("_Circle", cicle);
            isForward = false;
            tempTime = 0;
            isPlay = true;
        }
    }

    [ContextMenu("TestStopMsakAnim")]
    public void TestStop()
    {  
        if (!isTest) return;

        isPlay = false;
        raidus = isForward ? 0 : 1;
        cicle.z = raidus;
        tempTime = 0;
        markMaterial.SetVector("_Circle", cicle);     
    }


    public void TestMaskAnim()
    {
        if (isPlay)
        {
            TestMarkAnim(targetRaidus);

            if (Mathf.Abs(targetRaidus - raidus) <= 0.01f)
            {
                isPlay = false;
            }
        }

    }

    private void TestMarkAnim(float targetRaidus = 0)
    {
        if (markMaterial != null)
        {
            if (Mathf.Abs(targetRaidus - raidus) > 0.05f)
            {
                float laststepInt = (isForward) ? maskShowAnim.Evaluate(tempTime) : maskHideAnim.Evaluate(tempTime) ;
                tempTime += (isUnscaleTime ? Time.unscaledDeltaTime : Time.deltaTime) * speed / 2;

                float animTime = (isForward) ? maskShowAnim.keys[maskShowAnim.length - 1].time : maskHideAnim.keys[maskHideAnim.length - 1].time;

                tempTime =(animTime <= tempTime ) ? animTime : tempTime;

                float stepInt = (isForward) ? maskShowAnim.Evaluate(tempTime) : maskHideAnim.Evaluate(tempTime);

                raidus = Mathf.Max(0, raidus - dir * Mathf.Abs(stepInt - laststepInt));

                cicle.z = raidus;
                if (markMaterial.HasProperty("_Circle"))
                    markMaterial.SetVector("_Circle", cicle);

            }
            else
            {
                raidus = targetRaidus;
                cicle.z = targetRaidus;
                if (markMaterial.HasProperty("_Circle"))
                    markMaterial.SetVector("_Circle", cicle);
            }
        }
    }

#endif


    private void OnDestroy()
    {
        RemoveListener();
    }



    public override void ShowUI()
    {
        //RemoveListener();
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        oriRaidus = raidus;
        dir = (raidus - targetRaidus) >= 0 ? 1 : -1;

        isForward = dir == 1 ? false : true;

        if (isForward)
        {
            if (GameDataController.GetInstance() != null && GameDataController.GetInstance().musicData != null)
                MusicMgr.GetInstance().PlaySound(GameDataController.GetInstance().musicData.GetSEClip(1), false, 0.3f, true);
        }
        else
        {
            if (GameDataController.GetInstance() != null && GameDataController.GetInstance().musicData != null)
                MusicMgr.GetInstance().PlaySound(GameDataController.GetInstance().musicData.GetSEClip(2), false, 0.3f, true);
        }

        tempTime = 0;

        cicle = new Vector4(center.x, center.y, raidus);
        AddListener();
    }

    public void ShowUI(Vector2 _center,float _raidus, float _targetRaidus)
    {
        center = new Vector2(Mathf.Clamp(_center.x, 0, 1), Mathf.Clamp(_center.y, 0, 1));
        raidus = Mathf.Clamp(_raidus, 0, 1);
        targetRaidus = Mathf.Clamp(_targetRaidus, 0, 1);
        ShowUI();
    }



    private void MarkAnim()
    {
        MarkAnim(targetRaidus);
    }

    private void MarkAnim(float targetRaidus = 0)
    {
        if(markMaterial != null)
        {
            if (Mathf.Abs(targetRaidus - raidus) > 0.05f)
            {
                float laststepInt = (isForward) ? maskShowAnim.Evaluate(tempTime) : maskHideAnim.Evaluate(tempTime);
                tempTime += (isUnscaleTime ? Time.unscaledDeltaTime : Time.deltaTime) * speed / 2;

                float animTime = (isForward) ? maskShowAnim.keys[maskShowAnim.length - 1].time : maskHideAnim.keys[maskHideAnim.length - 1].time;

                tempTime = (animTime <= tempTime) ? animTime : tempTime;

                float stepInt = (isForward) ? maskShowAnim.Evaluate(tempTime) : maskHideAnim.Evaluate(tempTime);

                raidus = Mathf.Max(0, raidus - dir * Mathf.Abs(stepInt - laststepInt));

                cicle.z = raidus;
                if (markMaterial.HasProperty("_Circle"))
                    markMaterial.SetVector("_Circle", cicle);

            }
            else
            {
                if (markEndEvent != null)
                {
                    markEndEvent?.Invoke(0);
                }
                raidus = targetRaidus;
                cicle.z = targetRaidus;
                if (markMaterial.HasProperty("_Circle"))
                    markMaterial.SetVector("_Circle", cicle);
                RemoveListener();
            }


            //if (Mathf.Abs(targetRaidus - raidus) > 0.05f)
            //{
            //    if (Mathf.Abs(targetRaidus - raidus) > 0.2f)
            //    {
            //        raidus = Mathf.Lerp(raidus, targetRaidus, (isUnscaleTime ? Time.unscaledDeltaTime : Time.deltaTime) * speed);
            //        cicle.z = raidus;
            //        markMaterial.SetVector("_Circle", cicle);
            //    }
            //    else if (Mathf.Abs(targetRaidus - raidus) <= 0.2f && Mathf.Abs(targetRaidus - raidus) <= 0.1f)
            //    {
            //        raidus = Mathf.Lerp(raidus, targetRaidus, (isUnscaleTime ? Time.unscaledDeltaTime : Time.deltaTime) * speed);                
            //        markMaterial.SetVector("_Circle", cicle);
            //    }
            //    else 
            //    {
            //        raidus = Mathf.Lerp(cicle.z > raidus ? cicle.z : raidus, targetRaidus, (isUnscaleTime ? Time.unscaledDeltaTime : Time.deltaTime) * speed);
            //        cicle.z = raidus;
            //        markMaterial.SetVector("_Circle", cicle);
            //    }
            //}
            //else
            //{

            //    if (markEndEvent != null)
            //    {
            //        markEndEvent?.Invoke(0);
            //    }

            //    cicle.z = targetRaidus;
            //    markMaterial.SetVector("_Circle", cicle);
            //    RemoveListener();
            //}


        }
    }



    public void AddListener()
    {
        MonoMgr.GetInstance().AddUpdateListener(MarkAnim);
    }

    public void RemoveListener()
    {
        MonoMgr.GetInstance().RemoveUpdateListener(MarkAnim);
    }

}
