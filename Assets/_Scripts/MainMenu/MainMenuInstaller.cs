using System;
using MemoryGame.Game;
using MemoryGame.Game.Card;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace MemoryGame.MainMenu
{
    public class MainMenuInstaller : MonoInstaller<MainMenuInstaller>
    {
        [SerializeField] private ColorConfig[] _colorProviders;
        [SerializeField] private CardView _cardPrefab;
        [SerializeField] private MainMenuView _mainMenuView;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ColorConfig>()
                .FromInstance(_colorProviders[Random.Range(0, _colorProviders.Length)]);

            Container.BindInterfacesTo<MainMenuController>().AsSingle().WithArguments(_mainMenuView).NonLazy();
        
            Container.BindFactory<CardInstaller.CardConstructArguments, Tuple<CardModel, CardView>, CardInstaller.CardFactory>()
                .FromSubContainerResolve()
                .ByNewContextPrefab<CardInstaller>(_cardPrefab)
                .MoveIntoAllSubContainers();
        }
    }
}