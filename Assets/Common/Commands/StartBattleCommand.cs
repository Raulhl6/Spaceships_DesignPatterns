
using UnityEngine;

public class StartBattleCommand : ICommand
{
    public async Awaitable Execute()
    {
        await new ShowScreenFadeCommand().Execute();
        
        ServiceLocator.Instance.GetService<GameStateController>().Reset();
        ServiceLocator.Instance.GetService<ScoreView>().Reset();
        ServiceLocator.Instance.GetService<EnemySpawner>().StartSpawn();
        ServiceLocator.Instance.GetService<ShipInstaller>().SpawnUserShip();

        await new HideScreenFadeCommand().Execute();
    }


}