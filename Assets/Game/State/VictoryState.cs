using System;

public class VictoryState : IGameSate
{
    private readonly GameFacade _gameFacade;

    public VictoryState(GameFacade gameFacade)
    {
        _gameFacade = gameFacade;
    }

    public void Start(Action<GameStateController.EGameState> onStateFinished)
    {
        _gameFacade.StopBattle();
        EventQueue.Instance.EnqueueEvent(new EventData(EEventIds.Victory));
    }

    public void Stop() {}
}