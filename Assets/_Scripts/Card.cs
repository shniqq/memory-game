using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

public class Card : MonoBehaviour
{
    [SerializeField] private SortingGroup _sortingGroup;
    [SerializeField, Range(0, 5)] private float _cardPlayDuration;
    [SerializeField] private Ease _cardPlayEase;
    [SerializeField] private SpriteRenderer _frontBackground;
    [SerializeField] private SpriteRenderer _icon;
    

    [Inject]
    private int Index
    {
        set
        {
            _index = value;
            _sortingGroup.sortingOrder = -value;
        }
    }

    [Inject] private CardStack _cardManager;
    [Inject] private Vector3 _position;
    [Inject] private ICardConfigProvider _cardConfigProvider;

    private int _index;
    public int Id { get; private set; }

    private void Start()
    {
        var cardConfig = _cardConfigProvider.GetConfig();
        Id = cardConfig.Item1;
        _icon.sprite = cardConfig.Item2;
        
        transform.position = Vector3.down * 30f + Vector3.back * 10f;

        _frontBackground.color = Random.ColorHSV();
    }

    public void PlayIntroAnimation(float duration)
    {
        DOTween.Sequence()
            .Insert(0f, transform.DOMoveY(_position.y, duration * 0.6f).SetEase(Ease.OutBack))
            .Insert(duration * 0.6f, transform.DOMoveX(_position.x, duration * 0.3f).SetEase(Ease.Linear))
            .Insert(duration * 0.6f, transform.DOMoveZ(_position.z, duration * 0.3f).SetEase(Ease.Linear));
    }

    public void PlayCard()
    {
        _cardManager.OnCardPlayed(this);
        DOTween.Sequence().SetEase(_cardPlayEase)
            .Insert(0f, transform.DORotate(new Vector3(0, 180, 0), _cardPlayDuration, RotateMode.WorldAxisAdd))
            .Insert(0f, transform.DOMoveX(-transform.position.x, _cardPlayDuration))
            .InsertCallback(0.5f, () => _sortingGroup.sortingOrder = _index);
    }
}