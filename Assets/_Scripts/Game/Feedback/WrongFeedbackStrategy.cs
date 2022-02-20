using System;
using System.Linq;
using MemoryGame.Game.Score;
using UnityEngine;

namespace MemoryGame.Game.Feedback
{
    public class WrongFeedbackStrategy : IFeedbackStrategy
    {
        private readonly FeedbackView _feedbackView;
        public FeedbackType[] Types { get; } = { FeedbackType.Wrong };

        public WrongFeedbackStrategy(FeedbackView feedbackView)
        {
            _feedbackView = feedbackView;
        }

        public void ShowFeedback(FeedbackType feedback)
        {
            _feedbackView.ShowFeedbackText("Wrong!", Color.red);
        }
    }

    public class QuickFeedbackStrategy : IFeedbackStrategy
    {
        private readonly FeedbackView _feedbackView;
        public FeedbackType[] Types { get; } = { FeedbackType.Quick, FeedbackType.VeryQuick };

        public QuickFeedbackStrategy(FeedbackView feedbackView)
        {
            _feedbackView = feedbackView;
        }

        public void ShowFeedback(FeedbackType feedback)
        {
            var text = feedback == FeedbackType.Quick ? "Quick!" : "Very Quick!";
            _feedbackView.ShowFeedbackText(text, Color.yellow);
        }
    }

    public class StreakFeedbackStrategy : IFeedbackStrategy
    {
        private readonly FeedbackView _feedbackView;
        private readonly ScoreModel _scoreModel;
        public FeedbackType[] Types { get; } = { FeedbackType.Streak };

        public StreakFeedbackStrategy(FeedbackView feedbackView, ScoreModel scoreModel)
        {
            _feedbackView = feedbackView;
            _scoreModel = scoreModel;
        }

        public void ShowFeedback(FeedbackType feedback)
        {
            var text = $"Streak {_scoreModel.Streak}!";
            _feedbackView.ShowFeedbackText(text, Color.white);
        }
    }
}