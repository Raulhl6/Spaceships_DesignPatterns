using UnityEngine;

public class GameFacade : MonoBehaviour, IGameFacade
{
    [SerializeField] private ShipInstaller _shipInstaller;
    
    public void StartBattle()
    {
        ServiceLocator.Instance.GetService<ScreenFade>().Show();
        ServiceLocator.Instance.GetService<GameStateController>().Reset();
        ServiceLocator.Instance.GetService<ScoreView>().Reset();
        ServiceLocator.Instance.GetService<EnemySpawner>().StartSpawn();
        _shipInstaller.SpawnUserShip();
        ServiceLocator.Instance.GetService<ScreenFade>().Hide();
    }

    public void StopBattle()
    {
        ServiceLocator.Instance.GetService<EnemySpawner>().StopAndReset();
    }
    
}
