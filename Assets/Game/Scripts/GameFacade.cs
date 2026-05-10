using UnityEngine;

public class GameFacade : MonoBehaviour, IGameFacade
{
    [SerializeField] private ShipInstaller _shipInstaller;
    
    public void StartBattle()
    {
        ServiceLocator.Instance.GetService<ScoreView>().Reset();
        ServiceLocator.Instance.GetService<EnemySpawner>().StartSpawn();
        _shipInstaller.SpawnUserShip();
        ServiceLocator.Instance.GetService<LoadingScreen>().Hide();
        ServiceLocator.Instance.GetService<GameStateController>().Reset();
    }

    public void StopBattle()
    {
        ServiceLocator.Instance.GetService<EnemySpawner>().StopAndReset();
        //ServiceLocator.Instance.GetService<LoadingScreen>().Show();
    }
    
}
