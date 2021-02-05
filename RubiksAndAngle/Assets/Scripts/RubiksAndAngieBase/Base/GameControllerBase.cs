using UnityEngine;
using YProjectBase;

public abstract class GameControllerBase : MonoBehaviour, IInit, IGameState, IGameReset,IListener
{

    //public enum GameEventState
    //{
    //    AwakeEvent,
    //    ResetEvent,
    //    StartEvent,
    //    EndEvent,
    //    PauseEvent,
    //    ContinueEvent,
    //}
    //public GameEventState gameEvent;
    [SerializeField] private bool isShowGameData = true;
    [ConditionalHide("isShowGameData", true)] public GameData gameData;

    [Space(10)]
    public IGameEvent gameAwakeEvent;
    public IGameEvent gameResetEvent;

    [Space(20)]
    public IGameEvent gameStartEvent;
    public IGameEvent gameEndEvent;

    protected static GameState gameState;

    public static GameState GetGameState{ get { return gameState; } }

    private void Awake()
    {
        Init();
    }

    protected virtual void OnEnable()
    {
        AddListener();
        GameReset();
    }
     
    protected virtual void OnDisable()
    {
        RemoveListener();
    }

    protected virtual void OnDestroy()
    {
        RemoveListener();
    }


    /// <summary>
    /// 初始化
    /// </summary>
    public virtual void Init()
    {
        Time.timeScale = 1;
        gameState = GameState.Play;
        GameDataController.GetInstance();

        Application.targetFrameRate = 60;

        if (gameData != null)
        {
            DiamantController[] diamants = FindObjectsOfType<DiamantController>();
            for (int i = 0; i < diamants.Length; i++)
            {
                if (diamants[i])
                {
                    diamants[i].SetGameData = gameData;
                    diamants[i].Init();
                }
            }
        }

        if (gameAwakeEvent != null)
        {
            gameAwakeEvent?.Invoke(0);
        }
    }



    /// <summary>
    /// 游戏开始事件
    /// </summary>
    public virtual void GameStart()
    {
        if (gameStartEvent != null)
        {
            gameStartEvent?.Invoke(0);
        }

        if (gameData != null)
        {
            gameData.startTime = System.DateTime.Now;
        }

    }

    /// <summary>
    /// 游戏结束事件
    /// </summary>
    public virtual void GameEnd()
    {
        if (gameEndEvent != null)
        {
            gameEndEvent?.Invoke(0);
        }
    }

    /// <summary>
    /// 游戏暂停事件
    /// </summary>
    public abstract void GamePause();

    /// <summary>
    /// 游戏继续事件
    /// </summary>
    public abstract void GameContinue();

    /// <summary>
    /// 游戏重置事件
    /// </summary>
    public virtual void GameReset()
    {
        if (gameResetEvent != null)
        {
            gameResetEvent?.Invoke(0);
        }
    }



    public abstract void AddListener();
    public abstract void RemoveListener();
}
