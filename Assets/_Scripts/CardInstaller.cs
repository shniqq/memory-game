using System;
using Card;
using UnityEngine;
using Zenject;

public class CardInstaller : MonoInstaller<CardInstaller>
{
    public struct CardConstructArguments
    {
        public readonly int Index;
        public readonly Vector3 Position;
        public readonly int Id;

        public CardConstructArguments(int id, int index, Vector3 position)
        {
            Id = id;
            Index = index;
            Position = position;
        }
    }

    public class CardFactory : PlaceholderFactory<CardConstructArguments, Tuple<CardModel, CardView>>
    {
    }

    [Inject] private CardConstructArguments _constructArguments;
    [Inject] private ICardConfigProvider _cardConfigProvider;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<CardModel>().AsSingle()
            .WithArguments(_constructArguments.Id);
        Container.Bind<CardController>().AsSingle().NonLazy();
        Container.Bind<Vector3>().FromInstance(_constructArguments.Position).AsSingle().WhenInjectedInto<CardView>();
        Container.Bind<int>().FromInstance(_constructArguments.Index).AsSingle().WhenInjectedInto<CardView>();
        Container.Bind<Sprite>().FromInstance(_cardConfigProvider.GetConfig(_constructArguments.Id));
        Container.BindInterfacesAndSelfTo<CardView>()
            .FromComponentInHierarchy()
            .AsCached();

        Container.Bind<Tuple<CardModel, CardView>>().FromMethod(() =>
            new Tuple<CardModel, CardView>(Container.Resolve<CardModel>(), Container.Resolve<CardView>())).AsSingle();
    }
}