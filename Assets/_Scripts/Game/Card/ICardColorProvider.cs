using UnityEngine;

namespace MemoryGame.Game.Card
{
    public interface ICardColorProvider
    {
        public Color GetCardColor(int index);
    }
}