using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YProjectBase;

public class ResetBodyAix : MonoBehaviour
{
    [Tooltip("重置位置数据")] [Disabled] [SerializeField] private Vector3 resetPos;
    [Tooltip("重置旋转数据")] [Disabled] [SerializeField] private Vector3 resetRotation;
    [HideInInspector] [SerializeField] private Quaternion resetRoate;
    [Tooltip("重置速度")] [SerializeField] float resetSpeed = 1f;
    bool isPlaying;

    private void OnValidate()
    {
        resetSpeed = resetSpeed >= 0 ? resetSpeed : 0;
    }

    private void Awake()
    {
        isPlaying = true;
    }

    private void OnEnable()
    {
        MonoMgr.GetInstance().AddUpdateListener(UpdatedResetAix);
    }

    private void OnDisable()
    {
        MonoMgr.GetInstance().RemoveUpdateListener(UpdatedResetAix);
    }

    private void OnDestroy()
    {
        MonoMgr.GetInstance().RemoveUpdateListener(UpdatedResetAix);
    }

    /// <summary>
    /// 刷新重置对象
    /// </summary>
    void UpdatedResetAix()
    {
        if (InputController.GetInstance().IsInputing) return;

        if (Vector3.Distance(resetPos, transform.position) > 0.05f)
        {
            transform.position = Vector3.Lerp(transform.position, resetPos, resetSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = resetPos;
        }

        if (Quaternion.Angle(resetRoate, transform.rotation) > 1f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, resetRoate, resetSpeed * Time.deltaTime);
        }
        else
        {
            transform.rotation = resetRoate;
        }
 
    }

    private void OnDrawGizmosSelected()
    {
        if (isPlaying) return;
        resetPos = transform.position;
        resetRoate = transform.rotation;
        resetRotation = resetRoate.eulerAngles;
    }

}
