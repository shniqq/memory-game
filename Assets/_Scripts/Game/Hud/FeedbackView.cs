using System;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MemoryGame.Game.Hud
{
    public class FeedbackView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private void Awake()
        {
            _text.gameObject.SetActive(false); 
        }

        public void ShowFeedback(params FeedbackType[] feedback)
        {
            _text.color = feedback.Contains(FeedbackType.Wrong) ? Color.red : Color.white;
        
            var feedbackText = feedback
                .Aggregate(string.Empty, (current, feedbackType) => current + $"{feedbackType}{Environment.NewLine}");

            _text.text = feedbackText;
            _text.gameObject.SetActive(true);
            _text.rectTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(-20, 20)));
            _text.rectTransform
                .DOScale(1.25f, 0.25f).SetLoops(2, LoopType.Yoyo)
                .OnComplete(() => _text.gameObject.SetActive(false));
        }
    }
}