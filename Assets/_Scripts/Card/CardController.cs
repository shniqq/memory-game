using UniRx;
using Zenject;

namespace MemoryGame.Card
{
    public class CardController : IInitializable
    {
        private readonly CardView _view;
        private readonly CardModel _model;
        private readonly IntroView _introView;

        public CardController(CardView view, CardModel model, IntroView introView)
        {
            _view = view;
            _model = model;
            _introView = introView;
        }

        public void Initialize()
        {
            _view.OnCompletedIntroAnimation.Subscribe(_ => _model.OnCompletedIntro()).AddTo(_view);
            _introView.OnIntoFinished.Subscribe(_ => _view.PlayIntroAnimation()).AddTo(_view);
            _model.OnPlayedCard.Subscribe(_ => _view.PlayCard()).AddTo(_view);
        }
    }
}