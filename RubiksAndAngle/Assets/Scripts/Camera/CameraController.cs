using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YProjectBase;

public class CameraController : MonoBehaviour,IInit,IGameStart
{
    //public IGameEvent gameAwakeEvent;
    //public IGameEvent gameStartEvent;
    public Camera cambody;
    public Vector3 initQuaternion = new Vector3(0, 0, 0);
    Quaternion quaternion1;

    Camera mainCam;
    Camera bodyCam;
    Camera backgroundCam;
    Color camCol = Color.black;

    public Camera GetMainCam
    {
        get
        {
            if (mainCam == null)
                mainCam = Camera.main;
            return mainCam;
        }
    }

    bool isWait;

    private void Awake()
    {
        Init();
    }

    //private void Update()
    //{
    //    if (isWait) return;
    //    GameStart();
    //}

    IEnumerator WaitGame(float waitTime)
    {

        float tempTime = 0;

        while (tempTime < waitTime)
        {
            tempTime += Time.deltaTime;
            yield return null;
        }

        yield return null;

        isWait = false;
        
    }

    public void Init()
    {
        //if (gameAwakeEvent != null)
        //{
        //    Debug.Log("Game Awake");
        //    gameAwakeEvent?.Invoke(null);
        //}
        mainCam = null;
        bodyCam = null;

        if (mainCam == null)
            mainCam = Camera.main;

        //设置当前分辨率


        //Screen.SetResolution(1920, 1080, true);

        //Screen.fullScreen = false;
        //mainCam.
        //mainCam.aspect = 1.78f;

        if (bodyCam != null)
            bodyCam.aspect = mainCam.aspect;

        quaternion1 = Quaternion.Euler(0, -180, 0);
        transform.localRotation = Quaternion.Euler(initQuaternion);


        //if (cambody != null)
        //    AspectUtility.SetCamera(ref cambody);

        //if (mainCam)
        //    AspectUtility.SetCamera(ref mainCam);
        SetCam();

    }

    [ContextMenu("SetCam")]
    public void SetCam()
    {
        if (mainCam == null || mainCam != Camera.main)
            mainCam = Camera.main;
        // mainCam.aspect = 1.78f;
        if (bodyCam == null || bodyCam != cambody.transform.GetComponent<Camera>())
            bodyCam = cambody.transform.GetComponent<Camera>();

        if (mainCam) mainCam.aspect = 1.78f;
        if (bodyCam) bodyCam.aspect = 1.78f;

        float currentAspectRatio = (float)Screen.width / Screen.height;

        Rect temprect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
        if ((int)(currentAspectRatio * 100) / 100.0f == (int)(1.78f * 100) / 100.0f)
        {
            mainCam.rect = temprect;
            if (bodyCam)
                bodyCam.rect = temprect;
            return;
        }

        if (InputController.GetInstance().CamRect !=  new Rect(0,0,0,0))
        {
            mainCam.rect = InputController.GetInstance().CamRect;
            if (bodyCam)
                bodyCam.rect = InputController.GetInstance().CamRect;
            return;
        }

        if (currentAspectRatio > 1.78f)
        {
            float inset = 1.0f - 1.78f / currentAspectRatio;
            temprect = new Rect(inset / 2, 0.0f, 1.0f - inset, 1.0f);
        }
        // Letterbox
        else
        {
            float inset = 1.0f - currentAspectRatio / 1.78f;
            temprect = new Rect(0.0f, inset / 2, 1.0f, 1.0f - inset);
        }

        InputController.GetInstance().CamRect = temprect;
       // Debug.Log(temprect);
        mainCam.rect = temprect;
        if (bodyCam)
            bodyCam.rect = temprect;

        if (backgroundCam)
        {
            Destroy(backgroundCam.gameObject);
            backgroundCam = null;
        }

        if (!backgroundCam)
        {
            // Make a new camera behind the normal camera which displays black; otherwise the unused space is undefined
            backgroundCam = new GameObject("BackgroundCam", typeof(Camera)).GetComponent<Camera>();
            backgroundCam.orthographic = true;
            backgroundCam.depth = int.MinValue;
            backgroundCam.clearFlags = CameraClearFlags.SolidColor;
            backgroundCam.backgroundColor = camCol;
            backgroundCam.cullingMask = 0;
            if (mainCam)
            {
                backgroundCam.transform.position = mainCam.transform.position;
                backgroundCam.transform.rotation = mainCam.transform.rotation;
                backgroundCam.transform.localScale = mainCam.transform.localScale;
            }
        }
    }

    public void CamLaft()
    {
        //设置屏幕自动旋转， 并置支持的方向
        Screen.orientation = ScreenOrientation.AutoRotation;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
    }
    public void GameStart()
    {
        transform.localRotation = quaternion1;
        //if (gameStartEvent != null)
        //{
        //    Debug.Log("GameStart");
        //    gameStartEvent?.Invoke(null);
        //}
    }


}
