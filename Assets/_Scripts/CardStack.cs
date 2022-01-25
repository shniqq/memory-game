using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class CardStack : MonoBehaviour, IInitializable
{
    [SerializeField] private uint _cardAmount;
    [SerializeField, Range(0, 0.5f)] private float _spacing;
    [SerializeField, Range(0, 5f)] private float _introDuration;

    [SerializeField] private Intro _intro;

    [Inject] private CardInstaller.CardFactory _cardFactory;
    [Inject] private Score _score;

    private readonly Queue<Card> _cards = new();
    private Card _lastPlayedCard;

    public void Initialize()
    {
        for (var i = 0; i < _cardAmount; i++)
        {
            var card = _cardFactory.Create(i, Vector3.right * _spacing * i);
            _cards.Enqueue(card);
        }

        _intro.StartIntro(() => Invoke(nameof(StartGame), 1f));
    }

    private void StartGame()
    {
        foreach (var card in _cards)
        {
            card.PlayIntroAnimation(_introDuration);
        }

        Invoke(nameof(PlayNextCard), _introDuration);
    }

    public void OnCardPlayed(Card id)
    {
        _lastPlayedCard = _cards.Dequeue();
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
        return _cards.Any() && _lastPlayedCard.Id == _cards.Peek().Id;
    }

    private void PlayNextCard()
    {
        if (_cards.Any())
        {
            _lastPlayedCard = _cards.Peek();
            _lastPlayedCard.PlayCard();
            OnCardPlayed(_lastPlayedCard);
        }
    }
}