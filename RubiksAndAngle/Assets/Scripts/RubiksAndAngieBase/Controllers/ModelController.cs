using UnityEngine;
using YProjectBase;

namespace RubiksAndAngie
{

    [DisallowMultipleComponent]
    public class ModelController : MonoBehaviour, IInit, IGameReset ,IGameStart, IListener, IActive
    {

        private InputController input;

        [Tooltip("是否Awake后直接开启")] [SerializeField] private bool isAwakeStart;
        [SerializeField] float rotateSpeed = 5f;
        [SerializeField] Transform bodyControllerTrans = null;


        private BodyControllerBase bodyController;

        private Transform rotateTrans;
        private Transform rotateTransParent;
        private Transform resetTransParent;

        private Vector3 fwd;

        private bool isActive;

        private bool isGameStart;
        private bool isGameEnd;

        private GameObject targetObj;
        private Material targetMaterial;
        public Color targetShowColor = Color.red;
        public Color targetHideColor = Color.black;

        int beClickAnim;

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



        private void Awake()
        {
            Init();
        }

        private void OnEnable()
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


        public void Init()
        {
            //指定游戏以640x960的分辨率打开游戏
            Screen.SetResolution(1920, 1080, true);

            InputController.GetInstance().IsAwakeStart = true;
            input = InputController.GetInstance();
            bodyController = bodyControllerTrans != null ? bodyControllerTrans.GetComponent<BodyController>() : null;

            if (bodyController != null)
            {
                rotateTransParent = bodyController.RotateTrans;
                resetTransParent = bodyController.transform;
            }
            beClickAnim = AnimationMgr.GetInstance().GetAnimationID("beClick");
            isGameStart = false;
            RemoveListener();
        }




#if UNITY_STANDALONE || UNITY_EDITOR

        public void PCTargetActive()
        {
            if (!isActive) return;

            if ( input == null)
            {
                input = InputController.GetInstance();
            }

            if (!isGameStart) return;
            if (isGameEnd) return;

            PCTargetRotateObj(input.TargetObj);
            ToPCRotateObj(input.FwdCam, input.GetMouseX, input.GetMouseY, input.GetMouseScrollDelta, input.IsMouse);
        }

        private void PCTargetRotateObj(GameObject _targetObj)
        {

            if (!isGameStart) return;
            if (isGameEnd) return;

            if (_targetObj == null) return;

            if (_targetObj.CompareTag("Player"))
            {
                targetObj = null;
                rotateTrans = null;
                return;
            }

            if (targetObj == _targetObj) return;

            if (bodyController != null)
            {
                GameObject go = bodyController.GetAixObj(_targetObj);
                if (go == null) return;
                //PCTargetObjAnimPlay(_targetObj);             
                if (rotateTrans == go.transform) return;
                rotateTrans = go.transform;
                rotateTransParent.position = go.transform.position;
                targetObj = go;
            }

        }

        public void ToPCRotateObj(Vector3 _fwd ,float _x, float _y,Vector2 _mouseScrollDelta, bool _isMouse)
        {

            if (!isGameStart) return;
            if (isGameEnd) return;

            if (rotateTrans == null || input == null || rotateTransParent == null) return;

            fwd = _fwd;
            fwd.Normalize();

            if (_isMouse)
            {
                rotateTrans.parent = rotateTransParent;
                Vector3 vaxis = Vector3.Cross(fwd, Vector3.right);
                rotateTransParent.Rotate(vaxis, _x * rotateSpeed, Space.World);
                Vector3 haxis = Vector3.Cross(fwd, Vector3.up);
                rotateTransParent.Rotate(haxis, _y * rotateSpeed, Space.World);
            }

            rotateTrans.parent = resetTransParent;
            rotateTransParent.rotation = Quaternion.Euler(Vector3.zero);

            if (_mouseScrollDelta.y > 0)
            {
                rotateTrans.parent = rotateTransParent;
                rotateTransParent.Rotate(Vector3.forward * rotateSpeed * 0.1f, Space.World);
            }
            else if (_mouseScrollDelta.y < 0)
            {
                rotateTrans.parent = rotateTransParent;
                rotateTransParent.Rotate(Vector3.back * rotateSpeed * 0.1f, Space.World);
            }
    
            rotateTrans.parent = resetTransParent;
            rotateTransParent.rotation = Quaternion.Euler(Vector3.zero);
        }


#endif

#if UNITY_IOS || UNITY_ANDROID || UNITY_EDITOR

        public void PhoneTargetActive()
        {
            if (!isActive) return;

            if (input == null)
            {
                input = InputController.GetInstance();
            }

            if (!isGameStart) return;
            if (isGameEnd) return;

            PhoneTargetRotateObj(input.TargetObj);
            ToPhoneRotateObj(input.FwdCam, input.IsTouch, input.DeltaPos, input.IsTwoTouch, input.TouchAngle);
        }

        private void PhoneTargetRotateObj(GameObject _targetObj)
        {

            if (!isGameStart) return;
            if (isGameEnd) return;

            if (_targetObj == null) return;

            if (_targetObj.CompareTag("Player"))
            {
                targetObj = null;
                rotateTrans = null;
                return;
            }
            if (targetObj == _targetObj) return;

            if (bodyController != null)
            {
                GameObject go = bodyController.GetAixObj(_targetObj);
                if (go != null)
                {
                    if (go.transform != rotateTrans)
                    {
                        rotateTrans = go.transform;
                        rotateTransParent.position = go.transform.position;
                        targetObj = go;
                    }
                }
            }
        }

        public void ToPhoneRotateObj(Vector3 _fwd, bool _isTouch, Vector2 _deltaPos, bool _isTwoTouch, float _angle)
        {

            if (rotateTrans == null || rotateTransParent == null ) return;

            if (!_isTouch)
            {
                rotateTrans.parent = resetTransParent;
                rotateTransParent.rotation = Quaternion.Euler(Vector3.zero);
                return;
            }

            if (!_isTwoTouch)
            {
                rotateTrans.parent = resetTransParent;
                rotateTransParent.rotation = Quaternion.Euler(Vector3.zero);

                //单点触摸滑动， 水平上下旋转
                if (_isTouch)
                {
                    rotateTrans.parent = rotateTransParent;
                    Vector2 deltaPos = _deltaPos;
                    rotateTransParent.Rotate(Vector3.down * deltaPos.x / 20 * rotateSpeed, Space.World);
                    rotateTransParent.Rotate(Vector3.right * deltaPos.y / -20 * rotateSpeed, Space.World);
                }

                rotateTrans.parent = resetTransParent;
                rotateTransParent.rotation = Quaternion.Euler(Vector3.zero);

                return;
            }
            else
            {
                rotateTrans.parent = rotateTransParent;
                rotateTransParent.Rotate(Vector3.forward * -_angle);
                //Debug.Log("_isTwoTouch");
            }

            rotateTrans.parent = resetTransParent;
            rotateTransParent.rotation = Quaternion.Euler(Vector3.zero);

        }

#endif

        public void TargetObjOnClickAnimPlay(GameObject _targetObj)
        {
            if (!isActive) return;

            if (_targetObj == null) return;

            Animator tempAnim = _targetObj.GetComponent<Animator>();
            if (tempAnim == null) return;

            AnimationMgr.GetInstance().AnimPlay(tempAnim, beClickAnim, 0);

        }

        public void TargetObjOnClickBody(GameObject _targetObj)
        {
            if (!isActive) return;

            if (_targetObj == null)
                return;

            if (_targetObj.CompareTag("Player"))
            {
                if (targetMaterial != null)
                {
                    if (targetMaterial.HasProperty("_BaseCol"))
                    {
                        if (targetMaterial.GetColor("_BaseCol") != targetHideColor)
                            targetMaterial.SetColor("_BaseCol", targetHideColor);
                    }

                    if (targetMaterial.HasProperty("_IsBase"))
                    {
                        if (targetMaterial.GetFloat("_IsBase") != 0)
                            targetMaterial.SetFloat("_IsBase", 0);
                    }
                    targetMaterial = null;
                }
                return;
            }

            if (bodyController != null)
            {
                GameObject go = bodyController.GetModelObj(_targetObj);
                if (go != null)
                {
                    MeshRenderer renderer = go.GetComponent<MeshRenderer>();
                    if (renderer != null)
                    {
                        if(targetMaterial != null)
                        {
                            if (targetMaterial.HasProperty("_BaseCol"))
                            {
                                if (targetMaterial.GetColor("_BaseCol") != targetHideColor)
                                    targetMaterial.SetColor("_BaseCol", targetHideColor);
                            }

                            if (targetMaterial.HasProperty("_IsBase"))
                            {
                                if (targetMaterial.GetFloat("_IsBase") != 0)
                                    targetMaterial.SetFloat("_IsBase", 0);
                            }

                            targetMaterial = null;
                            targetMaterial = renderer.material;
                            if (targetMaterial == null || !targetMaterial.HasProperty("_BaseCol")) return;
   
                            if (targetMaterial.GetColor("_BaseCol") != targetShowColor)
                                 targetMaterial.SetColor("_BaseCol", targetShowColor);

                            if (targetMaterial.HasProperty("_IsBase"))
                            {
                                if (targetMaterial.GetFloat("_IsBase") != 1)
                                    targetMaterial.SetFloat("_IsBase", 1);
                            }

                        }
                        else
                        {
                            targetMaterial = null;
                            targetMaterial = renderer.material;

                            if (targetMaterial == null || !targetMaterial.HasProperty("_BaseCol")) return;
                            if (targetMaterial.GetColor("_BaseCol") != targetShowColor)
                                targetMaterial.SetColor("_BaseCol", targetShowColor);

                            if (targetMaterial == null || !targetMaterial.HasProperty("_IsBase")) return;
                                if (targetMaterial.GetFloat("_IsBase") != 1)
                                    targetMaterial.SetFloat("_IsBase", 1);
                            
                        }
                    }
                }
             
            }
        }

        public void TargetObjTwoOnClickBody(GameObject _targetObj)
        {
            if (!isActive) return;

            if (_targetObj == null)
            {
                if (targetMaterial != null)
                {
                    if (targetMaterial.HasProperty("_BaseCol"))
                    {
                        if (targetMaterial.GetColor("_BaseCol") != targetHideColor)
                            targetMaterial.SetColor("_BaseCol", targetHideColor);
                    }

                    if (targetMaterial.HasProperty("_IsBase"))
                    {
                        if (targetMaterial.GetFloat("_IsBase") != 0)
                            targetMaterial.SetFloat("_IsBase", 0);
                    }
                    targetMaterial = null;
                    rotateTrans = null;
                }
            }
            else
            {
                if (bodyController != null)
                {
                    if (bodyController.GetModelObj(_targetObj) == null)
                    {
                        if (targetMaterial != null)
                        {
                            if (targetMaterial.HasProperty("_BaseCol"))
                            {
                                if (targetMaterial.GetColor("_BaseCol") != targetHideColor)
                                    targetMaterial.SetColor("_BaseCol", targetHideColor);
                            }

                            if (targetMaterial.HasProperty("_IsBase"))
                            {
                                if (targetMaterial.GetFloat("_IsBase") != 0)
                                    targetMaterial.SetFloat("_IsBase", 0);
                            }
                            targetMaterial = null;
                            rotateTrans = null;
                        }
                    }
                }
            }
        }

        public void Active()
        {
            isActive = true;
        }

        public void UnActive()
        {
            isActive = false;
        }


        /// <summary>
        /// 游戏开始事件
        /// </summary>
        public void GameStart()
        {
           // RemoveListener();
            isGameStart = true;
            isGameEnd = false;
            AddListener();
            Active();
        }

        /// <summary>
        /// 游戏重置事件
        /// </summary>
        public void GameReset()
        {
            if (bodyController)
            {
                bodyController.GameReset();
            }
        }


        /// <summary>
        /// 添加监听
        /// </summary>
        public void AddListener()
        {

#if UNITY_STANDALONE || UNITY_EDITOR
            MonoMgr.GetInstance().AddUpdateListener(PCTargetActive);
#endif

#if UNITY_IOS || UNITY_ANDROID || UNITY_EDITOR
            MonoMgr.GetInstance().AddUpdateListener(PhoneTargetActive);
#endif

            EventCenter.GetInstance().AddEventListener<GameObject>("OnClickAnim", TargetObjOnClickAnimPlay);
            EventCenter.GetInstance().AddEventListener<GameObject>("OnClickBody", TargetObjOnClickBody);
            EventCenter.GetInstance().AddEventListener<GameObject>("TwoOnClickBody", TargetObjTwoOnClickBody);
        }
        /// <summary>
        /// 移除监听
        /// </summary>
        public void RemoveListener()
        {

#if UNITY_STANDALONE || UNITY_EDITOR
            MonoMgr.GetInstance().RemoveUpdateListener(PCTargetActive);
#endif

#if UNITY_IOS || UNITY_ANDROID || UNITY_EDITOR
            MonoMgr.GetInstance().RemoveUpdateListener(PhoneTargetActive);
#endif

            EventCenter.GetInstance().RemoveEventListener<GameObject>("OnClickAnim", TargetObjOnClickAnimPlay);
            EventCenter.GetInstance().RemoveEventListener<GameObject>("OnClickBody", TargetObjOnClickBody);
            EventCenter.GetInstance().RemoveEventListener<GameObject>("TwoOnClickBody", TargetObjTwoOnClickBody);
        }

  
    }

}