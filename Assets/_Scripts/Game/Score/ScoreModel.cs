using System;
using System.Collections.Generic;
using MemoryGame.Game.Feedback;
using MemoryGame.Game.Hud;
using UniRx;

namespace MemoryGame.Game.Score
{
    public class ScoreModel
    {
        private readonly IFeedbackConfig _feedbackConfig;
        private bool _wasLastScoreRight;
        private DateTime _lastChoiceTimeStamp;

        public int Streak => _streak;
        private int _streak;

        public IObservable<FeedbackType> OnShowFeedback => _onShowFeedback;
        private readonly Subject<FeedbackType> _onShowFeedback = new();

        public IReadOnlyReactiveProperty<int> Score => _score;
        private readonly ReactiveProperty<int> _score = new();

        public ScoreModel(IFeedbackConfig feedbackConfig)
        {
            _feedbackConfig = feedbackConfig;
        }

        public void OnPlayerChoice(bool playerChooseRight)
        {
            if (playerChooseRight)
            {
                OnPlayerChoseRight();
            }
            else
            {
                OnPlayerChoseWrong();
            }

            _lastChoiceTimeStamp = DateTime.UtcNow;
        }

        private void OnPlayerChoseWrong()
        {
            _wasLastScoreRight = false;
            _streak = 0;
            _onShowFeedback.OnNext(FeedbackType.Wrong);
        }

        private void OnPlayerChoseRight()
        {
            _score.Value++;

            FeedbackType? feedback = null;

            if ((DateTime.UtcNow - _lastChoiceTimeStamp).TotalSeconds < _feedbackConfig.QuickFeedbackThreshold)
            {
                feedback = FeedbackType.Quick;
            }

            if ((DateTime.UtcNow - _lastChoiceTimeStamp).TotalSeconds < _feedbackConfig.VeryQuickFeedbackThreshold)
            {
                feedback = FeedbackType.VeryQuick;
            }

            if (_wasLastScoreRight)
            {
                _streak++;
            }

            if (_streak >= _feedbackConfig.StreakFeedbackThreshold)
            {
                feedback = FeedbackType.Streak;
            }

            _wasLastScoreRight = true;
            if (feedback.HasValue)
            {
                _onShowFeedback.OnNext(feedback.Value);
            }
        }
    }
}