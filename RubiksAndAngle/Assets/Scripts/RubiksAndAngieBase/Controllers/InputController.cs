using UnityEngine;
using YProjectBase;

[DisallowMultipleComponent]
public class InputController : SingletonMonoBehaviour<InputController>, IInit, IGameStart, IListener
{

    [Tooltip("是否Awake后直接开启")] [SerializeField] private bool isAwakeStart;


#if UNITY_EDITOR

    [SerializeField] private bool isPCInput = true;
    [SerializeField] private bool isPhoneInput = true;

#endif


    private float mouseX = 0f;
    private float mouseY = 0f;
                
    private float horizontal = 0f;
    private float vertical = 0f;
                
    private bool isGameStart;

    private bool isMouse;
    private bool isMouseDown;
    private bool isMouseTwoDown;
    private bool isMouseRotate;
    private Vector2 screenPos;
    private Vector2 firstScreenPos;
    private Vector2 mouseScrollDelta;

    private bool isTouch;
    private bool isTwoTouch;
    private bool isTouchDown;
    private bool isTouchTwoDown;
    private Vector2 deltaPos;
    private Touch oldTouch1;
    private Touch oldTouch2;
    private float touchAngle = 0f;
    [SerializeField] private float inputDamping = 1.5f;


    private Camera cam;
    private Vector3 fwd;
    private GameObject targetObj;
    private Rect camRect;

    private bool isInputing;

    private System.DateTime nTempTime;
    private System.DateTime oTempTime;

    private readonly System.TimeSpan span = new System.TimeSpan(0, 0, 0, 0, 200);

    /// <summary>
    /// 控制Awake后直接开启
    /// </summary>
    public bool IsAwakeStart
    {
        get
        {
            return isAwakeStart;
        }

        set
        {
            isAwakeStart = value;
        }
    }

    /// <summary>
    /// 获得鼠标 X轴 值
    /// </summary>
    public float GetMouseX
    {
        get
        {
            return mouseX;
        }
    }
    /// <summary>
    ///  获得鼠标 Y轴 值
    /// </summary>
    public float GetMouseY
    {
        get
        {
            return mouseY;
        }
    }
    /// <summary>
    ///  获得 Horizontal 输入值
    /// </summary>
    public float GetHorizontal
    {
        get
        {
            return horizontal;
        }
    }
    /// <summary>
    /// 获得 Vertical 输入值
    /// </summary>
    public float GetVertical
    {
        get
        {
            return vertical;
        }
    }
    /// <summary>
    /// 获得鼠标滚轮增量
    /// </summary>
    public Vector2 GetMouseScrollDelta
    {
        get
        {
            return mouseScrollDelta;
        }
    }

    /// <summary>
    /// 鼠标左键是否按下
    /// </summary>
    public bool IsMouse
    {
        get
        {
            return isMouse;
        }
    }

    /// <summary>
    /// 获得点击对象
    /// </summary>
    public GameObject TargetObj
    {
        get
        {
            return targetObj;
        }
    }
    /// <summary>
    /// 获得主相机
    /// </summary>
    public Camera GetMainCam
    {
        get
        {
            if (cam == null || cam != Camera.main)
                cam = Camera.main;
            return cam;
        }
    }
    /// <summary>
    /// 主相机Forward
    /// </summary>
    public Vector3 FwdCam
    {
        get
        {
            if (cam == null || cam != Camera.main)
            {
                cam = Camera.main;
                fwd = cam.transform.forward;
            }
            else
            {
                fwd = cam.transform.forward;
            }

            return fwd;
        }
    }

    /// <summary>
    /// 单个手指触摸并且移动
    /// </summary>
    public bool IsTouch
    {
        get
        {
            return isTouch;
        }
    }
    /// <summary>
    /// 两个手指触摸并且移动
    /// </summary>
    public bool IsTwoTouch
    {
        get
        {
            return isTwoTouch;
        }
    }
    /// <summary>
    /// 单个手指移动值
    /// </summary>
    public Vector2 DeltaPos{get { return deltaPos; } }
    /// <summary>
    /// 两个手指触摸并且移动角度
    /// </summary>
    public float TouchAngle { get  {return touchAngle; } }
    /// <summary>
    /// 输入阻尼
    /// </summary>
    public float InputDamping { get { return inputDamping >= 0 ? inputDamping : -inputDamping; } set { inputDamping = value >= 0 ? value : -value; } }


    /// <summary>
    /// 是否有输入
    /// </summary>
    public bool IsInputing { get { return isInputing; } }
    /// <summary>
    /// 是否双击
    /// </summary>
    public bool IsInputTwoDown { get { return isMouseTwoDown || isTouchTwoDown; } }


    /// <summary>
    /// 获得屏幕点住坐标(0,1)
    /// </summary>
    public Vector2 GetScreenPoint { get { return new Vector2(screenPos.x/Screen.width,screenPos.y/Screen.height); } }

    /// <summary>
    /// 获得屏幕点击坐标(0,1)
    /// </summary>
    public Vector2 GetFirstScreenPoint { get { return new Vector2(firstScreenPos.x / Screen.width, firstScreenPos.y / Screen.height); } }

    public Rect CamRect { get { return camRect; } set { camRect = value; } }


    private void Awake()
    {
        Init();       
    }

    private void Start()
    {
        if (isAwakeStart)
        {
            GameStart();
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


    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        mouseX = 0f;
        mouseY = 0f;

        horizontal = 0f;
        vertical = 0f;

        isMouseDown = false;
        screenPos  = Vector2.zero;

        isTouch = false;
        isTwoTouch = false;
        deltaPos = Vector2.zero;
        touchAngle = 0f;

        targetObj = null;


        if (cam == null || cam != Camera.main)
        {
            cam = Camera.main;
            fwd = cam.transform.forward;
        }
        isGameStart = false;
        RemoveListener();
    }


#if UNITY_STANDALONE || UNITY_EDITOR

    public void ToPCInput()
    {
#if UNITY_EDITOR
        if (isPCInput)
        {
#endif
            PCInput();
            GetMouseTarget();

#if UNITY_EDITOR
        }
#endif

    }

    /// <summary>
    /// PC端输入
    /// </summary>
    public void PCInput()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = -Input.GetAxis("Mouse Y");

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButtonDown(0))
        {
            firstScreenPos = Input.mousePosition;
            screenPos = Input.mousePosition;
            isMouseDown = true;
#if UNITY_EDITOR
            UnityEngine.Debug.Log("PCInput");
#endif
        }

        if (Input.GetMouseButton(0))
        {
            isMouse = true;    
            //Debug.Log("GetMouseButton");
        }
        else
        {
            isMouse = false;
        }

        if (Input.GetKey("left shift"))
        {
            if (Input.GetMouseButton(0))
            {
                mouseScrollDelta = Vector2.up;
            }
            else if (Input.GetMouseButton(1))
            {
                mouseScrollDelta = Vector2.down;
            }
            else
            {
                mouseScrollDelta = Vector2.zero;
            }
        }

    }

    /// <summary>
    /// PC获得点击目标
    /// </summary>
    private void GetMouseTarget()
    {
        if (isMouseDown)
        {
            isMouseDown = false;
            nTempTime = System.DateTime.Now;

            if (cam == null || cam != Camera.main)
            {
                cam = Camera.main;
                fwd = cam.transform.forward;
            }

            Ray ray = cam.ScreenPointToRay(screenPos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                targetObj = hit.collider.gameObject;    //获得选中物体
                EventCenter.GetInstance().EventTrigger<GameObject>("OnClickAnim", targetObj);
                EventCenter.GetInstance().EventTrigger<GameObject>("OnClickBody", targetObj);

            }
            else
            {
                targetObj = null;
            }

            if (nTempTime - oTempTime < span)
            {
#if UNITY_EDITOR
                UnityEngine.Debug.Log("isMouseDown");
#endif
                isMouseTwoDown = true;
                EventCenter.GetInstance().EventTrigger<GameObject>("TwoOnClickBody", targetObj);
            }
            else
            {
                isMouseTwoDown = false;
            }
            oTempTime = nTempTime;

        }
    }

#endif


#if UNITY_IOS || UNITY_ANDROID || UNITY_EDITOR

    public void ToTouch()
    {
#if UNITY_EDITOR
        if (isPhoneInput)
        {
#endif
            TouchInput();
            TouchTwoInput();
            GetTouchTarget();
#if UNITY_EDITOR
        }
#endif
    }

    /// <summary>
    /// 一只手指输入
    /// </summary>
    public void TouchInput()
    {
        if (Input.touchCount <= 0)
        {
            isTouch = false;
            return;
        }

        if(1 == Input.touchCount && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);
            firstScreenPos = touch.position;
            isTouchDown = true;
        }

        //单点触摸滑动， 水平上下旋转
        if (1 == Input.touchCount && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Touch touch = Input.GetTouch(0);
            screenPos = touch.position;
            deltaPos = touch.deltaPosition;
            inputDamping = inputDamping >= 0 ? inputDamping : - inputDamping;
            deltaPos = new Vector2((Mathf.Abs(deltaPos.x) < inputDamping ? 0 : deltaPos.x), (Mathf.Abs(deltaPos.y) < inputDamping ? 0 : deltaPos.y) ) * 0.5f;
            //Debug.Log(deltaPos);
            isTouch = true;
            return;
        }
    }

    /// <summary>
    /// 两只手指输入
    /// </summary>
    public void TouchTwoInput()
    {
        if (Input.touchCount <= 1)
        {
            isTwoTouch = false;
            return;
        }

        Touch touch1 = Input.GetTouch(0);
        Touch touch2 = Input.GetTouch(1);

        if (touch2.phase == TouchPhase.Began)//0=>1 1=>2
        {
            oldTouch1 = touch1;
            oldTouch2 = touch2;
            isTouch = true;
            return;
        }

        if ((touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved))
        {
            Vector2 curVec = touch2.position - touch1.position;
            Vector2 oldVec = oldTouch2.position - oldTouch1.position;
            float angle = Vector2.Angle(oldVec, curVec);
            angle *= Mathf.Sign(Vector3.Cross(oldVec, curVec).z);
            touchAngle = angle;
            isTouch = true;
            isTwoTouch = true;
            oldTouch1 = touch1;
            oldTouch2 = touch2;
        }
    }

    /// <summary>
    /// 一只手指输入
    /// </summary>
    private void GetTouchTarget()
    {
        if (Input.touchCount <= 0 || Input.touchCount > 1)
            return;

        if (cam == null || cam != Camera.main)
        {
            cam = Camera.main;
            fwd = cam.transform.forward;
        }

        if (1 == Input.touchCount && Input.GetTouch(0).phase != TouchPhase.Moved)
        {
#if UNITY_EDITOR
            UnityEngine.Debug.Log("TouchTarget");
#endif

            Touch touch = Input.GetTouch(0);
        

            Vector2 pos = touch.position;
            Ray ray = cam.ScreenPointToRay(pos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                targetObj = hit.collider.gameObject;    //获得选中物体
                EventCenter.GetInstance().EventTrigger<GameObject>("OnClickAnim", targetObj);
                EventCenter.GetInstance().EventTrigger<GameObject>("OnClickBody", targetObj);
     
            }
            else
            {
                targetObj = null;           
            }

            if (isTouchDown)
            {
                isTouchDown = false;

                nTempTime = System.DateTime.Now;

                if (nTempTime - oTempTime < span)
                {
#if UNITY_EDITOR
                    UnityEngine.Debug.Log("isTouchDown");
#endif
                    isTouchTwoDown = true;
                    EventCenter.GetInstance().EventTrigger<GameObject>("TwoOnClickBody", targetObj);
                }
                else
                {
                    isTouchTwoDown = false;
                }

                oTempTime = nTempTime;
            }

        }
    }

    /// <summary>
    /// 手机震动
    /// </summary>
    public void IphoneShake()
    {
        Handheld.Vibrate();
    }

    //public void IphoneShake(float time)
    //{
    //    System.Activities.Activity curActivity = UnityPlayer.currentActivity;
    //    m_vibrator = (AndroidInput.os.Vibrator)curActivity.getSystemService(Service.VIBRATOR_SERVICE);
    //    m_vibrator.vibrate(new long[] { 100, 10, 100, 1000 }, -1);
    //}

#endif


    public void UpdatedInput()
    {
        isInputing = isTouch || isTwoTouch || isMouse || isMouseDown;
    }


    /// <summary>
    /// 游戏开始事件
    /// </summary>
    public void GameStart()
    {
        RemoveListener();
        isGameStart = true;
        AddListener();       
    }

    /// <summary>
    /// 添加监听
    /// </summary>
    public void AddListener()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        MonoMgr.GetInstance().AddUpdateListener(ToPCInput);
#endif

#if UNITY_IOS || UNITY_ANDROID || UNITY_EDITOR
        MonoMgr.GetInstance().AddUpdateListener(ToTouch);
#endif

        MonoMgr.GetInstance().AddLateUpdateListener(UpdatedInput);
    }
    /// <summary>
    /// 移除监听
    /// </summary>
    public void RemoveListener()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        MonoMgr.GetInstance().RemoveUpdateListener(ToPCInput);
#endif

#if UNITY_IOS || UNITY_ANDROID || UNITY_EDITOR
        MonoMgr.GetInstance().RemoveUpdateListener(ToTouch);
#endif

        MonoMgr.GetInstance().AddLateUpdateListener(UpdatedInput);
    }

}
