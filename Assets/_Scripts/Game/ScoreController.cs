using Zenject;

namespace MemoryGame.Game
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
            
        }
    }
}