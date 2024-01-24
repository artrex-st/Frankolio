using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Services/PlayerStatus")]
public class PlayerStatus : ScriptableObject
{
    [SerializeField] private float _speed;
    [SerializeField] private float _gravity;


    public float Speed => _speed;
    public float Gravity => _gravity;
}

