using System;
using MemoryGame.CardStack;
using UniRx;
using Zenject;

namespace MemoryGame
{
    public class GameFinishedController : IInitializable
    {
        private readonly CardStackModel _cardStackModel;
        private readonly ZenjectSceneLoader _sceneLoader;

        public GameFinishedController(CardStackModel cardStackModel, ZenjectSceneLoader sceneLoader)
        {
            _cardStackModel = cardStackModel;
            _sceneLoader = sceneLoader;
        }

        public void Initialize()
        {
            _cardStackModel.CardsLeft.SkipWhile(amount => amount > 0).Take(1).Delay(TimeSpan.FromSeconds(2)).Subscribe(_ => _sceneLoader.LoadScene("MainMenu"));
        }
    }
}