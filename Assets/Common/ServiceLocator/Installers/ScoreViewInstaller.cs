using UnityEngine;

public class ScoreViewInstaller : InstallerBase
{
    [SerializeField] private ScoreView _scoreView;
    
    public override void Install(ServiceLocator serviceLocator)
    {
        serviceLocator.RegisterService(_scoreView);
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.UnregisterService<ScoreView>();
    }
}
