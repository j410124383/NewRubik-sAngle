using UnityEngine;

/// <summary>
/// 开关模式控制特效 
/// Enable & Disable
/// </summary>
namespace MyParticlesController_ED
{

    /// <summary>
    /// 放在一个特效集合的父对象身上
    /// 主要功能
    /// 获得和存放子对象特效组件对象
    /// 计算播放最长时长
    /// </summary>
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    public abstract class PariclesBase : MonoBehaviour,IParticleSystem
    {

        [Tooltip("特效触发条件（允许在Play播放后触发 = 默认，允许在Stop暂停后触发，None无）")]
        [SerializeField] protected ParticleTriggeringConditions particleTriggering = ParticleTriggeringConditions.Play;

        [Tooltip("特效大约最大时长 （自动计算）")] [SerializeField] protected float maxLifeTime;                                //特效一次时长
        [Tooltip("特效延迟时间")] [SerializeField] protected float delayTime;                                                             //特效延迟时间
        [Tooltip("特效是否忽略TimeScale")] [SerializeField] protected bool isUnscaledDeltaTime = false;              //特效是否忽略TimeScale

        [Tooltip("特效数组")] [Space(10)] [SerializeField] protected Particle[] partials;                                           //粒子数组

        [Space(10)] [Tooltip("获得加上延迟特效时长和次数（自动计算）")] [SerializeField] protected float allTime;  // 获得加上延迟特效时长和次数

        [Tooltip("触发次数")] [SerializeField] protected int triggerNum = 1;                //触发次数

        [HideInInspector] public delegate void DelayAction();  //延迟委托方法
        protected float tempTime = 0; //临时计时
        protected bool isDelay = false;

        public ParticleTriggeringConditions ParticleTriggering { get { return this.particleTriggering; } set { this.particleTriggering = value; } }
        public float MaxLifeTime { get { return this.maxLifeTime; }}
        public float DelayTime { get { return this.delayTime; } set { this.delayTime = value; } }
        public bool IsUnscaledDeltaTime { get { return this.isUnscaledDeltaTime; } set { this.isUnscaledDeltaTime = value; } }
        public Particle[] GetParticles { get { return this.partials; } }
        /// <summary>
        /// 获得加上延迟特效时长和次数
        /// </summary>
        public float GetAllTime
        {
            get
            {
                if (maxLifeTime != -1)
                {
                    delayTime = Mathf.Max(0, delayTime);
                    triggerNum = Mathf.Max(0, triggerNum);

                    allTime = delayTime + maxLifeTime;
                    allTime *= triggerNum * 1f;
                }
                else
                {
                    allTime = -1;
                }

                return allTime;
            }
        }
        public int TriggerNum { get { return this.triggerNum; } set { this.triggerNum = value <= 0 ? 0 : value; } }


        #region mono方法

        protected virtual void Awake()
        {
            Init();
        }

#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            if (Application.isEditor)
            {

                UpdateParticles(isUnscaledDeltaTime);
                UpdateMaxLifeTime(true);

                delayTime = Mathf.Max(0, delayTime);
                triggerNum = Mathf.Max(0, triggerNum);

                if (maxLifeTime != -1)
                {
                    allTime = delayTime + maxLifeTime * triggerNum;
                    allTime *= triggerNum * 1f;
                }
                else
            {
                allTime = -1;
            }
            }
        }
#endif


        #endregion



        #region base方法

        /// <summary>
        /// 获得所有子对象 ParticleSystem
        /// </summary>
        /// <param name="enabled"> 是否获得隐藏对象 T获得, F不获得 </param>
        /// <returns></returns>
        protected ParticleSystem[] GetAllChildParticles(bool enabled = false)
        {
            return GetComponentsInChildren<ParticleSystem>(enabled);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void Init()
        {
            InitParticles();
            UpdateParticles();
            UpdateMaxLifeTime(true);
        }

        /// <summary>
        /// 初始化特效数组
        /// （有待优化）
        /// </summary>
        protected void InitParticles()
        {

            if (partials != null && partials.Length == GetAllChildParticles().Length) return;

            ParticleSystem[] pss = GetAllChildParticles(true);      //获得所有子对象特效，包括自身

            if(partials == null)
            {
                partials = new Particle[pss.Length];
            }

            Particle[] tempParticles = new Particle[pss.Length]; // 临时数组

            int partialNum = 0;
            foreach (var ps in pss)
            {
                tempParticles[partialNum] = new Particle(ps);
                partialNum++;
            }

            //替换新的数组
            if (partials != null && tempParticles != null)
            {
                for (int i = 0; i < partials.Length; i++)
                {
                    if (partials[i] == null) continue;

                    for (int j = 0; j < tempParticles.Length; j++)
                    {
                        if (tempParticles[j] == null) continue;

                        if (partials[i].particleSystem == tempParticles[j].particleSystem)
                        {
                            tempParticles[j] = partials[i];
                        }
                    }
                }
            }

            partials = new Particle[pss.Length]; //新数组
            partials = tempParticles;

        }

        /// <summary>
        /// 更新特效数据
        /// （目前只有loop）
        /// </summary>
        public void UpdateParticles(bool _isUnscaledDeltaTime = false)
        {
            if (partials == null || partials.Length != GetAllChildParticles().Length)
                InitParticles();

            foreach (var ps in partials)
            {
                if (ps != null)
                    ps.UpdateParticle(_isUnscaledDeltaTime);
            }

        }

        /// <summary>
        /// 更新播放最长时长
        /// </summary>
        /// <param name="allowLooping"></param>
        public virtual void UpdateMaxLifeTime(bool allowLooping = false)
        {
            if (partials == null) return;

            float _maxlt = 0;
           // ParticleSystem _particle = new ParticleSystem();

            //计算播放最长时长
            for (int i = 0; i < partials.Length; i++)
            {
                if (partials[i] == null) continue;

                if (partials[i].particleSystem != null)
                {
                    if (allowLooping)
                    {
                        if (partials[i].Looping)
                        {
                            maxLifeTime = -1;
                            return;
                        }
                    }

                    if (partials[i].Looping == false  && _maxlt < partials[i].MaxDuration)
                    {
                        _maxlt = partials[i].MaxDuration;
                        //_particle = partials[i].particleSystem;
                    }
                }
            }

            if (maxLifeTime != _maxlt)
            {
                Debug.LogFormat("<color=blue>(特效 （{0}） 最大播放长度) PlaybackTime</color>  :  <color=red> {1} </color>", gameObject.name ,maxLifeTime);
                maxLifeTime = _maxlt;
            }

        }

        /// <summary>
        /// 延迟计时
        /// 返回计时状态
        /// </summary>
        public virtual void ToDelay( float delay ,ref float tempTime, ref bool isDelay, out bool isDelaying, bool unscaledDeltaTime = false)
        {
            if (isDelay)
            {
                tempTime += unscaledDeltaTime ? Time.unscaledDeltaTime : Time.deltaTime;

                if (tempTime >= delay)
                {
                    isDelay = false;
                    tempTime = 0;
                }
            }

            isDelaying = isDelay;
        }


        /// <summary>
        /// 延迟后循环每帧触发
        /// </summary>
        /// <param name="delayAction">延迟后事件</param>
        public virtual void UpdateEvent(DelayAction delayAction = null)
        {
            bool isDelaying;

            ToDelay(delayTime, ref this.tempTime, ref this.isDelay, out isDelaying);

            if (isDelaying) return; //在延迟状态就返回

            if (delayAction != null)
            {
                delayAction?.Invoke();
            }

        }

        /// <summary>
        /// 带有延迟和触发次数的Update事件
        /// </summary>
        /// <param name="canUpdated">是否可以触发</param>
        /// <param name="triggerNum">触发次数</param>
        /// <param name="delayAction">延迟后事件</param>
        public virtual void UpdateEvent(ref bool canUpdated,ref int triggerNum,DelayAction delayAction = null)
        {
            if (canUpdated)
            {
                bool isDelaying;

                ToDelay(delayTime ,ref this.tempTime, ref this.isDelay, out isDelaying);

                if (isDelaying) return; //在延迟状态就返回

                if (triggerNum <= 0)
                {
                    canUpdated = false;
                    return; //检查触发次数
                }

                if (delayAction != null)
                {
                    delayAction();
                    this.tempTime = 0;
                    this.isDelay = true;
#if UNITY_EDITOR
                    Debug.LogFormat("<color=blue>(特效 （{0}）剩余播放次数) PlaybackTime</color>  :  <color=red> {1} </color>", gameObject.name, triggerNum);
#endif
                }

                triggerNum--;
            }

        }

        /// <summary>
        /// 重置延迟计算时间
        /// </summary>
        protected void ResetDelayTempTime()
        {
            tempTime = 0;
        }

        #endregion


        #region IParticleSystem 抽象接口

        public abstract void Play();
        public abstract void Stop();
        public abstract void None();

        #endregion

    }



    /// <summary>
    /// 存放 PariclesBase 对象
    /// 控制 PariclesBase 对象
    /// </summary>
    [System.Serializable]
    public class Particles
    {
        [HideInInspector] [SerializeField] private string particlesName; //用于序列化数组列表单个名字

        [Tooltip("存放 PariclesBase 类型脚本")] [SerializeField] private PariclesBase pariclesBase;
        [Tooltip("特效触发条件（允许在Play播放后触发 = 默认，允许在Stop暂停后触发，None无）")]
        [SerializeField] private ParticleTriggeringConditions particleTriggering = ParticleTriggeringConditions.Play; //特效触发条件
        [Tooltip("特效延迟时间")] [SerializeField] private float delayTime;                                                                          //特效延迟时间
        [Tooltip("特效是否忽略TimeScale")] [HideInInspector] [SerializeField] private bool isUnscaledDeltaTime = false;                           //特效是否忽略TimeScale
        [Tooltip("是否控制并更新子对象数据")] [SerializeField] private bool isUpdatedData = true;                                      //是否允许更改数据


        public PariclesBase GetPariclesBase { get { return this.pariclesBase; } }
        public ParticleTriggeringConditions ParticleTriggering { get { return this.particleTriggering; } set { this.particleTriggering = value; } }
        public float DelayTime { get { return this.delayTime; } set { this.delayTime = value; } }
        public bool IsUnscaledDeltaTime { get { return this.isUnscaledDeltaTime; } set { this.isUnscaledDeltaTime = value; } }
        public bool IsUpdatedData { get { return this.isUpdatedData; } set { this.isUpdatedData = value; } }


        /// <summary>
        /// Particles 构造函数
        /// </summary>
        /// <param name="_pariclesBase"></param>
        public Particles(PariclesBase _pariclesBase)
        {
            this.pariclesBase = _pariclesBase;
            if (_pariclesBase != null)
            {
                this.particleTriggering = _pariclesBase.ParticleTriggering;
                this.delayTime = _pariclesBase.DelayTime;
                this.particlesName = _pariclesBase.name;
                //this.isUnscaledDeltaTime = _pariclesBase.
            }
        }


        /// <summary>
        /// 更新控制的 PariclesBase 数据
        /// </summary>
        public void UpdatedParticlesData(bool _isUnscaledDeltaTime = false)
        {
            if (this.isUpdatedData == false) return;

            this.isUnscaledDeltaTime = _isUnscaledDeltaTime;

            if (this.pariclesBase != null && this.pariclesBase is PariclesBase)
            {
                if (this.pariclesBase.ParticleTriggering != this.particleTriggering || this.pariclesBase.DelayTime != this.delayTime || this.pariclesBase.IsUnscaledDeltaTime != this.isUnscaledDeltaTime)
                {
                    this.pariclesBase.ParticleTriggering = this.particleTriggering;
                    this.pariclesBase.DelayTime = this.delayTime;
                    this.pariclesBase.IsUnscaledDeltaTime = this.isUnscaledDeltaTime;
                    this.pariclesBase.UpdateParticles(isUnscaledDeltaTime);
                }
            }

            if (this.pariclesBase != null)
            {
                particlesName = this.pariclesBase.name;
            }
        }

    }



}