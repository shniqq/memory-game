using UnityEngine;
using Zenject;

public class GameSceneInstaller : MonoInstaller<GameSceneInstaller>
{
    [SerializeField] private Score _score;
    [SerializeField] private CardStack _cardStack;
    [SerializeField] private Card _cardPrefab;
    [SerializeField] private CardConfigProvider _cardConfigProvider;
    [SerializeField] private ColorProvider[] _colorProviders;
    [SerializeField] private Feedback _feedback;
    [SerializeField] private Background _background;

    public override void InstallBindings()
    {
        Container.BindInterfacesTo<CardConfigProvider>().FromInstance(_cardConfigProvider).AsSingle();
        Container.BindInterfacesAndSelfTo<CardStack>().FromInstance(_cardStack);
        Container.BindInterfacesAndSelfTo<ColorProvider>()
            .FromInstance(_colorProviders[Random.Range(0, _colorProviders.Length)]);
        Container.BindInstance(_score).AsSingle();
        Container.BindInstance(_feedback).AsSingle();
        Container.BindInstance(_background).AsSingle();

        Container.BindFactory<int, Vector3, Card, CardInstaller.CardFactory>()
            .FromSubContainerResolve()
            .ByNewContextPrefab<CardInstaller>(_cardPrefab);
    }
}