using DG.Tweening;
using TMPro;
using UnityEngine;

namespace MemoryGame.Game.Score
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;

        public void DisplayScore(int score)
        {
            _scoreText.text = $"Score: {score}";
            
            _scoreText.rectTransform.DOScale(1.25f, 0.25f).SetLoops(2, LoopType.Yoyo).ChangeStartValue(Vector3.one);
        }
    }
}