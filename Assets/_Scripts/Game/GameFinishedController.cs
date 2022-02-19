using System;
using UniRx;
using Zenject;

namespace MemoryGame.Game
{
    public class GameFinishedController : IInitializable
    {
        private readonly GameStateModel _cardStackModel;
        private readonly ZenjectSceneLoader _sceneLoader;

        public GameFinishedController(GameStateModel cardStackModel, ZenjectSceneLoader sceneLoader)
        {
            _cardStackModel = cardStackModel;
            _sceneLoader = sceneLoader;
        }

        public void Initialize()
        {
            _cardStackModel.GameState.Where(e => e == GameState.Ended).Take(1).Delay(TimeSpan.FromSeconds(2))
                .Subscribe(_ => _sceneLoader.LoadScene("MainMenu"));
        }
    }
}