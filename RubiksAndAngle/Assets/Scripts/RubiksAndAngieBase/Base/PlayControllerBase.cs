using UnityEngine;
using YProjectBase;

public abstract class PlayControllerBase : MonoBehaviour, IInit,IGameReset ,IGameStart, IGameEnd, IListener, IActive
{
    [SerializeField] protected bool isShowSetting; 
    [Tooltip("玩家对象")] [ConditionalHide("isShowSetting", true)] [SerializeField] protected Transform player;


    protected virtual void Awake()
    {
        Init();
    }

    protected virtual void OnDestroy()
    {
        RemoveListener();
    }


    /// <summary>
    /// Awake 初始化内容
    /// </summary>
    public virtual void Init()
    {
        player = player != null ? player : transform;
        ResetPlayer();
        RemoveListener();
    }


    protected abstract void ResetPlayer();

    public virtual void GameStart()
    {
        ResetPlayer();
        AddListener();
    }
    public virtual void GameEnd()
    {
        RemoveListener();
    }
    public virtual void GameReset()
    {
        ResetPlayer();
    }

    public abstract void AddListener();
    public abstract void RemoveListener();

    public abstract void Active();
    public abstract void UnActive();
}
