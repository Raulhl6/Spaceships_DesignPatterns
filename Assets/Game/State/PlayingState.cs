using System;
using UnityEngine;

public class PlayingState : IGameSate, IEventObsever
{
    private Action<GameStateController.EGameState> _onStateFinished;
    private int _aliveShips;
    private bool _allShipsSpawned;
    private bool _isCurrentState = false;

    public void Start(Action<GameStateController.EGameState> onStateFinished)
    {
        _isCurrentState = true;
        _onStateFinished = onStateFinished;
        _aliveShips = 0;
        _allShipsSpawned = false;

        var eventQueue = ServiceLocator.Instance.GetService<IEventQueue>();
        
        eventQueue.Subscribe(EEventIds.ShipDestroyed, this);
        eventQueue.Subscribe(EEventIds.ShipSpawned, this);
        eventQueue.Subscribe(EEventIds.AllShipSpawned, this);
    }

    public void Stop()
    {
        _isCurrentState = false;
        var eventQueue = ServiceLocator.Instance.GetService<IEventQueue>();
        
        eventQueue.UnSubscribe(EEventIds.ShipDestroyed, this);
        eventQueue.UnSubscribe(EEventIds.ShipSpawned, this);
        eventQueue.UnSubscribe(EEventIds.AllShipSpawned, this);
    }

    public void Process(EventData eventData)
    {
        if (!_isCurrentState) return;
        
        switch (eventData.EventId)
        {
            case EEventIds.ShipDestroyed:
                _aliveShips--;
                var shipDestroyedEventData = (ShipDestroyedEventData) eventData;
                if (shipDestroyedEventData.Team == ETeams.Ally)
                {
                    _isCurrentState = false;
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
            _isCurrentState = false;
            _onStateFinished?.Invoke(GameStateController.EGameState.Victory);
        }
    }
}