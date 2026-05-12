using UnityEngine;

public class GameInstaller : GeneralInstallerBase
{
    [SerializeField] private ScreenFade _screenFade;
    [SerializeField] private ShipInstaller _shipInstaller;
    
    protected override void DoStart()
    {
        
    }

    protected override void DoInstallDependencies()
    {
        ServiceLocator.Instance.RegisterService(_screenFade);
        ServiceLocator.Instance.RegisterService(_shipInstaller);
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.UnregisterService<ScreenFade>();
        ServiceLocator.Instance.UnregisterService<ShipInstaller>();
    }
}