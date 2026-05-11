using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneCommand : ICommand
{
    private readonly string _sceneToLoad;

    public LoadSceneCommand(string sceneToLoad)
    {
        _sceneToLoad = sceneToLoad;
    }


    public async Awaitable Execute()
    {
        var loadingScreen = ServiceLocator.Instance.GetService<LoadingScreen>();
        loadingScreen.Show();
    
        // Esperamos a que la escena termine de cargar
        await LoadScene(_sceneToLoad);
        await Awaitable.WaitForSecondsAsync(2f);
    
        loadingScreen.Hide();
    }

    private async Awaitable LoadScene(string sceneName)
    {
        var loadOperation = SceneManager.LoadSceneAsync(sceneName);
    
        // Awaitable permite esperar directamente un AsyncOperation
        // sin necesidad de bucles while ni Task.Yield
        await loadOperation;
    
        // Si necesitas esperar un frame extra por seguridad:
        await Awaitable.NextFrameAsync();
    }


}
