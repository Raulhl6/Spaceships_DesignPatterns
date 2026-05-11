using UnityEngine;

public class GameInstaller : GeneralInstallerBase
{
    [SerializeField] private ScreenFade _screenFade;
    
    protected override void DoStart()
    {
        
    }

    protected override void DoInstallDependencies()
    {
        ServiceLocator.Instance.RegisterService(_screenFade);
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.UnregisterService<ScreenFade>();
    }
}