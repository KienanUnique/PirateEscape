using UnityEngine;
using Utils;

namespace FinalTitles.Db.Video.Impl
{
    [CreateAssetMenu(menuName = MenuPathBase.Parameters + nameof(VideoBase), fileName = nameof(VideoBase))]
    public class VideoBase : ScriptableObject, IVideoBase
    {
        [field: SerializeField] public string DefuseWinVideoName { get; private set; }
        [field: SerializeField] public string MealWinVideoName { get; private set; }
    }
}