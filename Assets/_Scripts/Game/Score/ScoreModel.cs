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
        private int _streak;
        private DateTime _lastChoiceTimeStamp;

        public IObservable<FeedbackType[]> OnShowFeedback => _onShowFeedback;
        private readonly Subject<FeedbackType[]> _onShowFeedback = new();

        public IReadOnlyReactiveProperty<int> Score => _score;
        private ReactiveProperty<int> _score = new();

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
            _onShowFeedback.OnNext(new[] { FeedbackType.Wrong });
        }

        private void OnPlayerChoseRight()
        {
            _score.Value++;

            var feedback = new List<FeedbackType>();

            if ((DateTime.UtcNow - _lastChoiceTimeStamp).TotalSeconds < _feedbackConfig.VeryQuickFeedbackThreshold)
            {
                feedback.Add(FeedbackType.VeryQuick);
            }
            else if ((DateTime.UtcNow - _lastChoiceTimeStamp).TotalSeconds < _feedbackConfig.QuickFeedbackThreshold)
            {
                feedback.Add(FeedbackType.Quick);
            }

            if (_wasLastScoreRight)
            {
                _streak++;
            }

            if (_streak >= _feedbackConfig.StreakFeedbackThreshold)
            {
                feedback.Add(FeedbackType.Streak);
            }

            _wasLastScoreRight = true;
            _onShowFeedback.OnNext(feedback.ToArray());
        }
    }
}