using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YProjectBase;

public class BlackMark : BasePanel, IListener
{
    [SerializeField] Material markMaterial;
    [SerializeField] Transform targetObj;
    [Disabled] [SerializeField] Vector2 center = new Vector2(0.5f, 0.5f);

    [Space(5)] [SerializeField] float speed = 1;
    [Range(0, 1)] [SerializeField] float raidus = 1;
    [SerializeField] bool isUnscaleTime = false;


    float tempRaidus;
    [HideInInspector] [SerializeField] private Vector4 cicle;
    [HideInInspector] [SerializeField] private Vector2 targetCenter;

    public float Speed { set { speed = Mathf.Clamp(value, 1, 5); } get { return speed; } }
    public float Raidus { set { raidus = Mathf.Clamp(value, 0, 1); } get { return raidus; } }
    public Vector2 Center { set { center = new Vector2(Mathf.Clamp(value.x, 0, 1), Mathf.Clamp(value.y, 0, 1)); } get { return center; } }

#if UNITY_EDITOR

    bool isPlaying;

    private void OnValidate()
    {
        if (isPlaying) return;
        if (Application.isEditor)
        {
            if (markMaterial != null)
            {
                speed = Mathf.Clamp(speed, 1, 25);

                if (Camera.main == null) return;
                if (targetObj != null)
                {
                    Vector3 campoint = Camera.main.WorldToScreenPoint(targetObj.position);
                    center = new Vector2(-campoint.x / Screen.width, campoint.y / Screen.height);
                }

                center = new Vector2(Mathf.Clamp(center.x, 0, 1), Mathf.Clamp(center.y, 0, 1));              
                cicle = new Vector4(center.x, center.y, raidus);
                markMaterial.SetVector("_Circle", cicle);

            }

            if (markMaterial == null)
                markMaterial = GetComponent<Image>().material;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (isPlaying) return;

        if (targetObj != null)
        {
            Vector3 campoint = Camera.main.WorldToScreenPoint(targetObj.position);
            center = new Vector2(-campoint.x / Screen.width, campoint.y / Screen.height);
        }
    }

#endif

    protected override void Awake()
    {

#if UNITY_EDITOR

        isPlaying = true;

#endif
        base.Awake();

        if (markMaterial == null)
            markMaterial = GetComponent<Image>().material;
        tempRaidus = raidus;
        AddListener();
    }

    private void OnDestroy()
    {
        RemoveListener();
    }



    public void UpdatedMarkAix()
    {
        UpdatedMarkAix(targetObj);
    }

    public void UpdatedMarkAix(Transform targetTrans)
    {
        if (targetObj != targetTrans && targetTrans != null)
            targetObj = targetTrans;

        if (targetObj == null) return;
        Vector3 campoint = InputController.GetInstance().GetMainCam.WorldToScreenPoint(targetObj.position);
        Vector2 temp = new Vector2(campoint.x / Screen.width, campoint.y / Screen.height);
        UpdatedMarkAix(temp, raidus);
    }

    private void UpdatedMarkAix(Vector2 _center, float _raidus)
    {
        targetCenter = new Vector2(Mathf.Clamp(_center.x - 0.03f, 0, 1), Mathf.Clamp(_center.y, 0, 1));
        raidus = Mathf.Clamp(_raidus, 0, 1);
        center = Vector2.Lerp(center, targetCenter, speed * (isUnscaleTime ? Time.unscaledDeltaTime : Time.deltaTime));
        tempRaidus = Mathf.Lerp(tempRaidus, raidus, speed * 0.5f * (isUnscaleTime ? Time.unscaledDeltaTime : Time.deltaTime));
        cicle = new Vector4(center.x, center.y, tempRaidus);

        MarkAnim(cicle);
    }

    private void MarkAnim(Vector4 _cicle)
    {
        if (markMaterial != null)
        {
            if(markMaterial.HasProperty("_Circle"))
                markMaterial.SetVector("_Circle", _cicle);
        }
    }




    public void AddListener()
    {
        MonoMgr.GetInstance().AddUpdateListener(UpdatedMarkAix);
    }

    public void RemoveListener()
    {
        MonoMgr.GetInstance().RemoveUpdateListener(UpdatedMarkAix);
    }

}
