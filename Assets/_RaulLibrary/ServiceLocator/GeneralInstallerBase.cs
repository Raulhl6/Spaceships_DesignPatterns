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
    protected abstract void DoInstallDependencies();

    private void InstallDependencies()
    {
        foreach (var installer in _installers)
        {
            installer.Install(ServiceLocator.Instance);
        }
        DoInstallDependencies();
    }
}