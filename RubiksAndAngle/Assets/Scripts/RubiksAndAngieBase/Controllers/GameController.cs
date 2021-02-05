using UnityEngine;
using YProjectBase;

public class GameController : GameControllerBase
{

    [Space(20)]
    public IGameEvent gamePauseEvent;
    public IGameEvent gameContinueEvent;


    /// <summary>
    /// 继续游戏事件
    /// </summary>
    public override void GameContinue()
    {
        Time.timeScale = 1;
        if(gameContinueEvent != null)
        {
            gameContinueEvent?.Invoke(0);
        }
        gameState = GameState.Play;
    }

    /// <summary>
    /// 暂停游戏事件
    /// </summary>
    public override void GamePause()
    {
        if (gamePauseEvent != null)
        {
            gamePauseEvent?.Invoke(0);
        }
        Time.timeScale = 0;
        // Debug.Log("GamePause");
        gameState = GameState.Pause;
    }



    /// <summary>
    /// 添加监听事件
    /// </summary>
    public override void AddListener()
    {
        EventCenter.GetInstance().AddEventListener(EventData.gameEnd, GameEnd);
        EventCenter.GetInstance().AddEventListener(EventData.gameReset, GameReset);
        EventCenter.GetInstance().AddEventListener(EventData.gamePause, GamePause);
        EventCenter.GetInstance().AddEventListener(EventData.gameContinue, GameContinue);
    }

    /// <summary>
    /// 移除监听事件
    /// </summary>
    public override void RemoveListener()
    {
        EventCenter.GetInstance().RemoveEventListener(EventData.gameEnd, GameEnd);
        EventCenter.GetInstance().RemoveEventListener(EventData.gameReset, GameReset);
        EventCenter.GetInstance().RemoveEventListener(EventData.gamePause, GamePause);
        EventCenter.GetInstance().RemoveEventListener(EventData.gameContinue, GameContinue);
    }


}
