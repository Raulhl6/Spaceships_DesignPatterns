using System;

public class GameOverState : IGameSate
{
    private readonly GameFacade _gameFacade;

    public GameOverState(GameFacade gameFacade)
    {
        _gameFacade = gameFacade;
    }

    public void Start(Action<GameStateController.EGameState> onStateFinished)
    {
        _gameFacade.StopBattle();
        EventQueue.Instance.EnqueueEvent(new EventData(EEventIds.GameOver));
    }

    public void Stop() {}
}