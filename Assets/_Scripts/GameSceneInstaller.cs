using MemoryGame.CardStack;
using UnityEngine;
using Zenject;

namespace MemoryGame
{
    public class GameSceneInstaller : MonoInstaller<GameSceneInstaller>
    {
        [SerializeField] private Score _score;
        [SerializeField] private CardStackView _cardStackView;
        [SerializeField] private CardConfigProvider[] _cardConfigProviders;
        [SerializeField] private FeedbackView _feedbackView;
        [SerializeField] private IntroView _introView;
        [SerializeField] private DecisionInputView _decisionInputView;
        [SerializeField] private HudView _hudView;
    
        [SerializeField] private uint _cardAmount;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<CardStackView>().FromInstance(_cardStackView);
            Container.BindInterfacesAndSelfTo<CardStackModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<CardStackController>().AsSingle().NonLazy();
            Container.BindInstance(_cardAmount).WhenInjectedInto<CardStackModel>();

            Container.BindInstance(_hudView).AsSingle();
            Container.BindInterfacesTo<HudController>().AsSingle().NonLazy();

            Container.BindInterfacesTo<GameFinishedController>().AsSingle().NonLazy();
        
            Container.BindInterfacesAndSelfTo<CardConfigProvider>()
                .FromInstance(_cardConfigProviders[Random.Range(0, _cardConfigProviders.Length)]);
            Container.BindInstance(_score).AsSingle();
            Container.BindInstance(_introView).AsSingle();
            Container.BindInstance(_feedbackView).AsSingle();
            Container.BindInstance(_decisionInputView).AsSingle();
        }
    }
}