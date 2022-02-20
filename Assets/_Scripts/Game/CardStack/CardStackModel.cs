using System;
using System.Collections.Generic;
using System.Linq;
using MemoryGame.Game.Card;
using MemoryGame.Game.Difficulty;
using MemoryGame.Game.Score;
using UniRx;
using UnityEngine;
using Zenject;

namespace MemoryGame.Game.CardStack
{
    public class CardStackModel : IInitializable, IDisposable
    {
        [Inject] private CardInstaller.CardFactory _cardFactory;
        [Inject] private ScoreModel _scoreModel;
        [Inject] private GameStateModel _gameStateModel;
        [Inject] private ICardIdProvider _cardIdProvider;
        [Inject] private ICardStackConfig _cardStackConfig;

        private readonly Queue<CardModel> _cards = new();
        private CardModel _lastPlayedCard;

        private readonly ReactiveProperty<int> _cardsLeft = new();
        public IReadOnlyReactiveProperty<int> CardsLeft => _cardsLeft;

        public void Initialize()
        {
            for (uint i = 0; i < _cardStackConfig.CardAmount; i++)
            {
                var card = _cardFactory.Create(
                    new CardInstaller.CardConstructArguments(_cardIdProvider.GetId(), i, Vector3.right * _cardStackConfig.Spacing * i));
                _cards.Enqueue(card);
            }

            _cardsLeft.Value = _cards.Count;
            _gameStateModel.GameState.Pairwise()
                .Where(pair => pair.Previous == GameState.Intro && pair.Current == GameState.Playing)
                .Subscribe(_ => PlayNextCard());
            CardsLeft.Pairwise().Where(pair => pair.Previous > 0 && pair.Current == 0)
                .Subscribe(_ => _gameStateModel.SetEnded());
        }

        private void PlayNextCard()
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
            _scoreModel.OnPlayerChoice(IsLastCardSameAsCurrent());
            PlayNextCard();
        }

        public void OnDecidedDifferentCard()
        {
            _scoreModel.OnPlayerChoice(!IsLastCardSameAsCurrent());
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