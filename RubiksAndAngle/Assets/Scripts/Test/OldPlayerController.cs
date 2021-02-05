using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyTools;
using RubiksAndAngie;
using MyParticlesController_ED;

[DisallowMultipleComponent]
public class OldPlayerController : MonoBehaviour,IGameEnd,IGameStart
{
    [SerializeField] float rotateSpeed = 5;
    [SerializeField] Transform rotateTrans;
    [SerializeField] Transform newRotateTrans;
    [SerializeField] Transform rotateTransParent;
    [SerializeField] Transform sphere;
    [SerializeField] IGameEvent gameResetEvent;

    ShadowAixController aixController;
    BodyControllerBase bodyController;
    ParticlesControllerBase particleController;

    GenerateGravity generateGravity;
    Vector3 resetPos;
    Transform resetTransParent;
    float X;
    float Y;

    float H;
    float V;

    Vector2 screenPos;
    bool isMouseDown;

    bool isGameStart;
    bool isGameEnd;

    public void SetIsGameStart(bool isStart)
    {
        isGameStart = isStart;
    }

    private void Awake()
    {
        generateGravity = sphere != null ? sphere.GetComponent<GenerateGravity>() : null;
        resetPos = sphere != null ? sphere.transform.position : new Vector3(2 , 5 , -5.6f);
        resetTransParent = rotateTrans.parent;// sphere != null ? sphere.parent : null;
        aixController = resetTransParent != null ? resetTransParent.GetComponent<ShadowAixController>() : null;
        bodyController = newRotateTrans != null ? newRotateTrans.GetComponent<BodyController>() : null;
        particleController = sphere != null ? sphere.GetComponent<ParticlesControllerBase>() : null;

        if (bodyController != null)
        {
            rotateTransParent = bodyController.RotateTrans;
            resetTransParent = bodyController.transform;
        }

    }

    private void OnEnable()
    {
        if(aixController != null)
        {
            if (aixController.isEditor == false) return;
            aixController.isEditor = false;
        }

        isGameStart = false;

    }


    private void Update()
    {

        if (!isGameStart) return;

        if (isGameEnd) return;

        PlayerInput();

#if UNITY_EDITOR
        PCTargetRotateObj();
        ToRotateObj(X,Y);
        //ToRotateObj();
#endif
        //TouchToRotateObj();

        ResetSphere();
    }


    public void PlayerInput()
    {
        X = Input.GetAxis("Mouse X");
        Y = -Input.GetAxis("Mouse Y");

        H = Input.GetAxisRaw("Horizontal");
        V = Input.GetAxisRaw("Vertical");
    }

    private void PCTargetRotateObj()
    {

        if (Input.GetMouseButtonDown(0))
        {
            screenPos = Input.mousePosition;
            isMouseDown = true;
        }

        if (isMouseDown && bodyController != null)
        {
            if (cam == null) cam = Camera.main;
            Ray ray = cam.ScreenPointToRay(screenPos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject go = hit.collider.gameObject;    //获得选中物体
                //string goName = go.name;    //获得选中物体的名字，使用hit.transform.name也可以
                //Debug.Log(goName);
                if (bodyController != null)
                {
                    go = bodyController.GetAixObj(go);
                    if (go == null) return;
                    rotateTrans = go.transform;
                    rotateTransParent.position = go.transform.position;
                }
            }
            isMouseDown = false;
        }

       
    }

    public void ToRotateObj()
    {

        if (rotateTrans == null) return;

        if (V != 0 && H == 0)
        {
            if (rotateTransParent != null)
            {
                rotateTrans.parent = rotateTransParent;
                ToRotateObj(ref rotateTransParent, V, Vector3.right);

            }
        }

        if (H != 0 && V == 0)
        {
            if (rotateTransParent != null)
            {
                rotateTrans.parent = rotateTransParent;
                ToRotateObj(ref rotateTransParent, -H, Vector3.up);

            }
        }

        rotateTrans.parent = resetTransParent;
        rotateTransParent.rotation = Quaternion.Euler(Vector3.zero);
    }

    public void ToRotateObj(ref Transform _trans, float _x , Vector3 _vectorAix )
    {
        if(_trans != null)
        {
                _trans.Rotate(_x * rotateSpeed * _vectorAix );
        }
    }

    Camera cam;
  
    public void ToRotateObj(float _x , float _y)
    {
        if (cam == null) cam = Camera.main;

        if (rotateTrans == null) return;

        Vector3 fwd = cam.transform.forward;
        fwd.Normalize();
        if (Input.GetMouseButton(0))
        {
            rotateTrans.parent = rotateTransParent;
            Vector3 vaxis = Vector3.Cross(fwd, Vector3.right);
            rotateTransParent.Rotate(vaxis, _x * rotateSpeed, Space.World);
            Vector3 haxis = Vector3.Cross(fwd, Vector3.up);
            rotateTransParent.Rotate(haxis, _y * rotateSpeed, Space.World);
        }

        rotateTrans.parent = resetTransParent;
        rotateTransParent.rotation = Quaternion.Euler(Vector3.zero);
    }


    private Touch oldTouch1;
    private Touch oldTouch2;

    public void TouchToRotateObj()
    {
        if (cam == null) cam = Camera.main;

        if (1 == Input.touchCount && Input.GetTouch(0).phase == TouchPhase.Stationary && bodyController != null)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 pos = touch.position;
            Ray ray = cam.ScreenPointToRay(pos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject go = hit.collider.gameObject;    //获得选中物体
               
                //string goName = go.name;    //获得选中物体的名字，使用hit.transform.name也可以
                //Debug.Log(goName);
                if (bodyController != null)
                {
                    go = bodyController.GetAixObj(go);
                    if (go != null)
                    {
                        if (go.transform != rotateTrans)
                        {
                            rotateTrans = go.transform;
                            rotateTransParent.position = go.transform.position;
                        }
                    }
                }
            }
        }

        if (rotateTrans == null) return;

        Vector3 fwd = cam.transform.forward;
        fwd.Normalize();

        //if (!Input.touchPressureSupported) return;

        if (Input.touchCount <= 0)
        {
            rotateTrans.parent = resetTransParent;
            rotateTransParent.rotation = Quaternion.Euler(Vector3.zero);
            return;
        }

        //单点触摸滑动， 水平上下旋转
        if (1 == Input.touchCount && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            rotateTrans.parent = rotateTransParent;
            Touch touch = Input.GetTouch(0);
            Vector2 deltaPos = touch.deltaPosition;
            rotateTransParent.Rotate(Vector3.down * deltaPos.x / 20, Space.World);
            rotateTransParent.Rotate(Vector3.right * deltaPos.y / -20, Space.World);
        }
       


        if (Input.touchCount <= 1)
        {
            rotateTrans.parent = resetTransParent;
            rotateTransParent.rotation = Quaternion.Euler(Vector3.zero);
            return;
        }


        Touch touch1 = Input.GetTouch(0);
        Touch touch2 = Input.GetTouch(1);


        if (touch2.phase == TouchPhase.Began)//0=>1 1=>2
        {
            oldTouch1 = touch1;
            oldTouch2 = touch2;
            return;
        }

        if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
        {
            rotateTrans.parent = rotateTransParent;
            Vector2 curVec = touch2.position - touch1.position;
            Vector2 oldVec = oldTouch2.position - oldTouch1.position;
            float angle = Vector2.Angle(oldVec, curVec);
            angle *= Mathf.Sign(Vector3.Cross(oldVec, curVec).z);
            rotateTransParent.Rotate(Vector3.forward * -angle);
            oldTouch1 = touch1;
            oldTouch2 = touch2;
        }


        rotateTrans.parent = resetTransParent;
        rotateTransParent.rotation = Quaternion.Euler(Vector3.zero);
    }

    public void ResetSphere()
    {
        if (sphere == null) return;

        if (sphere.transform.position.y < -20)
        {
            if (generateGravity != null)
            {
                generateGravity.SetIsGravity = false;

                if (particleController != null)
                {
                    particleController.Stop();
                }

                sphere.transform.position = resetPos;
                generateGravity.ResetVelocity();
                generateGravity.SetIsGravity = true;


                if (particleController != null)
                {
                    particleController.Play();
                }

            }
        }
    }

    public void StopSphere()
    {
        if (sphere == null) return;

        if (generateGravity != null)
        {
            generateGravity.SetIsGravity = false;

            if (particleController != null)
            {
                particleController.Stop();
            }

            sphere.transform.position = resetPos;
            generateGravity.ResetVelocity();

        }
    }

    public void GameEnd()
    {
        isGameEnd = true;

        StopSphere();

        StartCoroutine(ResetGame(6));
    }

    IEnumerator ResetGame(float waitTime)
    {

        float tempTime = 0;

        while (tempTime < waitTime)
        {
            tempTime += Time.deltaTime;
            yield return null;
        }

        if (generateGravity != null)
        {
            generateGravity.SetIsGravity = false;
            generateGravity.ResetVelocity();
        }

        yield return null;

        if(gameResetEvent != null)
        {
            gameResetEvent?.Invoke(null);
            isGameEnd = false;
        }

        GameStart();
    }

    public void GameStart()
    {
        isGameStart = true;

        if (sphere == null) return;

        if (generateGravity != null)
        {
            sphere.transform.position = resetPos;
            generateGravity.ResetVelocity();
            generateGravity.SetIsGravity = true;

            if (particleController != null)
            {
                particleController.Play();
            }

        }
    }

}
