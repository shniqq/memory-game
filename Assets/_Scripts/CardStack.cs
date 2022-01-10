using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class CardStack : MonoBehaviour, IInitializable
{
    [SerializeField] private uint _cardAmount;
    [SerializeField, Range(0, 0.5f)] private float _spacing;

    [Inject] private Card.CardFactory _cardFactory;

    private readonly Queue<Card> _cards = new();
    private Card _lastPlayedCard;

    public void Initialize()
    {
        for (var i = 0; i < _cardAmount; i++)
        {
            var card = _cardFactory.Create(i);
            card.transform.position = Vector3.right * _spacing * i;
            _cards.Enqueue(card);
        }
        
        Invoke(nameof(PlayNextCard), 2f);
    }

    public void OnCardPlayed(Card id)
    {
        _lastPlayedCard = _cards.Dequeue();
    }

    public void PlayNextCard()
    {
        if (_cards.Any())
        {
            _cards.Peek().PlayCard();
        }
    }
}