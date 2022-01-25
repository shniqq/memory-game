using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Canvas _mainMenuCanvas;

    [Inject] private ZenjectSceneLoader _sceneLoader;

    private void Awake()
    {
        _playButton.OnClickAsObservable().Subscribe(_ =>
        {
            _sceneLoader.LoadScene("GameScene", LoadSceneMode.Additive, containerMode: LoadSceneRelationship.Child);
            _mainMenuCanvas.enabled = false;
        }).AddTo(this);
    }
}