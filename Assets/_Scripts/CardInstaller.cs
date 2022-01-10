using UnityEngine;
using Zenject;

public class CardInstaller : MonoInstaller<CardInstaller>
{
    public class CardFactory : PlaceholderFactory<int, Vector3, Card>
    {
    }
    
    [Inject] private int _index;
    [Inject] private Vector3 _position;
    
    public override void InstallBindings()
    {
        Container.Bind<int>().FromInstance(_index).AsSingle().WhenInjectedInto<Card>();
        Container.Bind<Vector3>().FromInstance(_position).AsSingle().WhenInjectedInto<Card>();
        Container.BindInterfacesAndSelfTo<Card>().FromComponentInHierarchy().AsCached();
    }
}