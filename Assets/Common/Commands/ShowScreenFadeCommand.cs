using UnityEngine;

public class ShowScreenFadeCommand : ICommand
{
    public async Awaitable Execute()
    {
        ServiceLocator.Instance.GetService<ScreenFade>().Show();
        await Awaitable.WaitForSecondsAsync(0.2f);
    }
}