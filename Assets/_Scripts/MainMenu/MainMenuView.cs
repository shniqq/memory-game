using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MemoryGame.MainMenu
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Canvas _mainMenuCanvas;

        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private float _scale;
        [SerializeField] private float _scaleDuration;
        [SerializeField] private float _rotateDuration;
        [SerializeField] private float _rotate;

        public Button PlayButton => _playButton;

        private void Awake()
        {
            _titleText.rectTransform.DOScale(_scale, _scaleDuration).SetLoops(-1, LoopType.Yoyo);
            _titleText.rectTransform.DORotate(new Vector3(0, 0, _rotate), _rotateDuration)
                .ChangeStartValue(new Vector3(0, 0, -_rotate))
                .SetLoops(-1, LoopType.Yoyo);
        }

        public void OnEnterGameScene()
        {
            _mainMenuCanvas.enabled = false;
        }
    }
}