using System;
using Game.Tutorial.Controller.Impl;
using Game.Views.Player;
using R3;
using Zenject;

namespace Game.Tutorial.Handlers
{
    public class PlayerDeathHandler : IInitializable, IDisposable
    {
        private readonly IPlayer _player;
        private readonly TutorialController _tutorialController;
        private readonly CompositeDisposable _disposables = new();

        public PlayerDeathHandler(IPlayer player, TutorialController tutorialController)
        {
            _player = player;
            _tutorialController = tutorialController;
        }

        public void Initialize()
        {
            _player.OnDied.Subscribe(_ => _tutorialController.RestartActiveTutorials())
                .AddTo(_disposables);
        }

        public void Dispose() => _disposables.Dispose();
    }
}