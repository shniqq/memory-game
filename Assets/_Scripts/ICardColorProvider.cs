using UnityEngine;

namespace MemoryGame
{
    public interface ICardColorProvider
    {
        public Color GetCardColor(int index);
    }
}