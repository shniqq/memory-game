using System;
using MemoryGame.Utils;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace MemoryGame.Game.Difficulty
{
    [CreateAssetMenu(menuName = "Game/Card Distribution Config")]
    public class CardDistributionConfig : ScriptableObject, ICardIdProvider
    {
        [SerializeField, Header("Chance to receive the same card in a row")]
        private AnimationCurve _distribution;

        private int _sameCardStreak;
        private int _lastId;

        [Inject] private IRandom _random;

        public int GetId()
        {
            var chance = _distribution.Evaluate(Math.Min(_sameCardStreak, _distribution.length));
            var sameIdAgain = Random.value >= chance;
            if (sameIdAgain)
            {
                _sameCardStreak++;
                return _lastId;
            }

            _sameCardStreak = 0;
            _lastId = _random.Range(0, 4);
            return _lastId;
        }
    }
}