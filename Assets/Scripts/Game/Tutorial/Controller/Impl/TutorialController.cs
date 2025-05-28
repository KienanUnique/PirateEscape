using System;
using System.Collections.Generic;
using System.Linq;
using Game.Tutorial.StateMachine;
using R3;
using Zenject;

namespace Game.Tutorial.Controller.Impl
{
    public class TutorialController : IInitializable, IDisposable, ITutorialController
    {
        private readonly Dictionary<ETutorialType, TutorialStateMachine> _tutorials;
        private readonly IHintsService _hintsService;
        private readonly CompositeDisposable _disposables = new();

        public TutorialController(
            IHintsService hintsService,
            List<TutorialStateMachine> tutorials)
        {
            _hintsService = hintsService;
            _tutorials = tutorials.ToDictionary(x => x.TutorialType, x => x);
        }

        public void Initialize() => TryStartTutorial(ETutorialType.Movement);

        public void TryStartTutorial(ETutorialType tutorialType)
        {
            if (_tutorials.TryGetValue(tutorialType, out var tutorial) && 
                tutorial.CurrentState == ETutorialState.Inactive)
            {
                tutorial.Start().Forget();
            }
        }

        public void RestartActiveTutorials()
        {
            foreach (var tutorial in _tutorials.Values)
            {
                if (tutorial.CurrentState == ETutorialState.Active)
                {
                    tutorial.ForceRestart();
                }
            }
        }

        public bool IsTutorialCompleted(ETutorialType tutorialType) =>
            _tutorials.TryGetValue(tutorialType, out var tutorial) && 
            tutorial.CurrentState == ETutorialState.Completed;

        public void Dispose()
        {
            _disposables.Dispose();
            foreach (var tutorial in _tutorials.Values)
                tutorial.Dispose();
        }
    }
}