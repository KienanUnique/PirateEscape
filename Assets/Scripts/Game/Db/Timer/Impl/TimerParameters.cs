using UnityEngine;
using Utils;

namespace Game.Db.Timer.Impl
{
    [CreateAssetMenu(menuName = MenuPathBase.Parameters + nameof(TimerParameters), fileName = nameof(TimerParameters))]
    public class TimerParameters : ScriptableObject, ITimerParameters
    {
        [field: SerializeField] public int LoseTimerDurationSeconds { get; private set; } = 60;
    }
}