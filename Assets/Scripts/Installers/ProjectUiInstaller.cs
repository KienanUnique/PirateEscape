using KoboldUi.Utils;
using Ui.LoadingWindow;
using UnityEngine;
using Utils;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(menuName = MenuPathBase.Installers + nameof(ProjectUiInstaller), fileName = nameof(ProjectUiInstaller))]
    public class ProjectUiInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private Canvas canvas;
        
        [Header("Windows")]
        [SerializeField] private LoadingWindow loadingWindow;

        public override void InstallBindings()
        {
            var canvasInstance = Instantiate(canvas);
            DontDestroyOnLoad(canvasInstance);
            
            Container.BindWindowFromPrefab(canvasInstance, loadingWindow);
        }
    }
}