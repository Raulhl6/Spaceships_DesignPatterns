public interface IEventQueue
{
    void Subscribe(EEventIds eventId, IEventObsever eventObserver);
    void UnSubscribe(EEventIds eventId, IEventObsever eventObserver);
    void EnqueueEvent(EventData eventData);
}