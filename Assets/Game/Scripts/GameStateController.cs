using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    [SerializeField] private GameFacade _gameFacade;
 
    public enum EGameState
    {
        Playing,
        GameOver,
        Victory
    }

    private Dictionary<EGameState, IGameSate> _idToState;
    private IGameSate _currentState;

    private void Awake()
    {
        _idToState = new Dictionary<EGameState, IGameSate>
        {
            { EGameState.Playing, new PlayingState() },
            { EGameState.GameOver, new GameOverState(_gameFacade) },
            { EGameState.Victory, new VictoryState(_gameFacade) }
        };
    }

    private void Start()
    {
        _currentState = GetState(EGameState.Playing);
        _currentState.Start(OnChangeToNextState);
    }

    private void OnChangeToNextState(EGameState nextState)
    {
        _currentState.Stop();
        _currentState = GetState(nextState);
        _currentState.Start(OnChangeToNextState);
    }


    public void Reset()
    {
        OnChangeToNextState(EGameState.Playing);
    }

    private IGameSate GetState(EGameState gameState)
    {
        return _idToState[gameState];
    }
}