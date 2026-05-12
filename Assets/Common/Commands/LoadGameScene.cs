using UnityEngine;

public class LoadGameScene : ICommand
{
    public async Awaitable Execute()
    {
        await new LoadSceneCommand("Game").Execute();
        await new StartBattleCommand().Execute();
        
    }
}