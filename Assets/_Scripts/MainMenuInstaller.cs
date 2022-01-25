using UnityEngine;
using Zenject;

public class MainMenuInstaller : MonoInstaller<MainMenuInstaller>
{
    [SerializeField] private ColorProvider[] _colorProviders;
    [SerializeField] private Card _cardPrefab;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<ColorProvider>()
            .FromInstance(_colorProviders[Random.Range(0, _colorProviders.Length)]);
        
        Container.BindFactory<int, Vector3, Card, CardInstaller.CardFactory>()
            .FromSubContainerResolve()
            .ByNewContextPrefab<CardInstaller>(_cardPrefab)
            .MoveIntoAllSubContainers();
    }
}