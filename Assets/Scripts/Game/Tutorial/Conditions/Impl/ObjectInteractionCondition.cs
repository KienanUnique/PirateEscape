using Game.Views.Player;

namespace Game.Tutorial.Conditions.Impl
{
    public class ObjectInteractionCondition : ITutorialCondition
    {
        private readonly ITutorialData _tutorialData;
        private readonly IPlayer _player;

        public ObjectInteractionCondition(ITutorialData tutorialData, IPlayer player)
        {
            _tutorialData = tutorialData;
            _player = player;
        }

        public bool CanStartTutorial() => 
            !_tutorialData.IsTutorialCompleted(ETutorialType.ObjectInteraction) &&
            _player.IsInInteractionZone;
    }
}