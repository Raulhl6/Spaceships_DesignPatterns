using UnityEngine;

public abstract class GeneralInstallerBase : MonoBehaviour
{
    [SerializeField] protected InstallerBase[] _installers;

    private void Start()
    {
        InstallDependencies();
        DoStart();
    }

    protected abstract void DoStart();

    private void InstallDependencies()
    {
        foreach (var installer in _installers)
        {
            installer.Install(ServiceLocator.Instance);
        }
    }
}