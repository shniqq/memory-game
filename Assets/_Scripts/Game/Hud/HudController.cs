using MemoryGame.Game.CardStack;
using UniRx;
using Zenject;

namespace MemoryGame.Game.Hud
{
    public class HudController : IInitializable
    {
        private readonly CardStackModel _cardStackModel;
        private readonly HudView _view;

        public HudController(CardStackModel cardStackModel, HudView view)
        {
            _cardStackModel = cardStackModel;
            _view = view;
        }

        public void Initialize()
        {
            _cardStackModel.CardsLeft.Subscribe(amount => _view.CardsLeft = amount).AddTo(_view);
        }
    }
}