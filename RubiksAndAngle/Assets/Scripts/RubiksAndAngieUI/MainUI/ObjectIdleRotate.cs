using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YProjectBase;

public class ObjectIdleRotate : MonoBehaviour,IInit,IListener,IGameStart,IActive
{

    [Tooltip("是否 Awake 时启动")] [SerializeField] private bool isAwakeStart;

    [Tooltip("目标对象")] [SerializeField] private Transform targetObj;
    [Tooltip("计时延迟时间")] [SerializeField] private float duration = 3f;
    [Tooltip("单位旋转速度")] [SerializeField] private float rotateSpeed = 1f;
    [Tooltip("单位旋转方向")] [SerializeField] private Vector3 rotateAix;
    [Tooltip("是否忽略 Time.Scale")] [SerializeField] private bool isIgnoreTime;
    [Tooltip("是否在世界空间")] [SerializeField] private bool isWorld = true;

    private bool isActive;
    private bool isRotating;

    private TimerBase timer = new TimerBase();


    public Transform TargetObj { get { return this.targetObj; } set { this.targetObj = value; } }
    public float Duration { get { return this.duration; } set { this.duration = value >= 0 ? value : this.duration; } }
    public float RotateSpeed { get { return this.rotateSpeed; } set { this.rotateSpeed = value >= 0 ? value : this.rotateSpeed; } }
    public Vector3 RotateAix { get { return this.rotateAix; } set { this.rotateAix = value.normalized; } }
    public bool GetIsRotating { get { return this.isRotating; } }
    public bool GetIsIgnoreTime { get { return this.isIgnoreTime; } set { this.isIgnoreTime = value; } }
    public bool GetIsWorld { get { return this.isWorld; } set { this.isWorld = value; } }

    private void Awake()
    {
        Init();
    }


    private void OnEnable()
    {
        if (!isAwakeStart) return;
        GameStart();
    }


    private void OnDisable()
    {
        RemoveListener();
    }

    private void OnDestroy()
    {
        RemoveListener();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        rotateAix = rotateAix.normalized;
        this.duration = this.duration >= 0 ? this.duration : 0;
    }
#endif


    public void Init()
    {
        if(timer != null)
            timer.Duration = duration;
        else
            timer = new TimerBase(duration);

        if (timer != null)
            timer.Stop();

        rotateAix = rotateAix.normalized;
        this.duration = this.duration >= 0 ? this.duration : 0;

        isRotating = false;
    }



    public void BackStartTimer()
    {
        if (timer == null) return;
        isRotating = false;
        timer.Go();
    }

    public void ToActive()
    {
        if (!isActive) return;

        if (timer == null || targetObj == null) return;

        if (InputController.GetInstance().IsInputing)
        {
            //Debug.Log("BackStartTimer");
            BackStartTimer();
            return;
        }

        //Debug.Log("ToTimer");

        ToTimer(timer, out isRotating, isIgnoreTime ? Time.unscaledDeltaTime : Time.deltaTime);

        if (isRotating)
        {
            //Debug.Log("ToRotate");
            ToRotate(targetObj, rotateAix.normalized * rotateSpeed, isIgnoreTime, isWorld);
        }
    }

    private void ToTimer(TimerBase _timer, out bool _isRotating, float _deltaTime)
    {
        _timer.Tick(_deltaTime);

        if (_timer.State == TimerBase.STATE.FINISHED)
        {
            _isRotating = true;
        }
        else _isRotating = false;
    }

    private void ToRotate(Transform _targetObj, Vector3 _rotateAix, bool _isIgnoreTime = false, bool _isWorld = true)
    {
        if (_targetObj != null)
            ToRotate(_targetObj, _rotateAix, _isIgnoreTime ? Time.unscaledDeltaTime : Time.deltaTime, _isWorld ? Space.World : Space.Self);
    }

    private void ToRotate(Transform _targetObj, Vector3 _rotateSpeed, float _deltaTime,Space _space = Space.World)
    {
        if(_targetObj != null)
        {
            _targetObj.Rotate(_rotateSpeed * _deltaTime, _space);
        }
    }




    public void AddListener()
    {
        MonoMgr.GetInstance().AddLateUpdateListener(ToActive);
    }

    public void RemoveListener()
    {
        MonoMgr.GetInstance().RemoveLateUpdateListener(ToActive);
    }

    public void GameStart()
    {
        RemoveListener();
        AddListener();
        BackStartTimer();
        Active();
    }


    public void Active()
    {
        isActive = true;
    }

    public void UnActive()
    {
        isActive = false;
    }

}
