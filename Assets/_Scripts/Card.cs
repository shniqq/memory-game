using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

public class Card : MonoBehaviour
{
    public class CardFactory : PlaceholderFactory<int, Card>
    {
    }

    [SerializeField] private SortingGroup _sortingGroup;
    [SerializeField, Range(0, 5)] private float _cardPlayDuration;
    [SerializeField] private Ease _cardPlayEase;

    [Inject]
    private int Index
    {
        set
        {
            _index = value;
            _sortingGroup.sortingOrder = -value;
        }
    }
    private int _index;
    private int _id;
    public int Id => _id;

    [Inject] private CardStack _cardManager;

    private void Start()
    {
        _id = Random.Range(0, 4);
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