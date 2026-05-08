using UnityEngine;

public class LoadingScreenInstaller : InstallerBase
{
    [SerializeField] private LoadingScreen _loadingScreen;
    
    public override void Install(ServiceLocator serviceLocator)
    {
        DontDestroyOnLoad(_loadingScreen.gameObject);
        serviceLocator.RegisterService(_loadingScreen);
    }
}