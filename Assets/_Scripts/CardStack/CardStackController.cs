using UniRx;
using Zenject;

namespace MemoryGame.CardStack
{
    public class CardStackController : IInitializable
    {
        private readonly CardStackModel _model;
        private readonly GameStateModel _gameStateModel;
        private readonly DecisionInputView _decisionInputView;

        public CardStackController(CardStackModel model, GameStateModel gameStateModel,
            DecisionInputView decisionInputView)
        {
            _model = model;
            _gameStateModel = gameStateModel;
            _decisionInputView = decisionInputView;
        }

        public void Initialize()
        {
            _decisionInputView.DifferentDecisionButton.OnClickAsObservable()
                .Where(_ => _gameStateModel.GameState.Value == GameState.Playing)
                .Subscribe(_ => _model.OnDecidedDifferentCard()).AddTo(_decisionInputView);
            _decisionInputView.EqualDecisionButton.OnClickAsObservable()
                .Where(_ => _gameStateModel.GameState.Value == GameState.Playing)
                .Subscribe(_ => _model.OnDecidedEqualCard()).AddTo(_decisionInputView);
        }
    }
}