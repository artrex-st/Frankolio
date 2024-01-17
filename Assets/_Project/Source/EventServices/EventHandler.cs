using System;
using UnityEngine;

namespace Source.ServiceEvent
{
    public class EventHandler<T> where T : IEvent
    {
        private readonly Action<T> callback;

        public EventHandler(Action<T> callback)
        {
            this.callback = callback ?? throw new ArgumentNullException(nameof(callback));
        }

        public void Invoke(object eventData)
        {
            if (eventData is T castedEventData)
            {
                callback.Invoke(castedEventData);
            }
            else
            {
                Debug.LogError($"Failed to cast event data to expected type: {typeof(T)}");
            }
        }
    }
}
