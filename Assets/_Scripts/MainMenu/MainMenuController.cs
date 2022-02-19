using UniRx;
using UnityEngine.SceneManagement;
using Zenject;

namespace MemoryGame.MainMenu
{
    public class MainMenuController : IInitializable
    {
        private readonly MainMenuView _view;
        private readonly ZenjectSceneLoader _sceneLoader;

        public MainMenuController(MainMenuView view, ZenjectSceneLoader sceneLoader)
        {
            _view = view;
            _sceneLoader = sceneLoader;
        }
        
        public void Initialize()
        {
            _view.PlayButton.OnClickAsObservable().Subscribe(_ =>
            {
                _sceneLoader.LoadScene("GameScene", LoadSceneMode.Additive, containerMode: LoadSceneRelationship.Child);
                _view.OnEnterGameScene();
            }).AddTo(_view);
        }
    }
}