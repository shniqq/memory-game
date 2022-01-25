using System.Collections.Generic;
using Card;
using UnityEngine;

public class MainMenuCardAnimation : MonoBehaviour
{
    [SerializeField] private CardView _cardPrefab;
    [SerializeField] private float _intoAnimationDuration;
    
    [SerializeField] private uint _amount;
    
    
    private void Awake()
    {
        var cards = new List<CardView>();
        for (var i = 0; i < _amount; i++)
        {
            var card = Instantiate(_cardPrefab);
            cards.Add(card);
        }
        
        foreach (var card in cards)
        {
            card.PlayIntroAnimation(_intoAnimationDuration);
        }
    }
}