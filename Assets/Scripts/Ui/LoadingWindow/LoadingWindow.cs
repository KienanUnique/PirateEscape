using KoboldUi.Interfaces;
using KoboldUi.Windows;
using Ui.LoadingWindow.LoadingIndicator;
using UnityEngine;

namespace Ui.LoadingWindow
{
    public class LoadingWindow : AWindow
    {
        [SerializeField] private LoadingIndicatorView loadingIndicatorView;
        
        protected override void AddControllers()
        {
            AddController<LoadingIndicatorController, LoadingIndicatorView>(loadingIndicatorView);
        }
    }
}