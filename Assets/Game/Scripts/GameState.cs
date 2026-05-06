using System;
using UnityEngine;

public class GameState : MonoBehaviour, IEventObsever
{
    [SerializeField] private GameFacade _gameFacade;
 
    private enum EGameState
    {
        Playing,
        GameOver,
        Victory
    }

    private EGameState _currentState = EGameState.Playing;
    private int _aliveShips = 0;
    private bool _allShipsSpawned = false;
    
    private void Start()
    {
        EventQueue.Instance.Subscribe(EEventIds.ShipDestroyed, this);
        EventQueue.Instance.Subscribe(EEventIds.ShipSpawned, this);
        EventQueue.Instance.Subscribe(EEventIds.AllShipSpawned, this);
    }

    private void OnDestroy()
    {
        EventQueue.Instance.UnSubscribe(EEventIds.ShipDestroyed, this);
        EventQueue.Instance.UnSubscribe(EEventIds.ShipSpawned, this);
        EventQueue.Instance.UnSubscribe(EEventIds.AllShipSpawned, this);
    }

    public void Reset()
    {
        _currentState = EGameState.Playing;
        _aliveShips = 0;
        _allShipsSpawned = false;
    }

    public void Process(EventData eventData)
    {
        if (_currentState != EGameState.Playing) return;
        
        switch (eventData.EventId)
        {
            case EEventIds.ShipDestroyed:
                _aliveShips--;
                var shipDestroyedEventData = (ShipDestroyedEventData) eventData;
                if (shipDestroyedEventData.Team == ETeams.Ally)
                {
                    _currentState = EGameState.GameOver;
                    _gameFacade.StopBattle();
                    EventQueue.Instance.EnqueueEvent(new EventData(EEventIds.GameOver));
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
            _gameFacade.StopBattle();
            _currentState = EGameState.Victory;
            EventQueue.Instance.EnqueueEvent(new EventData(EEventIds.Victory));
        }
    }
}