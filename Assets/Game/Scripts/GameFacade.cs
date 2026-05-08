using UnityEngine;

public class GameFacade : MonoBehaviour, IGameFacade
{
    [SerializeField] private ShipInstaller _shipInstaller;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private GameStateController gameStateController;
    
    public void StartBattle()
    {
        ScoreView.Instance.Reset();
        _enemySpawner.StartSpawn();
        _shipInstaller.SpawnUserShip();
        ServiceLocator.Instance.GetService<LoadingScreen>().Hide();
        gameStateController.Reset();
    }

    public void StopBattle()
    {
        _enemySpawner.StopAndReset();
        //ServiceLocator.Instance.GetService<LoadingScreen>().Show();
    }
    
}
