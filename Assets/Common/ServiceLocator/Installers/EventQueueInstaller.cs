using UnityEngine;

public class EventQueueInstaller : InstallerBase
{
    [SerializeField] private EventQueue _eventQueue;
    
    public override void Install(ServiceLocator serviceLocator)
    {
        DontDestroyOnLoad(_eventQueue.gameObject);
        serviceLocator.RegisterService<IEventQueue>(_eventQueue);
        Debug.Log("Event queue registered");
    }
}