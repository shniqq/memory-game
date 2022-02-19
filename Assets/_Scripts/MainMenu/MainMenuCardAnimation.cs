using System.Collections.Generic;
using MemoryGame.Game.Card;
using UnityEngine;

namespace MemoryGame.MainMenu
{
    public class MainMenuCardAnimation : MonoBehaviour
    {
        [SerializeField] private CardView _cardPrefab;
    
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
                card.PlayIntroAnimation();
            }
        }
    }
}