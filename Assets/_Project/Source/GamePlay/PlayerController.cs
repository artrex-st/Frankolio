using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask _jumpGroundLayer;
    [SerializeField] private Transform _jumpFootPoint;
    [SerializeField] private PlayerStatus _status;

    private Vector2 _bodyDirection;
    private Vector2 _inputDirection;
    private bool _isGround;
    private bool _isJumping;
    private IEventsService _eventService;
    private InputManager _inputManager;

    private Rigidbody2D _rigidbody => GetComponent<Rigidbody2D>();

    private void OnEnable()
    {
        Initialize();
    }

    private void Update()
    {
        Move();
        Jump();
    }

    private void FixedUpdate()
    {
        JumpGroundCheck();
        ApplyGravity();
        ApplyPhysic();
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

    private void JumpGroundCheck()
    {
        _isGround = Physics2D.Raycast(_jumpFootPoint.position, Vector2.down, _status.DetectGroundRange, _jumpGroundLayer);
        // isWall = Physics2D.OverlapCircle(wJPoint.position, wJRange, wJLayer);
        // canWallJump = isWall && !isGround && inputX != 0 && playerBody.velocity.y <= 0;
    }

    private void ApplyGravity()
    {
        if (_isGround)
        {
            return;
        }

        _bodyDirection.y -= Mathf.Clamp(_status.Gravity * Time.deltaTime, 0f, float.MaxValue);
    }

    private void ApplyPhysic()
    {
        _rigidbody.velocity = _bodyDirection;
    }

    private void Jump()
    {
        if (_isGround && _isJumping)
        {
            _bodyDirection.y = _status.JumpForce;
            _isJumping = false;
        }
    }

    private void Move()
    {
        _bodyDirection.x = _inputDirection.x * _status.Speed;
    }

    private void HandlerStartInputYEvent(InputYEvent e)
    {
        _inputDirection.y = e.AxisY;
    }

    private void HandlerStartInputXEvent(InputXEvent e)
    {
        _inputDirection.x = e.AxisX;
        new MoveAnimationEvent(e.AxisX).Invoke();
    }

    private void HandlerRequestInputPressEvent(RequestInputPressEvent e)
    {
        Debug.Log($"Button Phase: {e.CurrentPhase}");
        if (_isGround)
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
