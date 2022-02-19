using System;
using MemoryGame.Game.CardStack;
using UniRx;

namespace MemoryGame.Game
{
    public class GameStateModel : IDisposable
    {
        private readonly CardStackModel _cardStackModel;
        private readonly ReactiveProperty<GameState> _gameState = new(Game.GameState.Intro);
        public IReadOnlyReactiveProperty<GameState> GameState => _gameState;

        private readonly CompositeDisposable _disposer = new();

        public GameStateModel(CardStackModel cardStackModel)
        {
            _cardStackModel = cardStackModel;
        }

        public void SetIntro()
        {
            _gameState.Value = Game.GameState.Intro;
        }

        public void SetPlaying()
        {
            _gameState.Value = Game.GameState.Playing;
        }

        public void SetEnded()
        {
            _gameState.Value = Game.GameState.Ended;
        }

        public void Dispose()
        {
            _gameState?.Dispose();
            _disposer?.Dispose();
        }
    }

    public enum GameState
    {
        Intro,
        Playing,
        Ended
    }
}