using System;
using UniRx;

namespace MemoryGame.Game.Card
{
    public class CardModel
    {
        private readonly GameStateModel _gameStateModel;
        private uint Index { get; }
        public int ID { get; }

        public IReadOnlyReactiveProperty<bool> HasBeenPlayed => _hasBeenPlayed;
        private readonly ReactiveProperty<bool> _hasBeenPlayed = new();

        public CardModel(int id, uint index, GameStateModel gameStateModel)
        {
            _gameStateModel = gameStateModel;
            Index = index;
            ID = id;
        }

        public void PlayCard()
        {
            _hasBeenPlayed.Value = true;
        }

        public void OnCompletedIntro()
        {
            if (Index == 0)
            {
                _gameStateModel.SetPlaying();
            }
        }
    }
}