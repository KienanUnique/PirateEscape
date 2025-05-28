namespace Game.Tutorial.Conditions.Impl
{
    public class MovementTutorialCondition : ITutorialCondition
    {
        private readonly ITutorialData _tutorialData;

        public MovementTutorialCondition(ITutorialData tutorialData)
        {
            _tutorialData = tutorialData;
        }

        public bool CanStartTutorial() => !_tutorialData.IsTutorialCompleted(ETutorialType.Movement);
    }
}