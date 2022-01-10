using UnityEngine;
using Zenject;

public class GameSceneInstaller : MonoInstaller<GameSceneInstaller>
{
    [SerializeField] private CardStack _cardStack;
    [SerializeField] private Card _cardPrefab;

    public override void InstallBindings()
    {
        Container.BindInterfacesTo<CardStack>().FromInstance(_cardStack);

        Container.BindFactory<int, Card, Card.CardFactory>().FromSubContainerResolve().ByNewContextPrefab<CardInstaller>(_cardPrefab);
    }
}
