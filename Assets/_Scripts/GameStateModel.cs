using System;
using MemoryGame.CardStack;
using UniRx;

namespace MemoryGame
{
    public class GameStateModel : IDisposable
    {
        private readonly CardStackModel _cardStackModel;
        private readonly ReactiveProperty<GameState> _gameState = new(MemoryGame.GameState.Intro);
        public IReadOnlyReactiveProperty<GameState> GameState => _gameState;

        private readonly CompositeDisposable _disposer = new();

        public GameStateModel(CardStackModel cardStackModel)
        {
            _cardStackModel = cardStackModel;
        }

        public void SetIntro()
        {
            _gameState.Value = MemoryGame.GameState.Intro;
        }

        public void SetPlaying()
        {
            _gameState.Value = MemoryGame.GameState.Playing;
        }

        public void SetEnded()
        {
            _gameState.Value = MemoryGame.GameState.Ended;
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