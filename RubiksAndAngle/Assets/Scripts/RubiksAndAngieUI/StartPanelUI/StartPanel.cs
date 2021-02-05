using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyTools;
using UnityEngine.UI;
using YProjectBase;

public class StartPanel : BasePanel
{

    public UIState uiState;
    UIState oldUIState;
    UIState tempUIState;

    public AnimationCurve animationCurve = AnimationCurve.Linear( 0.0f, 0.0f, 1.0f, 1.0f );
    float tempTime;

    RectTransform startBall;
    RectTransform startRotateCenter;
    Image startImage;
    Text startText;

    GameObject startUI;

    protected override void Awake()
    {
        base.Awake();
        tempTime = 0;
        startUI = transform.GetChild(0).gameObject;
    }

    private void OnEnable()
    {
        if (tempUIState != uiState)
        {
            oldUIState = tempUIState;
            tempUIState = uiState;
        }
    }

    private void Update()
    {
        if(tempUIState != uiState)
        {
            oldUIState = tempUIState;
            tempUIState = uiState;
            tempTime = 0;
        }
        else
        {
            ToUpdatedUIState(tempUIState);
        }
    }



    public override void FowardShowUI()
    {
        // 551
       
    }

    public override void BackShowUI()
    {

        if (startUI&& startUI.activeSelf == false)
        {
            startUI.SetActive(true);
        }

        // 551
        if (animationCurve.keys[animationCurve.length - 1].time > tempTime)
        {
            tempTime += Time.deltaTime / 2;
            float stepInt = animationCurve.Evaluate(tempTime);

            bool isCompleteImage = false;
            bool isCompleteText = false;

            ToMoveBall(new Vector3(551, 0, 0), stepInt, out isCompleteImage);
            ToRotateStartUI(new Vector3(0, 0, 0), stepInt, out isCompleteText);

            ToHideStartImage(1, stepInt / 2);
            ToHideStartText(1, stepInt / 2);
            //MyDebug.ToDebugLog(debug, Color.green);

            if (isCompleteText && isCompleteText)
            {
                tempTime = animationCurve.keys[animationCurve.length - 1].time + 1f;
            }

        }

    }

    public override void FowardHideUI()
    {
        // 675

        if (animationCurve.keys[animationCurve.length - 1].time > tempTime )
        {

            bool isCompleteImage = false;
            bool isCompleteText = false;

            tempTime += Time.deltaTime / 2;
            float stepInt = animationCurve.Evaluate(tempTime);

            ToMoveBall(new Vector3(675, 0, 0), stepInt, out isCompleteImage);

            ToRotateStartUI(new Vector3(0, 0, -90), stepInt, out isCompleteText);

            ToHideStartImage(0, stepInt);
            ToHideStartText(0, stepInt);

            if (isCompleteText && isCompleteText)
            {
                tempTime = animationCurve.keys[animationCurve.length - 1].time + 1f;
            }

            //MyDebug.ToDebugLog(debug, Color.green);
        }
        else
        {
            //MyDebug.ToDebugLog("startUI is  "+ startUI, Color.red);
            if (startUI != null && startUI.activeSelf)
            {
                //MyDebug.ToDebugLog("startUI Hide", Color.green);
                startUI.SetActive(false);
            }
        }

    }

    public override void BackHideUI()
    {
       
    }



    public void ToMoveBall(Vector3 targatVec , float stepInt, out bool isComplete)
    {
        if (startBall == null)
            startBall = GetControl<Image>("Startball").rectTransform;

        if (startBall != null && Vector3.Distance(startBall.anchoredPosition3D, targatVec) > 0.01f)
        {
            startBall.anchoredPosition3D = Vector3.Lerp(startBall.anchoredPosition3D, targatVec, stepInt);
            isComplete = false;
        }
        else
        {
            startBall.anchoredPosition3D = targatVec;
            isComplete = true;
        }
    }

    public void ToRotateStartUI(Vector3 targatVec , float stepInt, out bool isComplete)
    {
        if (startRotateCenter == null)
            startRotateCenter = transform as RectTransform;

        if (startRotateCenter != null && Quaternion.Angle(startRotateCenter.localRotation, Quaternion.Euler(targatVec)) > 0.05f)
        {
            startRotateCenter.localRotation = Quaternion.Lerp(startRotateCenter.localRotation, Quaternion.Euler(targatVec) , stepInt);
            isComplete = false;
        }
        else
        {
            startRotateCenter.localRotation = Quaternion.Euler(targatVec);
            isComplete = true;
        }
    }

    public void ToHideStartImage(float a, float stepInt)
    {
        if (startImage == null)
            startImage = GetControl<Image>("StartImage");

        a = Mathf.Max(0, a);
        if (startImage != null && Mathf.Abs(startImage.color.a - a) >= 0)
            startImage.color = new Color(startImage.color.r, startImage.color.g, startImage.color.b, Mathf.Lerp(startImage.color.a, a, stepInt * 2));
        else
            startImage.color = new Color(startImage.color.r, startImage.color.g, startImage.color.b, a);
        
    }

    public void ToHideStartText(float a, float stepInt)
    {
        if (startText == null)
            startText = GetControl<Text>("StartText");
        a = Mathf.Max(0, a);
        if (startText != null && Mathf.Abs(startText.color.a - a) >= 0)
            startText.color = new Color(startText.color.r, startText.color.g, startText.color.b, Mathf.Lerp(startText.color.a, a, stepInt * 2));
        else
            startText.color = new Color(startText.color.r, startText.color.g, startText.color.b, a);
    }

}
