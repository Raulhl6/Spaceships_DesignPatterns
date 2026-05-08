using System;
using UnityEngine;

public class GameOverState : IGameSate
{
    private readonly IGameFacade _gameFacade;

    public GameOverState(IGameFacade gameFacade)
    {
        _gameFacade = gameFacade;
    }

    public void Start(Action<GameStateController.EGameState> onStateFinished)
    {
        //Debug.Log("Game over Start");
        _gameFacade.StopBattle();
        ServiceLocator.Instance.GetService<IEventQueue>().EnqueueEvent(new EventData(EEventIds.GameOver));
    }

    public void Stop() {}
}