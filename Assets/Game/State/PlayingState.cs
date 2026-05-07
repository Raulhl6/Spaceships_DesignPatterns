using System;

public class PlayingState : IGameSate, IEventObsever
{
    private Action<GameStateController.EGameState> _onStateFinished;
    private int _aliveShips;
    private bool _allShipsSpawned;

    public void Start(Action<GameStateController.EGameState> onStateFinished)
    {
        _onStateFinished = onStateFinished;
        _aliveShips = 0;
        _allShipsSpawned = false;
        
        EventQueue.Instance.Subscribe(EEventIds.ShipDestroyed, this);
        EventQueue.Instance.Subscribe(EEventIds.ShipSpawned, this);
        EventQueue.Instance.Subscribe(EEventIds.AllShipSpawned, this);
    }

    public void Stop()
    {
        EventQueue.Instance.UnSubscribe(EEventIds.ShipDestroyed, this);
        EventQueue.Instance.UnSubscribe(EEventIds.ShipSpawned, this);
        EventQueue.Instance.UnSubscribe(EEventIds.AllShipSpawned, this);
    }

    public void Process(EventData eventData)
    {
        switch (eventData.EventId)
        {
            case EEventIds.ShipDestroyed:
                _aliveShips--;
                var shipDestroyedEventData = (ShipDestroyedEventData) eventData;
                if (shipDestroyedEventData.Team == ETeams.Ally)
                {
                    _onStateFinished?.Invoke(GameStateController.EGameState.GameOver);
                    return;
                }
                break;
            case EEventIds.ShipSpawned:
                _aliveShips++;
                break;
            case EEventIds.AllShipSpawned:
                _allShipsSpawned = true;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        CheckGameState();
    }
    
    private void CheckGameState()
    {
        if (_aliveShips == 0 && _allShipsSpawned)
        {
            _onStateFinished?.Invoke(GameStateController.EGameState.Victory);
        }
    }
}