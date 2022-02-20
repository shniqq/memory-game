using System.Collections.Generic;
using System.Linq;
using MemoryGame.Game.Score;
using UniRx;
using UnityEngine;
using Zenject;

namespace MemoryGame.Game.Feedback
{
    public class FeedbackController : IInitializable
    {
        private readonly ScoreModel _scoreModel;
        private readonly FeedbackView _feedbackView;

        [Inject] private List<IFeedbackStrategy> _feedbackStrategies;

        public FeedbackController(ScoreModel scoreModel, FeedbackView feedbackView)
        {
            _scoreModel = scoreModel;
            _feedbackView = feedbackView;
        }

        public void Initialize()
        {
            _scoreModel.OnShowFeedback.Subscribe(ShowFeedback).AddTo(_feedbackView);
        }

        private void ShowFeedback(FeedbackType feedback)
        {
            var strategy = _feedbackStrategies.FirstOrDefault(strategy => strategy.Types.Contains(feedback));

            if (strategy == null)
            {
                Debug.LogError(new KeyNotFoundException($"Unable to find feedback strategy for {feedback}!"));
                return;
            }

            strategy.ShowFeedback(feedback);
        }
    }
}