using System;

public static class EventExtensions
{
    public static void AddListener<T>(this T eventData, Action<T> listener) where T : IEvent
    {
        ServiceLocator.Instance.GetService<IEventsService>().Subscribe(listener);
    }

    public static void RemoveListener<T>(this T eventData, Action<T> listener) where T : IEvent
    {
        ServiceLocator.Instance.GetService<IEventsService>().Unsubscribe(listener);
    }

    public static void Invoke<T>(this T eventData) where T : IEvent
    {
        ServiceLocator.Instance.GetService<IEventsService>().Invoke(eventData);
    }
}
