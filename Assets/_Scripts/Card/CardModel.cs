using System;
using UniRx;

namespace MemoryGame.Card
{
    public class CardModel
    {
        private readonly GameStateModel _gameStateModel;
        public uint Index { get; }
        public int ID { get; }

        public IObservable<Unit> OnPlayedCard => _onPlayedCard;
        private readonly Subject<Unit> _onPlayedCard = new();

        public CardModel(int id, uint index, GameStateModel gameStateModel)
        {
            _gameStateModel = gameStateModel;
            Index = index;
            ID = id;
        }

        public void OnPlayCard()
        {
            _onPlayedCard.OnNext(Unit.Default);
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