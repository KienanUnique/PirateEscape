using Game.Core;
using Game.Utils.Layers;
using R3;
using R3.Triggers;
using UnityEngine;

namespace Game.Views.WinTrigger
{
    public class WinTriggerView : AView, IWinTriggerView
    {
        private readonly ReactiveCommand _playerEntered = new();
        
        [SerializeField] private Collider _winTrigger;

        public Observable<Unit> PlayerEntered => _playerEntered;

        protected override void OnInitialize()
        {
            _winTrigger.OnTriggerEnterAsObservable().Subscribe(OnWinTriggerEnter).AddTo(this);
        }

        private void OnWinTriggerEnter(Collider other)
        {
            if (!other.IsOnLayer(LayerMasks.Player))
                return;
            
            _playerEntered.Execute(Unit.Default);
        }
    }
}