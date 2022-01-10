using Zenject;

public class CardInstaller : MonoInstaller<CardInstaller>
{
    [Inject] private int _index;

    public override void InstallBindings()
    {
        Container.Bind<int>().FromInstance(_index).AsSingle().WhenInjectedInto<Card>();
        Container.BindInterfacesAndSelfTo<Card>().FromComponentInHierarchy().AsSingle();
    }
}