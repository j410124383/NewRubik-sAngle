

    /// <summary>
    /// 粒子系统接口
    /// </summary>
    public interface IParticleSystem
    {
        void Play();
        void Stop();
    }

    /// <summary>
    /// 控制器粒子状态枚举
    /// </summary>
    public enum ParticleControllerState
    {
        None,//无
        Stop,//停止
        Play,//播放
    }

    /// <summary>
    /// 粒子触发时刻
    /// </summary>
    public enum ParticleTriggeringConditions
    {
        None,         //无(用不触发)
        Play,          //开始后状态触发
        Stop,         //暂停后状态触发
    }

