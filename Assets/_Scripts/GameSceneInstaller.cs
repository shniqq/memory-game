using UnityEngine;
using Zenject;

public class GameSceneInstaller : MonoInstaller<GameSceneInstaller>
{
    [SerializeField] private Score _score;
    [SerializeField] private CardStack _cardStack;
    [SerializeField] private CardConfigProvider[] _cardConfigProviders;
    [SerializeField] private Feedback _feedback;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<CardStack>().FromInstance(_cardStack);
        Container.BindInterfacesAndSelfTo<CardConfigProvider>()
            .FromInstance(_cardConfigProviders[Random.Range(0, _cardConfigProviders.Length)]);
        Container.BindInstance(_score).AsSingle();
        Container.BindInstance(_feedback).AsSingle();
    }
}