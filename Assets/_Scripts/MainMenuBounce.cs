using DG.Tweening;
using TMPro;
using UnityEngine;

namespace MemoryGame
{
    public class MainMenuBounce : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private float _scale;
        [SerializeField] private float _scaleDuration;
        [SerializeField] private float _rotateDuration;
        [SerializeField] private float _rotate;

        private void Awake()
        {
            _titleText.rectTransform.DOScale(_scale, _scaleDuration).SetLoops(-1, LoopType.Yoyo);
            _titleText.rectTransform.DORotate(new Vector3(0, 0, _rotate), _rotateDuration)
                .ChangeStartValue(new Vector3(0, 0, -_rotate))
                .SetLoops(-1, LoopType.Yoyo);
        }
    }
}