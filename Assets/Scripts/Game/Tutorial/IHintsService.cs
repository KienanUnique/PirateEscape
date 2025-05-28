namespace Game.Tutorial
{
    public interface IHintsService
    {
        void HideHint(EHintType movementWasd);
        void ShowHint(EHintType movementWasd);
        void HideAllHints();
    }
}