using System;
using UnityEngine;
using Zenject;

namespace MemoryGame.Game.Card
{
    public class CardInstaller : MonoInstaller<CardInstaller>
    {
        public struct CardConstructArguments
        {
            public readonly uint Index;
            public readonly Vector3 Position;
            public readonly int Id;

            public CardConstructArguments(int id, uint index, Vector3 position)
            {
                Id = id;
                Index = index;
                Position = position;
            }
        }

        public class CardFactory : PlaceholderFactory<CardConstructArguments, CardModel>
        {
        }

        [Inject] private CardConstructArguments _constructArguments;
        [Inject] private ICardConfigProvider _cardConfigProvider;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<CardModel>().AsSingle()
                .WithArguments(_constructArguments.Id);
            Container.BindInterfacesTo<CardController>().AsSingle().NonLazy();
            Container.Bind<Vector3>().FromInstance(_constructArguments.Position).AsSingle()
                .WhenInjectedInto<CardView>();
            Container.Bind<uint>().FromInstance(_constructArguments.Index).AsSingle()
                .WhenInjectedInto(typeof(CardView), typeof(CardModel));
            Container.Bind<Sprite>().FromInstance(_cardConfigProvider.GetConfig(_constructArguments.Id));
            Container.BindInterfacesAndSelfTo<CardView>()
                .FromComponentInHierarchy()
                .AsCached();
        }
    }
}