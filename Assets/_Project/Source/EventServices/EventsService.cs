using System;
using System.Collections.Generic;
using UnityEngine;

public class EventsService : MonoBehaviour, IEventsService
{
    private readonly Dictionary<Type, List<object>> _eventListeners = new();

    public void Subscribe<T>(Action<T> callback) where T : IEvent
    {
        Type eventType = typeof(T);

        if (!_eventListeners.ContainsKey(eventType))
        {
            _eventListeners[eventType] = new List<object>();
        }

        _eventListeners[eventType].Add(callback);
    }

    public void Unsubscribe<T>(Action<T> callback) where T : IEvent
    {
        Type eventType = typeof(T);

        if (_eventListeners.TryGetValue(eventType, out List<object> listener))
        {
            listener.Remove(callback);
        }
    }

    public void Invoke<T>(T eventData) where T : IEvent
    {
        Type eventType = typeof(T);

        if (_eventListeners.ContainsKey(eventType))
        {
            foreach (object handler in _eventListeners[eventType])
            {
                if (handler is Action<T> castedHandler)
                {
                    castedHandler.Invoke(eventData);
                }
            }
        }
    }
}
