using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Services/PlayerStatus")]
public class PlayerStatus : ScriptableObject
{
    [SerializeField] private float _speed = 10;
    [SerializeField] private float _gravity = 9;
    [SerializeField] private LayerMask _jumpGroundLayer;
    [SerializeField] private float _detectGroundRange = 0.1f;
    [SerializeField] private float _jumpForce = 5;


    public float Speed => _speed;
    public float Gravity => _gravity;

    public LayerMask JumpGroundLayer => _jumpGroundLayer;
    public float DetectGroundRange => _detectGroundRange;
    public float JumpForce => _jumpForce;
}

