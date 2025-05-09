using KoboldUi.Windows;
using Splash.Ui.SplashWindow.Logo;
using UnityEngine;

namespace Splash.Ui.SplashWindow
{
    public class SplashWindow : AWindow
    {
        [SerializeField] private LogoView logoView;
        
        protected override void AddControllers()
        {
            AddController<LogoController, LogoView>(logoView);
        }
    }
}