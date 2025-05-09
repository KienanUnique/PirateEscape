using UnityEngine;
using Utils;

namespace Game.Db.Player.Impl
{
    [CreateAssetMenu(menuName = MenuPathBase.Parameters + nameof(PlayerParameters), fileName = nameof(PlayerParameters))]
    public class PlayerParameters : ScriptableObject, IPlayerParameters
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float Acceleration { get; private set; }
    }
}