using System;
using System.Collections.Generic;
using DG.Tweening;
using MemoryGame.Game.Hud;
using TMPro;
using UnityEngine;
using Zenject;

namespace MemoryGame.Game
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private float _quickFeedbackThreshold;
        [SerializeField] private float _veryQuickFeedbackThreshold;
        [SerializeField] private int _streakFeedbackThreshold;

        [Inject] private FeedbackView _feedbackView;

        private int _score;
        private bool _wasLastScoreRight;
        private int _streak;
        private DateTime _lastChoiceTimeStamp;

        private void Awake()
        {
            DisplayScore();
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
            _feedbackView.ShowFeedback(FeedbackType.Wrong);
        }

        private void OnPlayerChoseRight()
        {
            _score++;
            DisplayScore();
            _scoreText.rectTransform.DOScale(1.25f, 0.25f).SetLoops(2, LoopType.Yoyo).ChangeStartValue(Vector3.one);

            var feedback = new List<FeedbackType>();

            if ((DateTime.UtcNow - _lastChoiceTimeStamp).TotalSeconds < _veryQuickFeedbackThreshold)
            {
                feedback.Add(FeedbackType.VeryQuick);
            }
            else if ((DateTime.UtcNow - _lastChoiceTimeStamp).TotalSeconds < _quickFeedbackThreshold)
            {
                feedback.Add(FeedbackType.Quick);
            }

            if (_wasLastScoreRight)
            {
                _streak++;
            }

            if (_streak >= _streakFeedbackThreshold)
            {
                feedback.Add(FeedbackType.Streak);
            }

            _wasLastScoreRight = true;
            _feedbackView.ShowFeedback(feedback.ToArray());
        }

        private void DisplayScore()
        {
            _scoreText.text = $"Score: {_score}";
        }
    }
}