using System;

public interface IEventsService
{
    void Subscribe<T>(Action<T> callback) where T : IEvent;
    void Unsubscribe<T>(Action<T> callback) where T : IEvent;
    void Invoke<T>(T eventData) where T : IEvent;
}
