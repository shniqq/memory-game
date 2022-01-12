using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Intro : MonoBehaviour
{
    [SerializeField] private TMP_Text _introText;

    public void StartIntro(Action onIntroFinished)
    {
        var introTextSequence = _introText.rectTransform
            .DOScale(1.1f, 3f)
            .ChangeStartValue(Vector3.one)
            .SetEase(Ease.Linear)
            .OnStart(() => _introText.text =
                "Identify if the new card is equal to the previous one by using the buttons below");

        DOTween.Sequence()
            .Insert(0f, introTextSequence)
            .Insert(3f, CreateCountdownTween(3))
            .Insert(4f, CreateCountdownTween(2))
            .Insert(5f, CreateCountdownTween(1))
            .OnComplete(() =>
            {
                _introText.gameObject.SetActive(false);
                onIntroFinished.Invoke();
            });
    }

    private Tween CreateCountdownTween(int counter)
    {
        return _introText.rectTransform
            .DOScale(1.2f, 1f)
            .OnStart(() => _introText.text = counter.ToString());
    }
}