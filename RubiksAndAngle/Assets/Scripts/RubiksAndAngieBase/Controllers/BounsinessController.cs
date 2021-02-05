using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YProjectBase;

[RequireComponent(typeof(Rigidbody))]
[DisallowMultipleComponent]
public class BounsinessController : MonoBehaviour,IInit,IListener
{

    [SerializeField] [Range(0,50)] float speed = 2;
    [SerializeField] [Range(0, 1)] float smoothTime = 1;
    [SerializeField] bool isIgnoreTimeScale = false;

    Rigidbody rig;

    float currentVectoryX;
    float currentVectoryY;
    float currentVectoryZ;


    public float Speed { get { return this.speed; } set { this.speed = value; } }
    public float SmoothTime { get { return this.smoothTime; } set { this.smoothTime = value; } }
    public bool IsIgnoreTimeScale { get { return this.isIgnoreTimeScale; } set { this.isIgnoreTimeScale = value; } }

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        rig = GetComponent<Rigidbody>();
        RemoveListener();
    }

    private void OnEnable()
    {
        AddListener();
    }

    private void OnDisable()
    {
        RemoveListener();
    }

    private void OnDestroy()
    {
        RemoveListener();
    }



    public void ControlRigBounsiness()
    {
        if (rig == null) return;

        if (rig.velocity.sqrMagnitude > speed)
        {
            rig.velocity = new Vector3(Mathf.SmoothDamp(rig.velocity.x, 0 , ref currentVectoryX, smoothTime , speed, isIgnoreTimeScale == false ? Time.fixedDeltaTime : Time.fixedUnscaledDeltaTime),
                                                        Mathf.SmoothDamp(rig.velocity.y, 0, ref currentVectoryY, smoothTime, speed, isIgnoreTimeScale == false ? Time.fixedDeltaTime : Time.fixedUnscaledDeltaTime),
                                                        Mathf.SmoothDamp(rig.velocity.z, 0, ref currentVectoryZ, smoothTime, speed, isIgnoreTimeScale == false ? Time.fixedDeltaTime : Time.fixedUnscaledDeltaTime));
        }      
    }

    public void AddListener()
    {
        MonoMgr.GetInstance().AddFixedUpdateListener(ControlRigBounsiness);
    }

    public void RemoveListener()
    {
        MonoMgr.GetInstance().RemoveFixedUpdateListener(ControlRigBounsiness);
    }

}
