using System;
using MemoryGame.Game.Card;
using MemoryGame.Game.CardStack;
using MemoryGame.Game.Difficulty;
using MemoryGame.Game.Feedback;
using MemoryGame.Game.Hud;
using MemoryGame.Game.Score;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace MemoryGame.Game
{
    public class GameSceneInstaller : MonoInstaller<GameSceneInstaller>
    {
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private CardStackView _cardStackView;
        [SerializeField] private CardConfigProvider[] _cardConfigProviders;
        [SerializeField] private FeedbackConfig _feedbackConfig;
        [SerializeField] private FeedbackView _feedbackView;
        [SerializeField] private IntroView _introView;
        [SerializeField] private DecisionInputView _decisionInputView;
        [SerializeField] private HudView _hudView;
        [SerializeField] private CardDistributionConfig _cardDistributionConfig;
        [SerializeField] private CardView _cardPrefab;
        [SerializeField] private CardStackConfig _cardStackConfig;

        public override void InstallBindings()
        {
            InstallCardStack();
            InstallConfigs();
            InstallHud();
            InstallGameState();
            InstallScore();
            InstallFeedback();

            Container
                .BindFactory<CardInstaller.CardConstructArguments, CardModel, CardInstaller.CardFactory>()
                .FromSubContainerResolve()
                .ByNewContextPrefab<CardInstaller>(_cardPrefab);
        }

        private void InstallConfigs()
        {
            Container.BindInterfacesTo<CardStackConfig>().FromInstance(_cardStackConfig).AsSingle();
            Container.QueueForInject(_cardDistributionConfig);
            Container.BindInterfacesTo<CardDistributionConfig>().FromInstance(_cardDistributionConfig).AsSingle();
            Container.BindInterfacesAndSelfTo<CardConfigProvider>()
                .FromInstance(_cardConfigProviders[Random.Range(0, _cardConfigProviders.Length)]);
        }

        private void InstallCardStack()
        {
            Container.BindInterfacesAndSelfTo<CardStackView>().FromInstance(_cardStackView);
            Container.BindInterfacesAndSelfTo<CardStackModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<CardStackController>().AsSingle().NonLazy();
        }

        private void InstallGameState()
        {
            Container.BindInterfacesTo<GameFinishedController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameStateModel>().AsSingle();
        }

        private void InstallHud()
        {
            Container.BindInstance(_hudView).AsSingle();
            Container.BindInterfacesTo<HudController>().AsSingle().NonLazy();

            Container.BindInstance(_decisionInputView).AsSingle();
            Container.BindInstance(_introView).AsSingle();
        }

        private void InstallScore()
        {
            Container.BindInstance(_scoreView).AsSingle();
            Container.BindInterfacesAndSelfTo<ScoreModel>().AsSingle();
            Container.BindInterfacesTo<ScoreController>().AsSingle().NonLazy();
        }

        private void InstallFeedback()
        {
            Container.BindInstance(_feedbackView).AsSingle();
            Container.BindInterfacesTo<FeedbackController>().AsSingle().NonLazy();
            Container.BindInterfacesTo<FeedbackConfig>().FromInstance(_feedbackConfig).AsSingle();
        }
    }
}