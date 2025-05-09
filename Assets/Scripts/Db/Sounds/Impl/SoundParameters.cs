using UnityEngine;
using Utils;

namespace Db.Sounds.Impl
{
    [CreateAssetMenu(menuName = MenuPathBase.Parameters + nameof(SoundParameters), fileName = nameof(SoundParameters))]
    public class SoundParameters : ScriptableObject, ISoundParameters
    {
        [field: SerializeField] public float BackgroundFadeDownDuration { get; private set; } = 0.75f;
        [field: SerializeField] public float BackgroundFadeUpDuration { get; private set; } = 0.75f;
    }
}