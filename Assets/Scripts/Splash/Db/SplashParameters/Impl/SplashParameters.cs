using UnityEngine;
using Utils;

namespace Splash.Db.SplashParameters.Impl
{
    [CreateAssetMenu(menuName = MenuPathBase.Parameters + nameof(SplashParameters),
        fileName = nameof(SplashParameters))]
    public class SplashParameters : ScriptableObject, ISplashParameters
    {
        [field: SerializeField] public float SplashDuration { get; private set; } = 10;
        [field: SerializeField] public float CloseLogoDelay { get; private set; } = 8;
    }
}