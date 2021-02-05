using UnityEngine;

namespace MyParticlesController_ED
{

    [DisallowMultipleComponent]
    /// <summary>
    /// 开关模式控制特效 
    /// Enable & Disable
    /// </summary>
    public abstract class ParticlesControllerBase : MonoBehaviour,IParticleSystem
    {
        [Tooltip("特效状态 （Play播放，Stop停止，None 无 默认）")][SerializeField] protected ParticleControllerState controllerState;
        [Tooltip("特效大约最大时长 (不算循环特效) ")] [YProjectBase.Disabled] [SerializeField] protected float maxLifeTime;
        [Tooltip("特效等待时长 (不算本身特效最大时长和循环特效) ")] protected float waitTime;
        [Tooltip("特效是否忽略TimeScale (不算循环特效) ")] [SerializeField] protected bool isUnscaledDeltaTime = false;
        [Tooltip("特效是否自动停止 (不算循环特效) ")] [SerializeField] protected bool isAutomaticStopped = true;
        protected bool isStopped = false;
        protected float tempStopTime = 0;

        [Tooltip("是否显示特效对象数组")] [Space(10)] [SerializeField] protected bool showParticles;
        [Tooltip("特效对象数组")] [YProjectBase.ConditionalHide("showParticles", false)] [SerializeField] protected Particles[] partials;                     //特效数组



        protected ParticleControllerState oldControllerState;                                        //特效旧状态
        protected PariclesBase particleMax;                                                                   //最长特效脚本


        #region Mono


        protected virtual void Awake()
        {
            Init();
        }

#if UNITY_EDITOR

        protected virtual void OnValidate()
        {
            UpdateValue();
        }

        public void UpdateValue()
        {
            if (Application.isEditor)
            {
                UpdateParticlesBases();
                UpdateParticlesData(isUnscaledDeltaTime);
                UpdateMaxLifeTime();
                waitTime = Mathf.Max(0, waitTime);
            }
        }

#endif

        protected virtual void Update()
        {
            UpdateParticleState();
            AutomaticStop(isUnscaledDeltaTime);
        }


        #endregion


        #region base方法


        /// <summary>
        /// 获得子对象 PariclesBase 数组
        /// </summary>
        /// <returns></returns>
        public PariclesBase[] GetPariclesBases()
        {
            return GetComponentsInChildren<PariclesBase>(true);
        }

        /// <summary>
        /// 初始化脚本
        /// </summary>
        public virtual void Init()
        {
            UpdateParticlesBases();
            UpdateParticlesData();
            UpdateMaxLifeTime();

            None();
        }

        /// <summary>
        /// 更新 PariclesBase[] 数组
        /// </summary>
        public void UpdateParticlesBases()
        {
            if (partials != null && partials.Length == GetPariclesBases().Length) return;

            PariclesBase[] pariclesBases = GetPariclesBases();

            if(pariclesBases == null || pariclesBases.Length <= 0)
            {
                Debug.LogFormat("<color=blue> GetPariclesBases() 数组是空的 </color>  :  <color=red> {0} </color>", pariclesBases , Color.red);
                return;
            }

            partials = new Particles[pariclesBases.Length];

            for (int i = 0; i < pariclesBases.Length; i++)
            {
                partials[i] = new Particles(pariclesBases[i]);
            }

        }

        /// <summary>
        /// 更新数据
        /// </summary>
        public void UpdateParticlesData(bool _isUnscaledDeltaTime = false)
        {
            if (partials == null || partials.Length <= 0) return; 

            for (int i = 0; i < partials.Length; i++)
            {
                if(partials[i] != null)
                {
                    if (partials[i].GetPariclesBase!= null && partials[i].GetPariclesBase.gameObject.activeSelf == false)
                    {
                        partials[i].GetPariclesBase.gameObject.SetActive(true);
                    }
                    partials[i].UpdatedParticlesData(_isUnscaledDeltaTime); 
                }
            }

        }


        /// <summary>
        /// 更新粒子状态
        /// </summary>
        protected virtual void UpdateParticleState()
        {

            //判断是否更新粒子状态
            if (oldControllerState == controllerState) return;

            oldControllerState = controllerState;

            switch (controllerState)
            {

                case ParticleControllerState.None:
                    None();
                    break;

                case ParticleControllerState.Play:
                    Play();
                    break;

                case ParticleControllerState.Stop:
                    Stop();
                    break;
            }
        }

        /// <summary>
        /// 更新播放最长时长
        /// </summary>
        [ContextMenu("UpdateMaxLifeTime")]
        protected virtual void UpdateMaxLifeTime()
        {
            if (partials == null) return;

            float _maxlt = -1;

            for (int i = 0; i < partials.Length; i++)
            {
                if (partials[i] != null)
                {
                    if (partials[i].ParticleTriggering == ParticleTriggeringConditions.Play && partials[i].GetPariclesBase != null)
                    {   
                        if ( partials[i].GetPariclesBase.GetAllTime == -1)
                        {
                            continue;
                        }
                        else if (partials[i].GetPariclesBase.GetAllTime > _maxlt)
                        {
                            _maxlt = partials[i].GetPariclesBase.GetAllTime;
                        }
                    }
                }
            }

            waitTime = Mathf.Max(0, waitTime);

            if (maxLifeTime != ( _maxlt + waitTime))
            {
                maxLifeTime = _maxlt + waitTime;
            }

        }


        /// <summary>
        /// 播放时间到自动执行停止
        /// </summary>
        public void AutomaticStop(bool _isUnscaledDeltaTime = false)
        {
            if (!isAutomaticStopped) return;

            if (isStopped)
            {
                tempStopTime += _isUnscaledDeltaTime ? Time.unscaledDeltaTime : Time.deltaTime;

                if (tempStopTime >= maxLifeTime)
                {
                    isStopped = false;
                    tempStopTime = 0;
                    controllerState = ParticleControllerState.Stop;
                }
            }
        }

        #endregion


        #region IParticleSystem抽象接口

        public abstract void Play();
        public abstract void Stop();
        public abstract void None();

        #endregion

    }

}