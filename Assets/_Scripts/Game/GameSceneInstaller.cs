using MemoryGame.Game.Card;
using MemoryGame.Game.CardStack;
using MemoryGame.Game.Difficulty;
using MemoryGame.Game.Feedback;
using MemoryGame.Game.Hud;
using MemoryGame.Game.Score;
using UnityEngine;
using Zenject;

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
    
        [SerializeField] private uint _cardAmount;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<CardStackView>().FromInstance(_cardStackView);
            Container.BindInterfacesAndSelfTo<CardStackModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<CardStackController>().AsSingle().NonLazy();
            Container.BindInstance(_cardAmount).WhenInjectedInto<CardStackModel>();

            Container.QueueForInject(_cardDistributionConfig);
            Container.BindInterfacesTo<CardDistributionConfig>().FromInstance(_cardDistributionConfig).AsSingle();

            Container.BindInstance(_hudView).AsSingle();
            Container.BindInterfacesTo<HudController>().AsSingle().NonLazy();

            Container.BindInterfacesTo<GameFinishedController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameStateModel>().AsSingle();
        
            Container.BindInterfacesAndSelfTo<CardConfigProvider>()
                .FromInstance(_cardConfigProviders[Random.Range(0, _cardConfigProviders.Length)]);
            Container.BindInstance(_scoreView).AsSingle();
            Container.BindInterfacesAndSelfTo<ScoreModel>().AsSingle();
            Container.BindInterfacesTo<ScoreController>().AsSingle().NonLazy();
            Container.BindInstance(_introView).AsSingle();
            Container.BindInstance(_feedbackView).AsSingle();
            Container.BindInterfacesTo<FeedbackController>().AsSingle().NonLazy();
            Container.BindInterfacesTo<FeedbackConfig>().FromInstance(_feedbackConfig).AsSingle();
            Container.BindInstance(_decisionInputView).AsSingle();
        }
    }
}