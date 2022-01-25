using UnityEngine;
using Zenject;

public class MainMenuInstaller : MonoInstaller<MainMenuInstaller>
{
    [SerializeField] private ColorProvider[] _colorProviders;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<ColorProvider>()
            .FromInstance(_colorProviders[Random.Range(0, _colorProviders.Length)]);
    }
}