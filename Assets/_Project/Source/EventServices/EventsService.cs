using System;
using System.Collections.Generic;
using UnityEngine;

public class EventsService : MonoBehaviour, IEventsService
{
    private readonly Dictionary<Type, List<object>> eventListeners = new Dictionary<Type, List<object>>();

    public void Subscribe<T>(Action<T> callback) where T : IEvent
    {
        Type eventType = typeof(T);

        if (!eventListeners.ContainsKey(eventType))
        {
            eventListeners[eventType] = new List<object>();
        }

        eventListeners[eventType].Add(callback);
    }

    public void Unsubscribe<T>(Action<T> callback) where T : IEvent
    {
        Type eventType = typeof(T);

        if (eventListeners.ContainsKey(eventType))
        {
            eventListeners[eventType].Remove(callback);
        }
    }

    public void Invoke<T>(T eventData) where T : IEvent
    {
        Type eventType = typeof(T);

        if (eventListeners.ContainsKey(eventType))
        {
            foreach (object handler in eventListeners[eventType])
            {
                if (handler is Action<T> castedHandler)
                {
                    castedHandler.Invoke(eventData);
                }
            }
        }
    }
}
