using UnityEngine;
using UnityEngine.InputSystem;

public readonly struct InputXEvent : IEvent
{
    public readonly float AxisX;
    public readonly double Duration;
    public readonly InputActionPhase CurrentPhase;

    public InputXEvent(float axisX, double duration, InputActionPhase currentPhase)
    {
        AxisX = axisX;
        Duration = duration;
        CurrentPhase = currentPhase;
    }
}

public readonly struct InputYEvent : IEvent
{
    public readonly float AxisY;
    public readonly double Duration;
    public readonly InputActionPhase CurrentPhase;

    public InputYEvent(float axisY, double duration, InputActionPhase currentPhase)
    {
        AxisY = axisY;
        Duration = duration;
        CurrentPhase = currentPhase;
    }
}

public readonly struct RequestInputPressEvent : IEvent
{
    public readonly double Duration;
    public readonly InputActionPhase CurrentPhase;

    public RequestInputPressEvent(double duration, InputActionPhase currentPhase)
    {
        Duration = duration;
        CurrentPhase = currentPhase;
    }
}

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

        _inputsActions.Player.Axis_X.started += MoveX;
        _inputsActions.Player.Axis_X.performed += MoveX;
        _inputsActions.Player.Axis_X.canceled += MoveX;

        _inputsActions.Player.Axis_Y.started += MoveY;
        _inputsActions.Player.Axis_Y.performed += MoveY;
        _inputsActions.Player.Axis_Y.canceled += MoveY;

        _inputsActions.Player.Press.started += PressStarted;
        _inputsActions.Player.Press.canceled += PressStarted;
    }

    private void MoveX(InputAction.CallbackContext context)
    {
        new InputXEvent(context.ReadValue<float>(), context.duration, context.phase).Invoke();
    }

    private void MoveY(InputAction.CallbackContext context)
    {
        new InputYEvent(context.ReadValue<float>(), context.duration, context.phase).Invoke();
    }

    private void PressStarted(InputAction.CallbackContext context)
    {
        new RequestInputPressEvent(context.duration, context.phase).Invoke();
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
