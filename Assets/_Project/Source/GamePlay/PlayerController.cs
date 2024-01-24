using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private IEventsService _eventService;
    private InputManager _inputManager;

    private void OnEnable()
    {
        Initialize();
    }

    private void OnDisable()
    {
        Dispose();
    }

    private void Initialize()
    {
        _inputManager = gameObject.AddComponent<InputManager>();
        new RequestInputPressEvent().AddListener(HandlerRequestInputPressEvent);
    }

    private void Dispose()
    {
        new RequestInputPressEvent().RemoveListener(HandlerRequestInputPressEvent);
    }

    private void HandlerRequestInputPressEvent(RequestInputPressEvent e)
    {
        Debug.Log($"Button Press");
    }
}
