using MyParticlesController_ED;
using UnityEngine;
using YProjectBase;

public class PlayController : PlayControllerBase
{

    [ConditionalHide("isShowSetting", true)] [SerializeField] Transform particleDoTW = null;
    ParticlesControllerBase particleController;
    GenerateGravity generateGravity;
    Vector3 resetPos;

    bool isReset;
    bool isDie;
    bool isDowning;

    public override void Init()
    {
        player = player != null ? player : transform;
        generateGravity = player != null ? player.GetComponent<GenerateGravity>() : GetComponent<GenerateGravity>();
        resetPos = player != null ? player.transform.position : new Vector3(2, 5, -5.6f); 
        particleController = player != null ? player.GetComponent<ParticlesControllerBase>() : GetComponent<ParticlesControllerBase>();
        ResetPlayer();
        RemoveListener(); 
    }

    protected override void ResetPlayer()
    {
        if (player == null) return;

        if (generateGravity != null)
        {
            if (particleDoTW)
                particleDoTW.gameObject.SetActive(true);
            StopPlayer();
            EventCenter.GetInstance().EventTrigger(EventData.playerReset);

            if (GameDataController.GetInstance() != null && GameDataController.GetInstance().musicData != null)
                MusicMgr.GetInstance().StopSound(GameDataController.GetInstance().musicData.GetSEClip(5));
        
            isReset = true;
        }
    }



    private void ActivePlayer()
    {
       
        if (!isReset) return;
        if (InputController.GetInstance().TargetObj == this.gameObject && InputController.GetInstance().IsInputing)
        {
            if (generateGravity != null)
            {
                generateGravity.SetIsGravity = true;
                generateGravity.SetIsKinematic(false);
                isReset = false;
                isDie = false;
            }
            if (particleController != null)
            {
                particleController.Play();
            }

            if (GameDataController.GetInstance() != null && GameDataController.GetInstance().musicData != null)
                MusicMgr.GetInstance().PlaySound(GameDataController.GetInstance().musicData.GetSEClip(3));

            if (particleDoTW)
                particleDoTW.gameObject.SetActive(false);
        }
    }

    public void DownPlayerEvnet()
    {
        if (player == null) return;

        if (!isDie && player.transform.position.y < -5.5f)
        {
            isDie = true;
            if (GameDataController.GetInstance() != null && GameDataController.GetInstance().musicData != null)
                MusicMgr.GetInstance().PlaySound(GameDataController.GetInstance().musicData.GetSEClip(5));
        }

        if (player.transform.position.y < -8)
        {
            ResetPlayer();
        }
    }

    private void StopPlayer()
    {
        if (player == null) return;

        if (generateGravity != null)
        {
            generateGravity.SetIsGravity = false;

            if (particleController != null)
            {
                particleController.Stop();
            }

            player.transform.position = resetPos;
            generateGravity.ResetVelocity();
            generateGravity.SetIsKinematic(true);
        }
    }



    public void DownColliderEvent()
    {
        if(Physics.CheckSphere(transform.position, transform.lossyScale.x / 1.5f, 1 << 0))
        {
            if (!isDowning)
            {
                isDowning = true;

                if (GameDataController.GetInstance() != null && GameDataController.GetInstance().musicData != null)
                    MusicMgr.GetInstance().PlaySound(GameDataController.GetInstance().musicData.GetSEClip(4));
            }
        }
        else
            isDowning = false;
              
    }


    public override void GameEnd()
    {
        //StopPlayer();

        //if(gameEndEvent != null)
        //{
        //    gameEndEvent?.Invoke(0);
        //}

        RemoveListener();

        if (player == null) return;

        if (generateGravity != null)
        {
            if (player.transform.position.y < -20)
            {
                generateGravity.SetIsGravity = false;

                if (particleController != null)
                {
                    particleController.Stop();
                }

                generateGravity.ResetVelocity();
            }
        }


    }
    public override void GameStart()
    {
        ResetPlayer();
        AddListener();
    }
  
    public override void Active()
    {
        if (isReset) return;
        if (player == null) return;
        if (generateGravity != null)
        {
            generateGravity.SetIsGravity = true;
        }
    }      
    public override void UnActive()
    {
        if (isReset) return;
        if (player == null) return;
        if (generateGravity != null)
        {
            generateGravity.SetIsGravity = false;
        }
    }
           
    public override void AddListener()
    {
        MonoMgr.GetInstance().AddUpdateListener(ActivePlayer);
        MonoMgr.GetInstance().AddLateUpdateListener(DownPlayerEvnet);
        MonoMgr.GetInstance().AddFixedUpdateListener(DownColliderEvent);
    }       
    public override void RemoveListener()
    {
        MonoMgr.GetInstance().RemoveUpdateListener(ActivePlayer);
        MonoMgr.GetInstance().RemoveLateUpdateListener(DownPlayerEvnet);
        MonoMgr.GetInstance().RemoveFixedUpdateListener(DownColliderEvent);
    }

#if UNITY_EDITOR

    SphereCollider sphereCollider;

    private void OnDrawGizmosSelected()
    {
      // if (sphereCollider == null)
      //     sphereCollider = GetComponent<SphereCollider>();

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, transform.lossyScale.x / 1.8f);
    }

#endif

}
