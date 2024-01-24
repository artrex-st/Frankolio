using UnityEngine;
using UnityEngine.InputSystem;

public readonly struct StartInputXEvent : IEvent
{
    public readonly float AxisX;
    public readonly bool IsMoving;

    public StartInputXEvent(float axisX, bool isMoving)
    {
        AxisX = axisX;
        IsMoving = isMoving;
    }
}

public readonly struct PerformInputXEvent : IEvent
{
    public readonly float AxisX;
    public readonly bool IsMoving;

    public PerformInputXEvent(float axisX, bool isMoving)
    {
        AxisX = axisX;
        IsMoving = isMoving;
    }
}

public readonly struct CancelInputXEvent : IEvent
{
    public readonly float AxisX;
    public readonly bool IsMoving;

    public CancelInputXEvent(float axisX, bool isMoving)
    {
        IsMoving = isMoving;
        AxisX = axisX;
    }
}

public readonly struct StartInputYEvent : IEvent
{
    public readonly float AxisY;
    public readonly bool IsMoving;

    public StartInputYEvent(float axisY, bool isMoving)
    {
        AxisY = axisY;
        IsMoving = isMoving;
    }
}

public readonly struct PerformInputYEvent : IEvent
{
    public readonly float AxisY;
    public readonly bool IsMoving;

    public PerformInputYEvent(float axisY, bool isMoving)
    {
        AxisY = axisY;
        IsMoving = isMoving;
    }
}

public readonly struct CancelInputYEvent : IEvent
{
    public readonly float AxisY;
    public readonly bool IsMoving;

    public CancelInputYEvent(float axisY, bool isMoving)
    {
        IsMoving = isMoving;
        AxisY = axisY;
    }
}

public readonly struct RequestInputPressEvent : IEvent { }

public class InputManager : MonoBehaviour
{
    private InputActions _inputsActions;
    //private readonly List<EventHandle> _eventHandles = new();

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
        _inputsActions = new InputActions();
        _inputsActions.Player.Enable();

        _inputsActions.Player.Axis_X.started += MoveXStarted;
        _inputsActions.Player.Axis_X.performed += MoveXPerformed;
        _inputsActions.Player.Axis_X.canceled += MoveXCanceled;

        _inputsActions.Player.Axis_Y.started += MoveYStarted;
        _inputsActions.Player.Axis_Y.performed += MoveYPerformed;
        _inputsActions.Player.Axis_Y.canceled += MoveYCanceled;

        _inputsActions.Player.Press.started += PressStarted;
    }

    private void MoveXStarted(InputAction.CallbackContext context)
    {
        new StartInputXEvent(context.ReadValue<float>(), context.performed).Invoke();
    }

    private void MoveXPerformed(InputAction.CallbackContext context)
    {
        new PerformInputXEvent(context.ReadValue<float>(), context.performed).Invoke();
    }

    private void MoveXCanceled(InputAction.CallbackContext context)
    {
        new CancelInputXEvent(context.ReadValue<float>(), context.performed).Invoke();
    }

    private void MoveYStarted(InputAction.CallbackContext context)
    {
        new StartInputYEvent(context.ReadValue<float>(), context.performed).Invoke();
    }

    private void MoveYPerformed(InputAction.CallbackContext context)
    {
        new PerformInputYEvent(context.ReadValue<float>(), context.performed).Invoke();
    }

    private void MoveYCanceled(InputAction.CallbackContext context)
    {
        new CancelInputYEvent(context.ReadValue<float>(), context.performed).Invoke();
    }

    private void PressStarted(InputAction.CallbackContext context)
    {
        new RequestInputPressEvent().Invoke();
    }

    private static Vector3 ScreenToWorld(Camera camera, Vector3 position)
    {
        position.z = camera.nearClipPlane;
        return camera.ScreenToWorldPoint(position);
    }

    private void Dispose()
    {
        _inputsActions.Player.Axis_X.Dispose();
        _inputsActions.Player.Axis_Y.Dispose();
    }
}
