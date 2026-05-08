using System;
using UnityEngine;

public class VictoryState : IGameSate
{
    private readonly IGameFacade _gameFacade;

    public VictoryState(IGameFacade gameFacade)
    {
        _gameFacade = gameFacade;
    }

    public void Start(Action<GameStateController.EGameState> onStateFinished)
    {
        Debug.Log("Victory Start");
        _gameFacade.StopBattle();
        ServiceLocator.Instance.GetService<IEventQueue>().EnqueueEvent(new EventData(EEventIds.Victory));
    }

    public void Stop() {Debug.Log("Victory Stop");}
}