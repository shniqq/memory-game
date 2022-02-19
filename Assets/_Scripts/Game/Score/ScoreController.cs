using UniRx;
using Zenject;

namespace MemoryGame.Game.Score
{
    public class ScoreController : IInitializable
    {
        private readonly ScoreModel _model;
        private readonly ScoreView _view;

        public ScoreController(ScoreModel model, ScoreView view)
        {
            _model = model;
            _view = view;
        }

        public void Initialize()
        {
            _model.Score.Subscribe(score => _view.DisplayScore(score)).AddTo(_view);
        }
    }
}