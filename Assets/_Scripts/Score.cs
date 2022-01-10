using DG.Tweening;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;

    private int _score;

    private void Awake()
    {
        DisplayScore();
    }

    public void OnPlayerChoice(bool playerChooseRight)
    {
        if (playerChooseRight)
        {
            _score++;
            DisplayScore();
            _scoreText.rectTransform.DOScale(1.25f, 0.25f).SetLoops(2, LoopType.Yoyo);
        }
    }

    private void DisplayScore()
    {
        _scoreText.text = $"Score: {_score}";
    }
}