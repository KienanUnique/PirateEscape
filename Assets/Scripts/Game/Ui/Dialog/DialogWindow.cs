using Game.Ui.Dialog.Dialog;
using KoboldUi.Windows;
using UnityEngine;

namespace Game.Ui.Dialog
{
    public class DialogWindow : AWindow
    {
        [SerializeField] private DialogView _dialogView;
        
        protected override void AddControllers()
        {
            AddController<DialogController, DialogView>(_dialogView);
        }
    }
}