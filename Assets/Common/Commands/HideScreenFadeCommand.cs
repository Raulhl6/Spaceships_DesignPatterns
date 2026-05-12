using UnityEngine;

public class HideScreenFadeCommand : ICommand
{
    public async Awaitable Execute()
    {
        ServiceLocator.Instance.GetService<ScreenFade>().Hide();
        await Awaitable.WaitForSecondsAsync(0.2f);
    }
}