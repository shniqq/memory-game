namespace Card
{
    public class CardController
    {
        private readonly CardView _view;
        private readonly CardModel _model;

        public CardController(CardView view, CardModel model)
        {
            _view = view;
            _model = model;
        }
    }
}