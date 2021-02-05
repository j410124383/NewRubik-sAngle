using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YProjectBase;

[RequireComponent(typeof(Rigidbody))]
[DisallowMultipleComponent]
public class GenerateGravity : MonoBehaviour,IListener
{
    [SerializeField] Rigidbody rig;
    [SerializeField] bool isGravity;
    [SerializeField][Range(0,10)] float gravityMultiple;
    [SerializeField] float fulljumpMultiplier = 3.2f;
    [SerializeField] float lowjumpMultiplier = 1.6f;
    [SerializeField] bool isIgnoreTimeScale = false;


    public bool SetIsGravity
    {
        get
        {
            return this.isGravity;
        }
        set
        {
            this.isGravity = value;
        }
    }

    public Rigidbody GetRigbody { get { return this.rig; } }
    public float GravityMultiple { get { return this.gravityMultiple; } set { this.gravityMultiple =  Mathf.Clamp(value,0f,10f); } }
    public float FulljumpMultiplier { get { return this.fulljumpMultiplier; } set { this.fulljumpMultiplier = value; } }
    public float LowjumpMultiplier { get { return this.lowjumpMultiplier; } set { this.lowjumpMultiplier = value; } }
    public bool IsIgnoreTimeScale { get { return this.isIgnoreTimeScale; } set { this.isIgnoreTimeScale = value; } }
    

    public void ResetVelocity()
    {
        if(rig  !=  null)
            rig.velocity = Vector3.zero; 
    }


    protected virtual void OnEnable()
    {
        if(rig == null)
            rig = GetComponent<Rigidbody>();

        AddListener();
    }

    protected virtual void OnDisable()
    {
        RemoveListener();
        rig = null;
    }

    private void OnDestroy()
    {
        RemoveListener();
        rig = null;
    }


    public void SetIsKinematic(bool _IK)
    {
        if (rig == null) return;
        rig.isKinematic = _IK;
    }


    /// <summary>
    /// 重力调节器
    /// </summary>
    public void GravityModifier()
    {
        if (isGravity)
        {
            if (rig != null)
            {
                if (rig.velocity.y <= 0f)
                    rig.velocity += Vector3.up * Physics.gravity.y * (fulljumpMultiplier - 1) * gravityMultiple * (isIgnoreTimeScale == false ? Time.fixedDeltaTime : Time.fixedUnscaledDeltaTime);
                else if (rig.velocity.y > 0)
                    rig.velocity += Vector3.up * Physics.gravity.y * (lowjumpMultiplier - 1) * gravityMultiple * (isIgnoreTimeScale == false ? Time.fixedDeltaTime : Time.fixedUnscaledDeltaTime);
            }
        }
    }

    public void AddListener()
    {
        MonoMgr.GetInstance().AddFixedUpdateListener(GravityModifier);
    }

    public void RemoveListener()
    {
        MonoMgr.GetInstance().RemoveFixedUpdateListener(GravityModifier);
    }

}
