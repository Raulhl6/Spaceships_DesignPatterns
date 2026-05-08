using System;
using UnityEngine;

public class GameFacadeInstaller : InstallerBase
{
    [SerializeField] private GameFacade _gameFacade;
    
    public override void Install(ServiceLocator serviceLocator)
    {
        serviceLocator.RegisterService<IGameFacade>(_gameFacade);
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.UnregisterService<IGameFacade>();
    }
}