using UnityEngine;
using Utils;

namespace Game.Db.Player.Impl
{
    [CreateAssetMenu(menuName = MenuPathBase.Parameters + nameof(PlayerParameters), fileName = nameof(PlayerParameters))]
    public class PlayerParameters : ScriptableObject, IPlayerParameters
    {
        [field: SerializeField] public float Speed { get; private set; } = 10f;
        [field: SerializeField] public float Acceleration { get; private set; } = 20f;
        [field: SerializeField] public float GroundedDrag { get; private set; } = 5f;
        [field: SerializeField] public float AirborneDrag { get; private set; } = 0f;
        [field: SerializeField] public float GroundCheckSphereRadius { get; private set; } = 0.45f;
        [field: SerializeField] public float JumpForce { get; private set; } = 450f;
        [field: SerializeField] public float JumpCooldown { get; private set; } = 0.5f;
        [field: SerializeField] public float LookVerticalClamp { get; private set; } = 90f;
    }
}