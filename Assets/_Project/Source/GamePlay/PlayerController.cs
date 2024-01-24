using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerStatus _status;
    private IEventsService _eventService;
    private InputManager _inputManager;
    private Vector2 _bodyDirection;

    private Rigidbody2D _rigidbody => GetComponent<Rigidbody2D>();

    private void OnEnable()
    {
        Initialize();
    }

    private void FixedUpdate()
    {
        ApplyGravity();
        ApplyPhysicMove();
    }

    private void OnDisable()
    {
        Dispose();
    }

    private void Initialize()
    {
        _inputManager = gameObject.AddComponent<InputManager>();
        new RequestInputPressEvent().AddListener(HandlerRequestInputPressEvent);
        new InputXEvent().AddListener(HandlerStartInputXEvent);
        new InputYEvent().AddListener(HandlerStartInputYEvent);
    }

    private void Dispose()
    {
        new RequestInputPressEvent().RemoveListener(HandlerRequestInputPressEvent);
        new InputXEvent().RemoveListener(HandlerStartInputXEvent);
        new InputYEvent().RemoveListener(HandlerStartInputYEvent);
    }

    private void ApplyGravity()
    {
        _bodyDirection.y = _rigidbody.velocity.y;
    }

    private void ApplyPhysicMove()
    {
        _rigidbody.velocity = _bodyDirection * _status.Speed;
    }

    private void HandlerStartInputYEvent(InputYEvent e)
    {
        //_bodyDirection.y = e.AxisY;
        Debug.Log($"Axis_X press: {e.AxisY}");
    }

    private void HandlerStartInputXEvent(InputXEvent e)
    {
        _bodyDirection.x = e.AxisX;
        Debug.Log($"Axis_X press: {e.AxisX}");
    }

    private void HandlerRequestInputPressEvent(RequestInputPressEvent e)
    {
        Debug.Log($"Button Press");
    }
}
