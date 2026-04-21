using UnityEngine;

public class GameFacade : MonoBehaviour
{
    
    [SerializeField] private ScreenFade _screenFade;
    [SerializeField] private ShipInstaller _shipInstaller;
    [SerializeField] private EnemySpawner _enemySpawner;
    
    public void StartBattle()
    {
        _enemySpawner.StartSpawn();
        _shipInstaller.SpawnUserShip();
        _screenFade.Hide();
    }

    public void StopBattle()
    {
        _enemySpawner.StopAndReset();
        _shipInstaller.DestroyUserShip();
        _screenFade.Show();
    }
    
}
