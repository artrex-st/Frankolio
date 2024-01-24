using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Services/PlayerStatus")]
public class PlayerStatus : ScriptableObject
{
    [SerializeField] private float _speed;

    public float Speed => _speed;
}

