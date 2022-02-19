using MemoryGame.Background;
using MemoryGame.Game.Card;
using UnityEngine;

namespace MemoryGame.Game
{
    [CreateAssetMenu(menuName = "Game/Color Provider")]
    public class ColorConfig : ScriptableObject, ICardColorProvider, IBackgroundColorProvider
    {
        [SerializeField] private Color[] _cardColors;
        [SerializeField] private Color _backgroundColor;

        public Color GetBackgroundColor()
        {
            return _backgroundColor;
        }

        public Color GetCardColor(int index)
        {
            return _cardColors[(int)Mathf.Repeat(index, _cardColors.Length)];
        }
    }
}