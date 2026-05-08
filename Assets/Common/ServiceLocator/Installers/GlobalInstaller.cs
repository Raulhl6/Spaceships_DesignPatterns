using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class GlobalInstaller : GeneralInstallerBase
{
    protected async override void DoStart()
    {
        await LoadNextScene();
    }

    private async Task LoadNextScene()
    {
        await LoadScene("Game");
        ServiceLocator.Instance.GetService<LoadingScreen>().Hide();
    }

    

    private async Task LoadScene(string sceneName)
    {
        var loadSceneAsync = SceneManager.LoadSceneAsync(sceneName);
        while (loadSceneAsync is { isDone: false })
        {
            await Task.Yield();
        }
        await Task.Yield();
    }
}
