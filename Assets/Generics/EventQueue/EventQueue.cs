using System;
using System.Collections.Generic;
using UnityEngine;

public class EventQueue : MonoBehaviour
{
    
    public static EventQueue Instance { get; private set; }

    private Queue<EventData> _currentEvents;
    private Queue<EventData> _nextEvents;
    
    private Dictionary<EEventIds, List<IEventObsever>> _observers;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        _currentEvents = new Queue<EventData>();
        _nextEvents = new Queue<EventData>();
        _observers = new Dictionary<EEventIds, List<IEventObsever>>();
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
        _observers[eventId].Remove(eventObserver);
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
