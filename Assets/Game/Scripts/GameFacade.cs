using UnityEngine;

public class GameFacade : MonoBehaviour
{
    
    [SerializeField] private ScreenFade _screenFade;
    [SerializeField] private ShipInstaller _shipInstaller;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private GameState _gameState;
    
    public void StartBattle()
    {
        ScoreView.Instance.Reset();
        _enemySpawner.StartSpawn();
        _shipInstaller.SpawnUserShip();
        _screenFade.Hide();
        _gameState.Reset();
    }

    public void StopBattle()
    {
        _enemySpawner.StopAndReset();
        _screenFade.Show();
    }
    
}
