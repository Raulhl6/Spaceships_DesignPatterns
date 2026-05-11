using UnityEngine;

public class EnemySpawnerInstaller : InstallerBase
{
    [SerializeField] private EnemySpawner _enemySpawner;
    
    public override void Install(ServiceLocator serviceLocator)
    {
        serviceLocator.RegisterService(_enemySpawner);
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.UnregisterService<EnemySpawner>();
    }
}
