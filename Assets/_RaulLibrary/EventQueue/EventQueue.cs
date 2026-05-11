using System;
using System.Collections.Generic;
using UnityEngine;

public class EventQueue : MonoBehaviour, IEventQueue
{
    private Queue<EventData> _currentEvents;
    private Queue<EventData> _nextEvents;
    
    private Dictionary<EEventIds, List<IEventObsever>> _observers;
    private Queue<(EEventIds, IEventObsever)> _pendingUnsubscribes;

    private void Awake()
    {
        _currentEvents = new Queue<EventData>();
        _nextEvents = new Queue<EventData>();
        _observers = new Dictionary<EEventIds, List<IEventObsever>>();
        _pendingUnsubscribes = new Queue<(EEventIds, IEventObsever)>();
    }

    public void Subscribe(EEventIds eventId, IEventObsever eventObserver)
    {
        if (!_observers.TryGetValue(eventId, out var eventObservers))
        {
            eventObservers = new List<IEventObsever>();
        }
        eventObservers.Add(eventObserver);
        _observers[eventId] = eventObservers;
    }
    
    public void UnSubscribe(EEventIds eventId, IEventObsever eventObserver)
    {
        _pendingUnsubscribes.Enqueue((eventId, eventObserver));
    }
    
    public void EnqueueEvent(EventData eventData)
    {
        _nextEvents.Enqueue(eventData);
    }

    private void LateUpdate()
    {
        ProcessEvents();
    }

    private void ProcessEvents()
    {
        /*var tempCurrentEvents = _currentEvents;
        _currentEvents = _nextEvents;
        _nextEvents = tempCurrentEvents;*/
        
        (_currentEvents, _nextEvents) = (_nextEvents, _currentEvents);

        foreach (var currentEvent in _currentEvents)
        {
            ProcessEvent(currentEvent);
        }
        
        _currentEvents.Clear();

        // Process pending unsubscribes
        while (_pendingUnsubscribes.Count > 0)
        {
            var (eventId, observer) = _pendingUnsubscribes.Dequeue();
            if (_observers.TryGetValue(eventId, out var eventObservers))
            {
                eventObservers.Remove(observer);
                if (eventObservers.Count == 0)
                {
                    _observers.Remove(eventId);
                }
            }
        }
    }

    private void ProcessEvent(EventData eventData)
    {
        if (_observers.TryGetValue(eventData.EventId, out var eventObservers))
        {
            foreach (var eventObserver in eventObservers)
            {
                eventObserver.Process(eventData);
            }
        }
    }
}
