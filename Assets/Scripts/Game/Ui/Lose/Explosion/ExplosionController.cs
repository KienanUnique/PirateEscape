using KoboldUi.Element.Controller;

namespace Game.Ui.Lose.Explosion
{
    public class ExplosionController : AUiController<ExplosionView>
    {
        protected override void OnOpen()
        {
            View.Play();
        }
    }
}