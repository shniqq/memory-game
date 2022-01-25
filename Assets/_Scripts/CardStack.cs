using System;
using System.Collections.Generic;
using System.Linq;
using Card;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class CardStack : MonoBehaviour, IInitializable
{
    [SerializeField] private uint _cardAmount;
    [SerializeField, Range(0, 0.5f)] private float _spacing;
    [SerializeField, Range(0, 5f)] private float _introDuration;

    [SerializeField] private Intro _intro;

    [Inject] private CardInstaller.CardFactory _cardFactory;
    [Inject] private Score _score;

    private readonly Queue<Tuple<CardModel, CardView>> _cards = new();
    private Tuple<CardModel, CardView> _lastPlayedCard;

    public void Initialize()
    {
        for (var i = 0; i < _cardAmount; i++)
        {
            var card = _cardFactory.Create(
                new CardInstaller.CardConstructArguments(Random.Range(0, 3), i, Vector3.right * _spacing * i));
            _cards.Enqueue(card);
        }

        _intro.StartIntro(() => Invoke(nameof(StartGame), 1f));
    }

    private void StartGame()
    {
        foreach (var card in _cards)
        {
            card.Item2.PlayIntroAnimation(_introDuration);
        }

        Invoke(nameof(PlayNextCard), _introDuration);
    }

    public void OnDecidedEqualCard()
    {
        _score.OnPlayerChoice(IsLastCardSameAsCurrent());
        PlayNextCard();
    }

    public void OnDecidedDifferentCard()
    {
        _score.OnPlayerChoice(!IsLastCardSameAsCurrent());
        PlayNextCard();
    }

    private bool IsLastCardSameAsCurrent()
    {
        return _cards.Any() && _lastPlayedCard.Item1.ID == _cards.Peek().Item1.ID;
    }

    private void PlayNextCard()
    {
        if (_cards.Any())
        {
            _lastPlayedCard = _cards.Peek();
            _lastPlayedCard.Item2.PlayCard();
            _lastPlayedCard = _cards.Dequeue();
        }
    }
}