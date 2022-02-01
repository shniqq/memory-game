using System;
using System.Collections.Generic;
using System.Linq;
using MemoryGame.Card;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace MemoryGame.CardStack
{
    public class CardStackModel : IInitializable, IDisposable
    {
        [Inject] private CardInstaller.CardFactory _cardFactory;
        [Inject] private Score _score;

        private readonly uint _cardAmount;
        private readonly float _spacing;

        private readonly Queue<CardModel> _cards = new();
        private CardModel _lastPlayedCard;

        private readonly ReactiveProperty<int> _cardsLeft = new();
        public IReadOnlyReactiveProperty<int> CardsLeft => _cardsLeft;

        public CardStackModel(uint cardAmount)
        {
            _cardAmount = cardAmount;
            _spacing = 0.2f;
        }

        public void Initialize()
        {
            for (uint i = 0; i < _cardAmount; i++)
            {
                var card = _cardFactory.Create(
                    new CardInstaller.CardConstructArguments(Random.Range(0, 3), i, Vector3.right * _spacing * i));
                _cards.Enqueue(card.Item1);
            }

            _cardsLeft.Value = _cards.Count;
        }

        public void PlayNextCard()
        {
            if (_cards.Any())
            {
                _lastPlayedCard = _cards.Peek();
                _lastPlayedCard.OnPlayCard();
                _lastPlayedCard = _cards.Dequeue();
                _cardsLeft.Value = _cards.Count;
            }
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
            return _cards.Any() && _lastPlayedCard.ID == _cards.Peek().ID;
        }

        public void Dispose()
        {
            _cardsLeft?.Dispose();
        }
    }
}