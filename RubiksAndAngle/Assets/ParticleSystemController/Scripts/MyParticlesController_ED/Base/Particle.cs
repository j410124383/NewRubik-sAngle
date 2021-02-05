using MyExtension;
using UnityEngine;

namespace MyParticlesController_ED
{

    [System.Serializable]
    public class Particle 
    {
        [HideInInspector] [SerializeField] private string particleName; //用于序列化数组列表单个名字
        public ParticleSystem particleSystem;                           //存放 ParticleSystem
        [Tooltip("是否循环")] [SerializeField] private bool looping = false;              //是否循环
        [Tooltip("特效是否忽略TimeScale")] [HideInInspector] [SerializeField] private bool isUnscaledDeltaTime = false;              //特效是否忽略TimeScale
        // [Tooltip("延迟时间")] public float delayTime = 0f;
       // [HideInInspector] public float duration = 1f;

        float maxDuration;                                                            //存放最大时长       

        public bool Looping { get { return this.looping; } set { this.looping = value; } }
        public bool IsUnscaledDeltaTime { get { return this.isUnscaledDeltaTime; } set { this.isUnscaledDeltaTime = value; } }
        /// <summary>
        /// 获得最大时长
        /// </summary>
        public float MaxDuration  {  get{ maxDuration = ParticleMaxDuration(); return maxDuration;  } }



        #region 储存的初始化数据

        //float tempDelayTime = 0f;
        //float tempDuration = 1f;
        bool tempLooping = false;

        #endregion




        /// <summary>
        /// Particle 构造函数
        /// </summary>
        /// <param name="_particleSystem"></param>
        public Particle(ParticleSystem _particleSystem)
        {
            this.particleSystem = _particleSystem;
            this.particleName = _particleSystem.gameObject.name;

            if (_particleSystem != null && _particleSystem is ParticleSystem)
            {
                //this.delayTime = _particleSystem.main.startDelayMultiplier;
               // this.duration = _particleSystem.main.duration;
                this.looping = _particleSystem.main.loop;

                //this.tempDelayTime = _particleSystem.main.startDelayMultiplier;
                //this.tempDuration = _particleSystem.main.duration;
                this.tempLooping = _particleSystem.main.loop;

                this.isUnscaledDeltaTime = _particleSystem.main.useUnscaledTime;
            }
        }





        /// <summary>
        /// 更新粒子系统
        /// </summary>
        public void UpdateParticle(bool _isUnscaledDeltaTime = false)
        {
            if (particleSystem == null)
            {
                Debug.LogWarning("Particle 类 中 particleSystem == null " + this);
                return;
            }

            ParticleSystem.MainModule mainModule = this.particleSystem.main;

            if (mainModule.loop != this.looping)
            {
                this.particleSystem.Stop();
                if (this.particleSystem.isStopped && mainModule.loop != this.looping)
                {
                    mainModule.loop = this.looping;
                    //mainModule.startDelayMultiplier = Mathf.Max(0, this.delayTime);
                    //mainModule.duration = this.duration;
                    //this.delayTime = mainModule.startDelayMultiplier;
                }
            }

            this.isUnscaledDeltaTime = _isUnscaledDeltaTime;
            if (mainModule.useUnscaledTime != this.isUnscaledDeltaTime)
            {
                this.particleSystem.Stop();
                mainModule.useUnscaledTime = this.isUnscaledDeltaTime;               
            }

            if (this.particleName != particleSystem.gameObject.name)
                this.particleName = particleSystem.gameObject.name;
        }

        /// <summary>
        /// 计算最大粒子时长
        /// </summary>
        /// <returns></returns>
        float ParticleMaxDuration()
        {
            if (particleSystem == null)
            {
                Debug.LogWarning("Particle 类 中 particleSystem == null");
                return 0;
            }

            float _maxDuration = 0;


            if (particleSystem.emission.enabled)
            {
                if (particleSystem.main.loop)
                    return -1;

                float lifeTime = 0;

                if (particleSystem.emission.burstCount > 0)
                {
                    float burstTime = 0;
                    for (int j = 0; j < particleSystem.emission.burstCount; j++)
                    {
                        if (particleSystem.emission.GetBurst(j).time > burstTime)
                        {
                            burstTime = particleSystem.emission.GetBurst(j).time;
                        }
                    }

                    if (burstTime == 0)
                    {
                        float timeTemp = particleSystem.main.startDelay.YGetMaxValue() + particleSystem.main.startLifetime.YGetMaxValue();
                        if (timeTemp > lifeTime)
                        {
                            lifeTime = timeTemp;
                        }
                    }
                    else
                    {
                        float time = burstTime + particleSystem.main.startDelay.YGetMaxValue() + particleSystem.main.startLifetime.YGetMaxValue();
                        if (time > lifeTime)
                        {
                            lifeTime = time;
                        }
                    }
                }


                float rateOverTime = particleSystem.emission.rateOverTime.YGetMaxValue();
                if (rateOverTime != 0)
                {
                    float timeRo = particleSystem.main.startDelay.YGetMaxValue() + particleSystem.main.startLifetime.YGetMaxValue() + particleSystem.main.duration;
                    if (timeRo > lifeTime)
                    {
                        lifeTime = timeRo;
                    }
                }

                if (particleSystem.trails.enabled)
                {
                    float timeRr = particleSystem.main.startDelay.YGetMaxValue() + Mathf.Max(particleSystem.main.startLifetime.YGetMaxValue(), particleSystem.main.duration) + particleSystem.trails.lifetime.YGetMaxValue();
                    if (timeRr > lifeTime)
                    {
                        lifeTime = timeRr;
                    }
                }

                if (lifeTime > _maxDuration)
                {
                    _maxDuration = lifeTime;
                }

            }

            return _maxDuration;
        }

        /// <summary>
        /// 重置Particle数据
        /// （恢复到创建时）
        /// </summary>
        /// <param name="allowParticleTriggering"> 是否允许重置粒子触发状态 （T重置 ，F不重置） 重置 = None 状态 </param>
        public void ResetParticle(bool allowParticleTriggering = false)
        {
            //this.delayTime = this.tempDelayTime;
            //this.duration = this.tempDuration;
            this.looping = this.tempLooping;

            //if (allowParticleTriggering)
            //{
            //    particleTriggering = ParticleTriggeringConditions.None;
            //}

        }


    }

}
