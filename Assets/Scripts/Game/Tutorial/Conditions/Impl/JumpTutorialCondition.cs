using Game.Tutorial.Controller;

namespace Game.Tutorial.Conditions.Impl
{
    public class JumpTutorialCondition : ITutorialCondition
    {
        private readonly ITutorialData _tutorialData;
        private readonly ITutorialController _tutorialController;

        public JumpTutorialCondition(ITutorialData tutorialData, ITutorialController tutorialController)
        {
            _tutorialData = tutorialData;
            _tutorialController = tutorialController;
        }

        public bool CanStartTutorial() => 
            !_tutorialData.IsTutorialCompleted(ETutorialType.Jump) &&
            _tutorialController.IsTutorialCompleted(ETutorialType.Movement);
    }
}