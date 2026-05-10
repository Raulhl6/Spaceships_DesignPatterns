using UnityEngine;

public class GameStateInstaller : InstallerBase
{
    [SerializeField] private GameStateController _gameStateController;
    
    public override void Install(ServiceLocator serviceLocator)
    {
        serviceLocator.RegisterService(_gameStateController);
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.UnregisterService<GameStateController>();
    }
}
