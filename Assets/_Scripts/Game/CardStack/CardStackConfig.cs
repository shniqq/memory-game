using UnityEngine;

namespace MemoryGame.Game.CardStack
{
    [CreateAssetMenu(menuName = "Game/Card Stack Config")]
    public class CardStackConfig : ScriptableObject, ICardStackConfig
    {
        [SerializeField] private uint _cardAmount;
        [SerializeField, Range(0, 1)] private float _spacing;

        public uint CardAmount => _cardAmount;
        public float Spacing => _spacing;
    }
}