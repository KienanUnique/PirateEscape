using UnityEngine;
using Utils;

namespace Db.Sounds.Impl
{
    [CreateAssetMenu(menuName = MenuPathBase.Parameters + nameof(GameDefaultParameters), fileName = nameof(GameDefaultParameters))]
    public class GameDefaultParameters : ScriptableObject, IGameDefaultParameters
    {
        [Range(0f, 1f)] [SerializeField] private float soundsVolume = 0.5f;
        [Range(0f, 1f)] [SerializeField] private float musicVolume = 1f;
        [Range(0f, 1f)] [SerializeField] private float finalTitlesVolume = 0.5f;

        public float SoundsVolume => soundsVolume;
        public float MusicVolume => musicVolume;

        public float FinalTitlesVolume => finalTitlesVolume;
    }
}