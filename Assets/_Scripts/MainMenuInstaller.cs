using System;
using MemoryGame.Card;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace MemoryGame
{
    public class MainMenuInstaller : MonoInstaller<MainMenuInstaller>
    {
        [SerializeField] private ColorProvider[] _colorProviders;
        [SerializeField] private CardView _cardPrefab;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ColorProvider>()
                .FromInstance(_colorProviders[Random.Range(0, _colorProviders.Length)]);
        
            Container.BindFactory<CardInstaller.CardConstructArguments, Tuple<CardModel, CardView>, CardInstaller.CardFactory>()
                .FromSubContainerResolve()
                .ByNewContextPrefab<CardInstaller>(_cardPrefab)
                .MoveIntoAllSubContainers();
        }
    }
}