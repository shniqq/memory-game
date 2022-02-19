using MemoryGame.Game.Score;
using UniRx;
using Zenject;

namespace MemoryGame.Game.Feedback
{
    public class FeedbackController : IInitializable
    {
        private readonly ScoreModel _scoreModel;
        private readonly FeedbackView _feedbackView;

        public FeedbackController(ScoreModel scoreModel, FeedbackView feedbackView)
        {
            _scoreModel = scoreModel;
            _feedbackView = feedbackView;
        }

        public void Initialize()
        {
            _scoreModel.OnShowFeedback.Subscribe(feedback => _feedbackView.ShowFeedback(feedback)).AddTo(_feedbackView);
        }
    }
}