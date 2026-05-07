using System;

public interface IGameSate
{
    void Start(Action<GameStateController.EGameState> onStateFinished);
    void Stop();
}