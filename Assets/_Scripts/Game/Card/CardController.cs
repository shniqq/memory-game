using MemoryGame.Game.CardStack;
using MemoryGame.Game.Hud;
using MemoryGame.Utils;
using UniRx;
using Zenject;

namespace MemoryGame.Game.Card
{
    public class CardController : IInitializable
    {
        private readonly CardView _view;
        private readonly CardModel _model;
        private readonly IntroView _introView;
        private readonly CardStackModel _cardStackModel;

        public CardController(CardView view, CardModel model, IntroView introView, CardStackModel cardStackModel)
        {
            _view = view;
            _model = model;
            _introView = introView;
            _cardStackModel = cardStackModel;
        }

        public void Initialize()
        {
            _view.OnCompletedIntroAnimation.Subscribe(_ => _model.OnCompletedIntro()).AddTo(_view);
            _introView.OnIntoFinished.Subscribe(_ => _view.PlayIntroAnimation()).AddTo(_view);
            _model.HasBeenPlayed.IfSwitchedToTrue().Subscribe(_ => _view.PlayCard()).AddTo(_view);

            _cardStackModel.CardsLeft.Distinct()
                .Where(_ => !_model.HasBeenPlayed.Value)
                .Subscribe(_ => _view.MoveCardToTheLeft())
                .AddTo(_view);
        }
    }
}