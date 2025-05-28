using Cysharp.Threading.Tasks;
using Game.Views.Player;
using R3;

namespace Game.Tutorial.Steps.Impl
{
    public class MovementTutorialStep : ITutorialStep, IResettableTutorialStep
    {
        public bool IsComplete { get; private set; }
    
        private readonly IHintsService _hintsService;
        private readonly IPlayer _player;
        private CancellationDisposable _disposables;

        public MovementTutorialStep(IHintsService hintsService, IPlayer player)
        {
            _hintsService = hintsService;
            _player = player;
        }

        public async UniTask Execute()
        {
            _disposables = new CancellationDisposable();
            _hintsService.ShowHint(EHintType.MovementWASD);

            await _player.OnMovementStarted.FirstAsync().AsUniTask().AttachExternalCancellation(_disposables.Token);
        
            await UniTask.Delay(2000, cancellationToken: _disposables.Token);
        
            IsComplete = true;
            _hintsService.HideHint(EHintType.MovementWASD);
        }

        public void Cancel()
        {
            _hintsService.HideHint(EHintType.MovementWASD);
            _disposables?.Dispose();
        }

        public void Reset()
        {
            IsComplete = false;
            _disposables?.Dispose();
        }
    }
}