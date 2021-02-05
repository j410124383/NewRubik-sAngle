using UnityEngine;

namespace MyParticlesController_ED
{

    public class TestParticleController : ParticlesControllerBase
    {

        [Tooltip("计算特效运行时长")] [Space(10)] [YProjectBase.Disabled] [SerializeField] float tempAllTime;
        bool istempAllTime;         //是否计算运行时长


        protected override void Update()
        {
            base.Update();

            //是否计算允许时间
            if (istempAllTime)
            {
                tempAllTime += Time.deltaTime;
            }
       
        }



        #region 具体实现接口

        /// <summary>
        /// 无
        /// </summary>
        public override void None()
        {
            if (partials == null && partials.Length <= 0) return;

            //所有特效触发None();
            for (int i = 0; i < partials.Length; i++)
            {
                if (partials[i] != null && partials[i].GetPariclesBase != null)
                {
                    partials[i].GetPariclesBase.None();
                }
            }

            //更新状态
            controllerState = ParticleControllerState.None;
            oldControllerState = controllerState;

            //重置停止时间计算
            tempStopTime = 0;
            isStopped = false;

            //停止计算运行时间
            istempAllTime = false;
        }

        /// <summary>
        /// 播放
        /// </summary>
        public override void Play()
        {
            if (partials == null && partials.Length <= 0) return;
           
            //更新状态
            controllerState = ParticleControllerState.Play;
            oldControllerState = controllerState;

            //触发控制的每个特效播放
            for (int i = 0; i < partials.Length; i++)
            {
                if (partials[i] != null && partials[i].GetPariclesBase != null)
                {
                    //是允许在播放状态下触发
                    if (partials[i].ParticleTriggering == ParticleTriggeringConditions.Play)
                    {
                        if (partials[i].GetPariclesBase.gameObject.activeSelf == false)
                        {
                            partials[i].GetPariclesBase.gameObject.SetActive(true);
                        }

                        partials[i].GetPariclesBase.Play();
                    }
                    else
                    {
                        partials[i].GetPariclesBase.Stop();
                    }

                }
            }
           
            //重置停止时间计算
            tempAllTime = 0;
            tempStopTime = 0;
            isStopped = true;

            //开始计算运行时间
            istempAllTime = true;
        }

        /// <summary>
        /// 停止
        /// </summary>
        public override void Stop()
        {
            if (partials == null && partials.Length <= 0) return;

            //更新状态
            controllerState = ParticleControllerState.Stop;
            oldControllerState = controllerState;

            //触发控制的每个特效停止
            for (int i = 0; i < partials.Length; i++)
            {
                if (partials[i] != null && partials[i].GetPariclesBase != null)
                {
                    //是允许在停止状态下触发
                    if (partials[i].ParticleTriggering == ParticleTriggeringConditions.Stop)
                    {
                        if (partials[i].GetPariclesBase.gameObject.activeSelf == false)
                        {
                            partials[i].GetPariclesBase.gameObject.SetActive(true);
                        }

                        partials[i].GetPariclesBase.Stop();
                        partials[i].GetPariclesBase.Play();

                    }
                    else
                    {
                        partials[i].GetPariclesBase.Stop();
                    }

                }
            }

            //停止时间计算
            isStopped = false;
            tempStopTime = 0;

            istempAllTime = false;
        }
      
        #endregion

    }

}
