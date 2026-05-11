
public class GlobalInstaller : GeneralInstallerBase
{
    protected override void DoStart()
    {
        ServiceLocator.Instance.GetService<CommandQueue>().AddCommand(new LoadSceneCommand("Menu"));
    }

    protected override void DoInstallDependencies()
    {
        ServiceLocator.Instance.RegisterService(CommandQueue.Instance);
    }
}
