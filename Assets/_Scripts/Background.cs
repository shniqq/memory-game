using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class Background : MonoBehaviour
{
    [SerializeField] private Sprite[] _backgroundSprites;
    [SerializeField] private SpriteRenderer _patternSpriteRenderer;
    [SerializeField] private SpriteRenderer _backgroundSpriteRenderer;
    [SerializeField, Range(0, 1)] private float _alpha;

    [Inject] private IBackgroundColorProvider _colorProvider;

    private void Awake()
    {
        _patternSpriteRenderer.sprite = _backgroundSprites[Random.Range(0, _backgroundSprites.Length)];
        var backgroundColor = _colorProvider.GetBackgroundColor();
        _backgroundSpriteRenderer.color = backgroundColor;
        backgroundColor.a = _alpha;
        _patternSpriteRenderer.color = backgroundColor;
    }
}