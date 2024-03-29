using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _jumpFootPoint;
    [SerializeField] private PlayerStatus _status;

    private Vector2 _bodyDirection;
    private Vector2 _inputDirection;
    private bool _isGround;
    private bool _isJumping;
    private bool _isGameRunning;
    private IEventsService _eventService;
    private InputManager _inputManager;
    private IGameDataService _gameDataService;

    private Rigidbody2D _rigidbody => GetComponent<Rigidbody2D>();

    private void OnEnable()
    {
        Initialize();
    }

    private void Update()
    {
        ApplyDirection();
        ApplyJump();
    }

    private void FixedUpdate()
    {
        JumpGroundCheck();
        ApplyPhysic();
    }

    private void OnDisable()
    {
        Dispose();
    }

    private void Initialize()
    {
        _gameDataService = ServiceLocator.Instance.GetService<IGameDataService>();
        new ResponseGameStateUpdateEvent().AddListener(HandlerRequestNewGameStateEvent);

        _inputManager = gameObject.AddComponent<InputManager>();
        new RequestInputPressEvent().AddListener(HandlerRequestInputPressEvent);
        new InputXEvent().AddListener(HandlerStartInputXEvent);
        new InputYEvent().AddListener(HandlerStartInputYEvent);
    }

    private void Dispose()
    {
        new ResponseGameStateUpdateEvent().RemoveListener(HandlerRequestNewGameStateEvent);
        new RequestInputPressEvent().RemoveListener(HandlerRequestInputPressEvent);
        new InputXEvent().RemoveListener(HandlerStartInputXEvent);
        new InputYEvent().RemoveListener(HandlerStartInputYEvent);
    }

    private void JumpGroundCheck()
    {
        _isGround = Physics2D.Raycast(_jumpFootPoint.position, Vector2.down, _status.DetectGroundRange, _status.JumpGroundLayer);
        // isWall = Physics2D.OverlapCircle(wJPoint.position, wJRange, wJLayer);
        // canWallJump = isWall && !isGround && inputX != 0 && playerBody.velocity.y <= 0;
    }

    private void ApplyPhysic()
    {
        if (!_isGameRunning)
        {
            return;
        }

        ApplyGravity();
        new RequestJumpAnimationEvent(_rigidbody.velocity.y, _isGround).Invoke();
        _rigidbody.velocity = _bodyDirection;
    }

    private void ApplyGravity()
    {
        if (_isGround)
        {
            if (_bodyDirection.y < -1)
            {
                _bodyDirection.y = 0;
            }
            return;
        }

        _bodyDirection.y = Mathf.Clamp(_bodyDirection.y - (_status.Gravity * Time.deltaTime), _status.MaxFallSpeed, float.MaxValue);
    }

    private void ApplyJump()
    {
        if (_isGround && _isJumping)
        {
            _bodyDirection.y = _status.JumpForce;
            _isJumping = false;
        }
    }

    private void ApplyDirection()
    {
        _bodyDirection.x = _inputDirection.x * _status.Speed;
    }

    private void HandlerRequestNewGameStateEvent(ResponseGameStateUpdateEvent e)
    {
        _isGameRunning = e.CurrentGameState.Equals(GameStates.GameRunning);
        _rigidbody.bodyType = _isGameRunning ? RigidbodyType2D.Dynamic : RigidbodyType2D.Static;
    }

    private void HandlerStartInputYEvent(InputYEvent e)
    {
        _inputDirection.y = e.AxisY;
    }

    private void HandlerStartInputXEvent(InputXEvent e)
    {
        _inputDirection.x = e.AxisX;
        new RequestMoveAnimationEvent(e.AxisX).Invoke();
    }

    private void HandlerRequestInputPressEvent(RequestInputPressEvent e)
    {
        Debug.Log($"Button Phase: {e.CurrentPhase}");
        if (_isGround && e.CurrentPhase.Equals(InputActionPhase.Started))
        {
            _isJumping = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(_jumpFootPoint.position, Vector2.down * _status.DetectGroundRange);
    }
}
