using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class Background : MonoBehaviour
{
    [SerializeField] private Sprite[] _backgroundSprites;
    [SerializeField] private SpriteRenderer _backgroundSpriteRenderer;
    [SerializeField] private Camera _camera;
    [SerializeField, Range(0, 1)] private float _alpha;

    [Inject] private IColorProvider _colorProvider;
    
    private void Awake()
    {
        _backgroundSpriteRenderer.sprite = _backgroundSprites[Random.Range(0, _backgroundSprites.Length)];
        var backgroundColor = _colorProvider.GetBackgroundColor();
        backgroundColor.a = _alpha;
        _backgroundSpriteRenderer.color = backgroundColor;
        _camera.backgroundColor = backgroundColor;
    }
}
