
using UnityEngine.Events;

public interface IGameState : IGameStart,IGameEnd
{
    /// <summary>
    /// 游戏暂停
    /// </summary>
    void GamePause();
    /// <summary>
    /// 游戏继续
    /// </summary>
    void GameContinue();

}

public enum GameState
{
    Play,
    Pause,
}

[System.Serializable]
public class IGameEvent : UnityEvent<object> { }