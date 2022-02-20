using MemoryGame.Game;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace MemoryGame.MainMenu
{
    public class MainMenuInstaller : MonoInstaller<MainMenuInstaller>
    {
        [SerializeField] private ColorConfig[] _colorProviders;
        [SerializeField] private MainMenuView _mainMenuView;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ColorConfig>()
                .FromInstance(_colorProviders[Random.Range(0, _colorProviders.Length)]);

            Container.BindInterfacesTo<MainMenuController>().AsSingle().WithArguments(_mainMenuView).NonLazy();
        }
    }
}