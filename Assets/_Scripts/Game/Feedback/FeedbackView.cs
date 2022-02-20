using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MemoryGame.Game.Feedback
{
    public class FeedbackView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private void Awake()
        {
            _text.gameObject.SetActive(false);
        }

        public void ShowFeedbackText(string text, Color color)
        {
            _text.color = color;
            _text.text = text;
            _text.gameObject.SetActive(true);
            _text.rectTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(-20, 20)));
            _text.rectTransform
                .DOScale(1.25f, 0.25f).SetLoops(2, LoopType.Yoyo)
                .OnComplete(() => _text.gameObject.SetActive(false));
        }
    }
}