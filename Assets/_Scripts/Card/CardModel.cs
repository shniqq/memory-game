using System;
using CardStack;
using UniRx;

namespace Card
{
    public class CardModel
    {
        private readonly CardStackModel _cardStackModel;
        public uint Index { get; }
        public int ID { get; }

        public IObservable<Unit> OnPlayedCard => _onPlayedCard;
        private readonly Subject<Unit> _onPlayedCard = new();

        public CardModel(int id, uint index, CardStackModel cardStackModel)
        {
            _cardStackModel = cardStackModel;
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
                _cardStackModel.PlayNextCard();
            }
        }
    }
}