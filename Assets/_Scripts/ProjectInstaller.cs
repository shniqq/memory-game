using MemoryGame.Utils;
using NaughtyAttributes;
using UnityEngine;
using Zenject;
using Random = MemoryGame.Utils.Random;

namespace MemoryGame
{
    public class ProjectInstaller : MonoInstaller<ProjectInstaller>
    {
        [SerializeField, HideIf(nameof(_randomNumbersFromFile))]
        private bool _customSeed;
        [SerializeField, HideIf(nameof(_randomNumbersFromFile)), ShowIf(nameof(_customSeed))]
        private int _seed;
        [SerializeField, HideIf(nameof(_randomNumbersFromFile))] 
        private bool _saveRandomNumbers;
        
        [SerializeField] private bool _randomNumbersFromFile;
        [SerializeField, ShowIf(nameof(_randomNumbersFromFile))]
        private TextAsset _randomNumbers;

        public override void InstallBindings()
        {
            if (_randomNumbersFromFile)
            {
                Container.BindInterfacesTo<RandomFromFile>().AsSingle().WithArguments(_randomNumbers);
            }
            else
            {
                Container.BindInterfacesTo<Random>().AsSingle().WithArguments(_saveRandomNumbers);
                if (_customSeed)
                {
                    Container.Bind<int?>().FromInstance(_seed).AsSingle().WhenInjectedInto<Random>();
                }
            }
        }
    }
}