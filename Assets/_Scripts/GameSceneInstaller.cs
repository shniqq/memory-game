using UnityEngine;
using Zenject;

public class GameSceneInstaller : MonoInstaller<GameSceneInstaller>
{
    [SerializeField] private Score _score;
    [SerializeField] private CardStack _cardStack;
    [SerializeField] private Card _cardPrefab;
    [SerializeField] private CardConfigProvider _cardConfigProvider;
    [SerializeField] private Feedback _feedback;

    public override void InstallBindings()
    {
        Container.BindInterfacesTo<CardConfigProvider>().FromInstance(_cardConfigProvider).AsSingle();
        Container.BindInterfacesAndSelfTo<CardStack>().FromInstance(_cardStack);
        Container.BindInstance(_score).AsSingle();
        Container.BindInstance(_feedback).AsSingle();

        Container.BindFactory<int, Vector3, Card, CardInstaller.CardFactory>()
            .FromSubContainerResolve()
            .ByNewContextPrefab<CardInstaller>(_cardPrefab);
    }
}
