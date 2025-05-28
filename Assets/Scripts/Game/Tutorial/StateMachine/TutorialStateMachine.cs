using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Game.Tutorial.Conditions;
using Game.Tutorial.Steps;
using R3;

namespace Game.Tutorial.StateMachine
{
    public class TutorialStateMachine : IDisposable
{
    public ETutorialType TutorialType { get; }
    public ETutorialState CurrentState { get; private set; } = ETutorialState.Inactive;
    
    private readonly List<ITutorialStep> _steps;
    private readonly ITutorialCondition _startCondition;
    private readonly IHintsService _hintsService;
    private readonly CancellationDisposable _disposables = new();
    private int _currentStepIndex = 0;

    public TutorialStateMachine(
        ETutorialType tutorialType,
        List<ITutorialStep> steps,
        ITutorialCondition startCondition,
        IHintsService hintsService)
    {
        TutorialType = tutorialType;
        _steps = steps;
        _startCondition = startCondition;
        _hintsService = hintsService;
    }

    public async UniTaskVoid Start()
    {
        if (CurrentState == ETutorialState.Completed || !_startCondition.CanStartTutorial())
            return;

        CurrentState = ETutorialState.Active;
        _currentStepIndex = 0;

        try
        {
            while (_currentStepIndex < _steps.Count)
            {
                var currentStep = _steps[_currentStepIndex];
                await currentStep.Execute();
                
                if (!currentStep.IsComplete)
                {
                    CurrentState = ETutorialState.Restarting;
                    await HandleRestart();
                    return;
                }
                
                _currentStepIndex++;
            }

            CurrentState = ETutorialState.Completed;
        }
        catch (Exception)
        {
            CurrentState = ETutorialState.Restarting;
            await HandleRestart();
            throw;
        }
    }

    private async UniTask HandleRestart()
    {
        _hintsService.HideAllHints();
        
        if (_currentStepIndex < _steps.Count)
            _steps[_currentStepIndex].Cancel();

        await UniTask.WaitUntil(() => _startCondition.CanStartTutorial(), 
            cancellationToken: _disposables.Token);
        
        ResetAllSteps();
        Start().Forget();
    }

    private void ResetAllSteps()
    {
        foreach (var step in _steps.OfType<IResettableTutorialStep>())
            step.Reset();
    }

    public void ForceRestart()
    {
        if (CurrentState == ETutorialState.Active)
        {
            CurrentState = ETutorialState.Restarting;
            HandleRestart().Forget();
        }
    }

    public void Dispose() => _disposables.Dispose();
}
}