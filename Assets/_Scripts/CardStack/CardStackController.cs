using UniRx;
using Zenject;

namespace MemoryGame.CardStack
{
    public class CardStackController : IInitializable
    {
        private readonly CardStackModel _model;
        private readonly IntroView _introView;
        private readonly DecisionInputView _decisionInputView;

        public CardStackController(CardStackModel model, IntroView introView, DecisionInputView decisionInputView)
        {
            _model = model;
            _introView = introView;
            _decisionInputView = decisionInputView;
        }

        public void Initialize()
        {
            _decisionInputView.DifferentDecisionButton.OnClickAsObservable()
                .Subscribe(_ => _model.OnDecidedDifferentCard()).AddTo(_decisionInputView);
            _decisionInputView.EqualDecisionButton.OnClickAsObservable()
                .Subscribe(_ => _model.OnDecidedEqualCard()).AddTo(_decisionInputView);
        }
    }
}